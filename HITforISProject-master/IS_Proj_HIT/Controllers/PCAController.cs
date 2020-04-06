using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models.Enum;
using IS_Proj_HIT.Models.PCA;
using IS_Proj_HIT.Services;
using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace IS_Proj_HIT.Controllers
{
    public class PCAController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;

        public PCAController(IWCTCHealthSystemRepository repository) => _repository = repository;

        /// <summary>
        ///     Show a detailed view of a single PCA
        /// </summary>
        /// <param name="assessmentId">PCA Id for Db Lookup</param>
        public IActionResult ViewAssessment(int assessmentId)
        {
            var assessment = _repository.PcaRecords
                .Include(pca => pca.Encounter)
                .Include(pca => pca.PcaComment)
                .ThenInclude(com => com.PcacommentType)
                .Include(pca => pca.CareSystemAssessment)
                .ThenInclude(ca => ca.CareSystemParameter)
                .ThenInclude(cp => cp.CareSystemType)
                .Include(pca => pca.PainAssessment)
                .ThenInclude(pa => pa.PainParameter)
                .Include(pca => pca.PainAssessment)
                .ThenInclude(pa => pa.PainRating)
                .Include(pca => pca.PainScaleType)
                .Include(pca => pca.TempRouteType)
                .Include(pca => pca.BmiMethod)
                .Include(pca => pca.BloodPressureRouteType)
                .Include(pca => pca.PulseRouteType)
                .Include(pca => pca.O2deliveryType)
                .FirstOrDefault(pca => pca.PcaId == assessmentId);
            if (assessment is null)
                return RedirectToAction("Index", "Encounter",
                    new {filter = "CheckedIn"});

            var patientAlerts = _repository.PatientAlerts.Count(a => a.Mrn == assessment.Encounter.Mrn);
            ViewBag.PatientAlertsCount = patientAlerts;
            ViewBag.Patient = _repository.Patients.FirstOrDefault(p => p.Mrn == assessment.Encounter.Mrn);
            
            return View(assessment);
        }

        /// <summary>
        /// todo: Expand this to display the form to enter a PCA, parameters tenative
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        /// <param name="patientMrn">Unique Identifier of patient</param>
        public IActionResult CreateAssessment(long encounterId, string mrn)
        {
            var encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == encounterId);
            var patient = _repository.Patients.FirstOrDefault(p => p.Mrn == mrn);
            var patientAlerts = _repository.PatientAlerts.Count(b => b.Mrn == mrn);

            if (encounter is null || patient is null)
                return RedirectToAction("ViewEncounter", "Encounter",
                    new {encounterId = encounterId, isPatientEncounter = false});

            ViewBag.Encounter = encounter;
            ViewBag.Patient = patient;
            ViewBag.PatientAlertsCount = patientAlerts;

            AddUnites();
            AddRoutes();
            var painScales = _repository.PainScaleTypes
                .Include(ps => ps.PainParameters)
                .ThenInclude(pp => pp.PainRatings)
                .ToList();
            var painRatings = new Dictionary<int, int?>();
            painScales.ForEach(ps =>
                ps.PainParameters.ToList().ForEach(pp =>
                    pp.PainRatings.ToList().ForEach(pr =>
                        painRatings.Add(pr.PainRatingId, null))));

            var secondarySystems = _repository.CareSystemAssessmentTypes
                .Include(cs => cs.CareSystemParameters)
                .ToList();
            var sysAssessments = new Dictionary<int, CareSystemAssessment>();
            secondarySystems.ForEach(s =>
                s.CareSystemParameters.ToList().ForEach(sp =>
                    sysAssessments.Add(sp.CareSystemParameterId,
                        new CareSystemAssessment
                        {
                            CareSystemParameterId = sp.CareSystemParameterId,
                            IsWithinNormalLimits = null
                        })));

            var newFormPca = new AssessmentFormPageModel
            {
                PainScales = painScales,
                PainRatings = painRatings,
                SecondarySystemTypes = secondarySystems,
                Assessments = sysAssessments
            };

            return View(newFormPca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAssessment(AssessmentFormPageModel formPca)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == formPca.PcaRecord.EncounterId);
                ViewBag.Patient = _repository.Patients.FirstOrDefault(p => p.Mrn == formPca.PatientMrn);
                ViewBag.PatientAlertsCount  = _repository.PatientAlerts.Count(b => b.Mrn == formPca.PatientMrn);
                return View(formPca);
            }

            return SaveAssessment(formPca);
        }

        /// <summary>
        /// todo: Expand this method to display the form to update an existing PCA, parameters are tenative
        /// </summary>
        /// <param name = "assessmentId" > PCA Id for Db Lookup</param>
        ///  <param name = "patientMrn" > Unique Identifier of patient</param>
        ///   <param name = "encounterId" > Unique Identifier of patient</param>
        public IActionResult UpdateAssessment(int assessmentId,string patientMRN,long encounterId)
        {
           var assessment = _repository.PcaRecords.Include(pc => pc.CareSystemAssessment).FirstOrDefault(pc => pc.PcaId == assessmentId);
            var patient = _repository.Patients.FirstOrDefault(p => p.Mrn == patientMRN);
            var encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == encounterId);
            var patientAlerts = _repository.PatientAlerts.Where(b => b.Mrn == patientMRN).Count();

            if (assessment is null || patient is null) 
                return RedirectToAction("ViewAssessment", "PCA",
                    new {assessmentId = assessment.PcaId });
            ViewBag.PcaRecord = assessment;
            ViewBag.Patient = patient;
            ViewBag.Encounter = encounter;
            ViewBag.PatientAlertsCount = patientAlerts;


            var model = new AssessmentFormPageModel();
            return View(model);
        }
        
        private IActionResult SaveAssessment(AssessmentFormPageModel formPca)
        {
            var pca = formPca.PcaRecord;
            //Convert temp to F, if entered in other unit
            if(pca.Temperature != null)
            {
                Enum.TryParse<TempUnit>(formPca.TempUnit, out var tempUnit);
                pca.Temperature = ConversionService.ConvertTemp(tempUnit, TempUnit.Fahrenheit, pca.Temperature);
            }
            
            if (pca.PcaId is 0)
            {
                using (var tran = new TransactionScope())
                {
                    pca.DateVitalsAdded = DateTime.Now;
                    _repository.AddPcaRecord(pca);

                    var vitalCommentTypeId = 0;
                    foreach (var note in formPca.VitalNotes.Where(n => !string.IsNullOrWhiteSpace(n)))
                    {
                        if (vitalCommentTypeId is 0)
                        {
                            var comType =
                                _repository.PcaCommentTypes.FirstOrDefault(t => t.PcaCommentTypeName == "Vitals");
                            if (comType is null) break;
                            vitalCommentTypeId = comType.PcaCommentTypeId;
                        }

                        _repository.AddPcaComment(new PcaComment
                        {
                            PcaId = pca.PcaId,
                            PcaCommentTypeId = vitalCommentTypeId,
                            Comment = note,
                            DateCommentAdded = DateTime.Now
                        });
                    }

                    var painScale = _repository.PainScaleTypes
                        .Include(ps => ps.PainParameters)
                        .ThenInclude(pp => pp.PainRatings)
                        .FirstOrDefault(ps => ps.PainScaleTypeId == pca.PainScaleTypeId);
                    if (painScale != null)
                    {
                        foreach (var (paramId, ratingId) in formPca.PainRatings)
                        {
                            if (painScale.PainParameters.Any(pp => pp.PainParameterId == paramId) && ratingId.HasValue)
                            {
                                _repository.AddPainAssessment(new PcaPainAssessment
                                {
                                    PcaId = pca.PcaId,
                                    PainParameterId = paramId,
                                    PainRatingId = (int) ratingId,
                                    LastModified = DateTime.Now
                                });
                            }
                        }
                    }

                    foreach (var (systemParamId, assessment) in formPca.Assessments.Where(a =>
                        a.Value.IsWithinNormalLimits != null))
                    {
                        assessment.PcaId = pca.PcaId;
                        assessment.CareSystemParameterId = (short) systemParamId;
                        assessment.LastModified = DateTime.Now;
                        _repository.AddSystemAssessment(assessment);
                    }

                    tran.Complete();
                }
            }
            else
            {
                //Update Pca and assessments
                //_repository.EditPcaRecord(pca);
            }

            // Return to assessment view
            return RedirectToAction("ViewAssessment",
                new {assessmentId = formPca.PcaRecord.PcaId});
        }

        private void AddUnites()
        {
            ViewBag.WeightUnits = new List<SelectListItem>
            {
                new SelectListItem("Kilograms", WeightUnit.Grams.ToString(), true),
                new SelectListItem("Grams", WeightUnit.Grams.ToString()),
                new SelectListItem("Pounds", WeightUnit.Pounds.ToString())
            };
            ViewBag.LengthUnits = new List<SelectListItem>
            {
                new SelectListItem("Inches", LengthUnit.Inches.ToString(), true),
                new SelectListItem("Feet", LengthUnit.Feet.ToString()),
                new SelectListItem("Centimeters", LengthUnit.Centimeters.ToString()),
                new SelectListItem("Meters", LengthUnit.Feet.ToString())
            };
            ViewBag.TempUnits = new List<SelectListItem>
            {
                new SelectListItem("Fahrenheit", TempUnit.Fahrenheit.ToString(), true),
                new SelectListItem("Celsius", TempUnit.Celsius.ToString())
            };
        }

        private void AddRoutes()
        {
            ViewBag.TempRoutes = new List<SelectListItem>(
                _repository.TempRouteTypes.ToList()
                    .Select((r, i) =>
                        new SelectListItem(
                            r.TempRouteTypeName,
                            r.TempRouteTypeId.ToString(),
                            i == 0)));
            ViewBag.O2DeliveryRoutes = new List<SelectListItem>(
                _repository.O2DeliveryTypes.ToList().Select((r, i) => new SelectListItem(
                    r.O2deliveryTypeName,
                    r.O2deliveryTypeId.ToString(),
                    i == 0)));
            ViewBag.BloodPressureRoutes = new List<SelectListItem>(
                _repository.BloodPressureRouteTypes.ToList()
                    .Select((r, i) =>
                        new SelectListItem(
                            r.Name,
                            r.BloodPressureRouteTypeId.ToString(),
                            i == 0)));
            ViewBag.PulseRoute = new List<SelectListItem>(
                _repository.PulseRouteTypes.ToList()
                    .Select((r, i) =>
                        new SelectListItem(
                            r.PulseRouteTypeName,
                            r.PulseRouteTypeId.ToString(),
                            i == 0)));
            ViewBag.BodyMassIndexRoutes = new List<SelectListItem>(
                _repository.BmiMethods.ToList()
                    .Select((r, i) =>
                        new SelectListItem(
                            r.Name,
                            r.BmiMethodId.ToString(),
                            i == 0)));

        }

    }
}