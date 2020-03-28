using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models.Enum;
using IS_Proj_HIT.Models.PCA;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                .Include(pca => pca.CareSystemAssessment)
                .FirstOrDefault(pca => pca.PcaId == assessmentId);
            if (assessment is null)
                return RedirectToAction("Index", "Encounter",
                    new {filter = "CheckedIn"});

            var patientAlerts = _repository.PatientAlerts.Where(a => a.Mrn == assessment.Encounter.Mrn).Count();
            ViewBag.PatientAlertsCount = patientAlerts;

            var model = new CareAssessmentPageModel
            {
                Assessment = assessment,
                Encounter = assessment.Encounter,
                Patient = _repository.Patients.FirstOrDefault(p => p.Mrn == assessment.Encounter.Mrn)
            };
            return View(model);
        }

        /// <summary>
        /// todo: Expand this to display the form to enter a PCA, parameters tenative
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        /// <param name="patientMrn">Unique Identifier of patient</param>
        public IActionResult CreateAssessment(long encounterId, string patientMrn, AssessmentFormPageModel formPca = null)
        {
            var encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == encounterId);
            var patient = _repository.Patients.FirstOrDefault(p => p.Mrn == patientMrn);
            var patientAlerts = _repository.PatientAlerts.Where(b => b.Mrn == patientMrn).Count();

            if (encounter is null || patient is null)
                return RedirectToAction("ViewEncounter", "Encounter",
                    new {encounterId = encounterId, isPatientEncounter = false});

            ViewBag.Encounter = encounter;
            ViewBag.Patient = patient;
            ViewBag.PatientAlertsCount = patientAlerts;

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
            ViewBag.BpLocation = new List<SelectListItem>
            {
                new SelectListItem("Left", BpLocation.Left.ToString(), true),
                new SelectListItem("Right", BpLocation.Right.ToString())
            };
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

            var pca = _repository.PcaRecords.FirstOrDefault();
            var painratings = _repository.PainRatings.ToList();

            return View(formPca ?? new AssessmentFormPageModel());
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


            var model = new AssessmentFormPageModel(assessment);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAssessment(AssessmentFormPageModel formPca)
        {
            // Get Patient Alerts for return view
            string patientMrn = formPca.PatientMrn.ToString("D6");
            var patientAlerts = _repository.PatientAlerts.Where(a => a.Mrn == patientMrn).Count();
            ViewBag.PatientAlertsCount = patientAlerts;

            //Do whatever Asp.Net redirect back to form here, I don't remember it and I'm tired. :)
            if (!ModelState.IsValid)
            {
                return View("CreateAssessment", formPca);
            }

            var pca = formPca.ToPcaRecord();
            List<CareSystemAssessment> assessments;
            if (pca.PcaId is 0)
            {
                //Create if pca from form does not include an ID
                _repository.AddPcaRecord(pca);

                assessments = formPca.ToSystemAssessments(pca.PcaId);
                assessments.ForEach(a =>
                {
                    a.LastModified = DateTime.Now;
                });
                _repository.AddSystemAssessments(assessments);
            }
            else
            {
                //Update Pca and assessments
                _repository.EditPcaRecord(pca);

                assessments = _repository.CareSystemAssessments.Where(a => a.PcaId == pca.PcaId).ToList();

                var formAssessments = formPca.ToSystemAssessments();
                foreach (var current in assessments)
                {
                    var formVersion = formAssessments.FirstOrDefault(a =>
                        a.CareSystemParameterId == current.CareSystemParameterId);
                    if (formVersion is null) continue;
                    if (current.Comment == formVersion.Comment) continue;

                    //need to determine how to find Within Normal(Defined) limits
                    //IsWithinNormalLimits = model.Height != null && model.Height != 0,
                    current.Comment = formVersion.Comment;
                    current.LastModified = formVersion.LastModified;
                    current.IsWithinNormalLimits = formVersion.IsWithinNormalLimits;
                    _repository.EditSystemAssessment(current);
                }
            }

            // Return to assessment view
            return RedirectToAction("ViewAssessment",
                new {assessmentId = pca.PcaId});
        }
    }
}