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
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT.Controllers
{
    [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar, HIT Clerk, Nursing Student, Read Only")]
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
                return RedirectToAction("CheckedIn", "Encounter",
                    new {filter = "CheckedIn"});

            ViewBag.Patient = _repository.Patients.Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == assessment.Encounter.Mrn);

            return View(assessment);
        }

        /// <summary>
        /// Display the form to enter a PCA
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        /// <param name="patientMrn">Unique Identifier of patient</param>
        [Authorize(Roles = "Administrator, Nursing Student, Nursing Faculty, HIT Faculty")]
        public IActionResult CreateAssessment(long encounterId, string mrn)
        {
            var encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == encounterId);
            var patient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(p => p.Mrn == mrn);

            if (encounter is null || patient is null)
                return RedirectToAction("ViewEncounter", "Encounter",
                    new { encounterId });

            ViewBag.Encounter = encounter;
            ViewBag.Patient = patient;

            var newFormPca = PrepareAssessmentFormPageModel();

            return View(newFormPca);
        }

        /// <summary>
        /// Post form of Create PCA. Check for errors. Trigger SaveAssessment if none.
        /// </summary>
        /// <param name="formPca">view model for PCA</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAssessment(AssessmentFormPageModel formPca)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Encounter = _repository.Encounters
                    .FirstOrDefault(e => e.EncounterId == formPca.PcaRecord.EncounterId);
                ViewBag.Patient = _repository.Patients
                    .Include(p => p.PatientAlerts)
                    .FirstOrDefault(p => p.Mrn == formPca.PatientMrn);

                formPca = PrepareAssessmentFormPageModel(formPca);
                return View(formPca);
            }

            return SaveAssessment(formPca);
        }

        /// <summary>
        /// Display the form to update an existing PCA
        /// </summary>
        /// <param name = "assessmentId" > PCA Id for Db Lookup</param>
        ///  <param name = "patientMrn" > Unique Identifier of patient</param>
        ///   <param name = "encounterId" > Unique Identifier of patient</param>

        public IActionResult UpdateAssessment(int assessmentId, string patientMRN, long encounterId)
        {
            var assessment = _repository.PcaRecords
                .Include(pca => pca.Encounter)
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

            var vitalID=_repository.PcaComments.Where(pca=>pca.PcaId == assessmentId && pca.PcaCommentTypeId==11).Select(c=>c.Comment).SingleOrDefault();

            string vitalsNotes = Convert.ToString(vitalID);

            var painID = _repository.PcaComments.Where(pa => pa.PcaId == assessmentId && pa.PcaCommentTypeId== 12).Select(c => c.Comment).SingleOrDefault();

            string painsNotes = Convert.ToString(painID);


            var patient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(p => p.Mrn == patientMRN);
                var encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == encounterId);
                var patientAlerts = _repository.PatientAlerts.Where(b => b.Mrn == patientMRN).Count();

                if (encounter is null || patient is null)
                    return RedirectToAction("ViewEncounter", "Encounter",
                        new { encounterId = encounterId, isPatientEncounter = false });


            AddTooltips();
            AddUnits();
            AddRoutes();
            var painScales = _repository.PainScaleTypes
                .Include(ps => ps.PainParameters)
                .ThenInclude(pp => pp.PainRatings)
                .ToList();
            var painRatings = new Dictionary<int, int?>();


            // Create a pain assessment record for each pain scale/parameter/rating
            foreach (var ps in painScales)
            {
                foreach (var pp in ps.PainParameters)
                {
                    foreach (var pr in pp.PainRatings)
                    {
                        // If a PainAssessment exists for this parameter/rating, mark the value not null.
                        var existingPainAssessment = assessment.PainAssessment.FirstOrDefault(pa => pa.PainParameterId == pr.PainParameterId &&
                          pa.PainRatingId == pr.PainRatingId);
                        if (existingPainAssessment != null)
                        {
                            painRatings.Add(pr.PainRatingId, 1);
                        }
                        else
                        {
                            painRatings.Add(pr.PainRatingId, null);
                        }
                    }
                }
            }


            var secondarySystems = _repository.CareSystemAssessmentTypes
                .Include(cs => cs.CareSystemParameters)
                .ToList();
         

            var sysAssessments = new Dictionary<int, CareSystemAssessment>();
            
            var csaID = assessment.CareSystemAssessment.FirstOrDefault();


            foreach (var ss in secondarySystems)
            {
                foreach (var csp in ss.CareSystemParameters)
                {                 
                        
                        var existingCareAssessments = assessment.CareSystemAssessment.FirstOrDefault(ca => ca.CareSystemParameterId == csp.CareSystemParameterId &&
                          csp.CareSystemTypeId == ss.CareSystemTypeId);

                        if (existingCareAssessments != null)
                        {
                            sysAssessments.Add(csp.CareSystemParameterId,existingCareAssessments);
                        }
                        else
                        {
                            sysAssessments.Add(csp.CareSystemParameterId, new CareSystemAssessment
                            {
                                CareSystemParameterId = csp.CareSystemParameterId,
                                IsWithinNormalLimits = null
                            });
                        }
                    
                }
            }


            ViewBag.Encounter = encounter;
            ViewBag.Patient = patient;
            ViewBag.PcaRecord = assessment;
            var updatePCA = new AssessmentFormPageModel
            {
                PcaRecord = assessment,
                PainScales = painScales,
                PainRatings = painRatings,
                SecondarySystemTypes = secondarySystems,
                Assessments = sysAssessments,
                VitalNote = vitalsNotes,
                PainNote= painsNotes
            };

            return View(updatePCA);
        }

        /// <summary>
        /// Post form for updating existing PCA. Check for errors. Trigger SaveAssessment if none.
        /// </summary>
        /// <param name="formPca">view model for PCA</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
          public IActionResult UpdateAssessment(AssessmentFormPageModel formPca)
          {
            if (!ModelState.IsValid)
            {
                ViewBag.Encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == formPca.PcaRecord.EncounterId);
                ViewBag.Patient = _repository.Patients.FirstOrDefault(p => p.Mrn == formPca.PatientMrn);
                ViewBag.PatientAlertsCount = _repository.PatientAlerts.Count(b => b.Mrn == formPca.PatientMrn);
                return View(formPca);
            }

            return SaveAssessment(formPca);

          }
         
        /// <summary>
        /// Save the information added or updated in the PCA form. 
        /// Convert necessary units. Update date modified as needed.
        /// </summary>
        /// <param name="formPca">view model for PCA</param>
        /// <returns></returns>
        private IActionResult SaveAssessment(AssessmentFormPageModel formPca)
        {
            var pca = formPca.PcaRecord;
           
            
            //Convert temp to F, if entered in other unit
            if (pca.Temperature != null)
            {
                Enum.TryParse<TempUnit>(formPca.TempUnit, out var tempUnit);
                pca.Temperature = ConversionService.ConvertTemp(tempUnit, TempUnit.Fahrenheit, pca.Temperature);
            }

            if (pca.PcaId is 0)
            {
                using (var tran = new TransactionScope())
                {
                    pca.DateVitalsAdded = DateTime.Now;
                    pca.LastModified = DateTime.Now;
                    _repository.AddPcaRecord(pca);

                    if (!string.IsNullOrWhiteSpace(formPca.VitalNote))
                    {
                        var vNote = _repository.PcaCommentTypes
                            .FirstOrDefault(t => t.PcaCommentTypeName == "Vitals Notes");

                        _repository.AddPcaComment(new PcaComment
                        {
                            PcaId = pca.PcaId,
                            PcaCommentTypeId = vNote?.PcaCommentTypeId ?? 11,
                            Comment = formPca.VitalNote,
                            DateCommentAdded = DateTime.Now,
                            LastModified = DateTime.Now
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(formPca.VitalNote))
                    {
                        var vNote = _repository.PcaCommentTypes
                            .FirstOrDefault(t => t.PcaCommentTypeName == "Pain Assessment Notes");

                        _repository.AddPcaComment(new PcaComment
                        {
                            PcaId = pca.PcaId,
                            PcaCommentTypeId = vNote?.PcaCommentTypeId ?? 12,
                            Comment = formPca.PainNote,
                            DateCommentAdded = DateTime.Now,
                            LastModified = DateTime.Now
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
            } //UPDATE/////////////////////////////////
            else
            {
                using (var tran = new TransactionScope())
                {

                    var existingRecord = _repository.PcaRecords
                        .Include(pca => pca.PainAssessment)
                        .Include(pca => pca.PcaComment)
                        .Include(pca => pca.CareSystemAssessment)
                        .FirstOrDefault(pcr => pcr.PcaId == pca.PcaId);

                    existingRecord.Temperature = pca.Temperature;
                    existingRecord.HeadCircumference = pca.HeadCircumference;
                    existingRecord.HeadCircumferenceUnits = pca.HeadCircumferenceUnits;
                    existingRecord.Height = pca.Height;
                    existingRecord.HeightUnits = pca.HeightUnits;
                    existingRecord.LastModified = DateTime.Now;
                    existingRecord.O2deliveryTypeId = pca.O2deliveryTypeId;
                    existingRecord.OxygenFlow = pca.OxygenFlow;
                    existingRecord.PainLevelGoal = pca.PainLevelGoal;
                    existingRecord.PainScaleTypeId = pca.PainScaleTypeId;
                    existingRecord.PercentOxygenDelivered = pca.PercentOxygenDelivered;
                    existingRecord.Pulse = pca.Pulse;
                    existingRecord.PulseOximetry = pca.PulseOximetry;
                    existingRecord.PulseRouteTypeId = pca.PulseRouteTypeId;
                    existingRecord.Respiration = pca.Respiration;
                    existingRecord.SystolicBloodPressure = pca.SystolicBloodPressure;
                    existingRecord.DiastolicBloodPressure = pca.DiastolicBloodPressure;
                    existingRecord.TempRouteTypeId = pca.TempRouteTypeId;
                    existingRecord.Weight = pca.Weight;
                    existingRecord.WeightUnits = pca.WeightUnits;




                    var vitalComment = existingRecord.PcaComment.FirstOrDefault(c => c.PcaCommentTypeId == 11);
                    if(vitalComment == null)
                    {
                        if (!string.IsNullOrWhiteSpace(formPca.VitalNote))
                        {
                            var vNote = _repository.PcaCommentTypes
                                .FirstOrDefault(t => t.PcaCommentTypeName == "Vitals Notes");

                            _repository.AddPcaComment(new PcaComment
                            {
                                PcaId = pca.PcaId,
                                PcaCommentTypeId = vNote?.PcaCommentTypeId ?? 11,
                                Comment = formPca.VitalNote,
                                DateCommentAdded = DateTime.Now,
                                LastModified = DateTime.Now
                            });
                        }
                    }
                    else if(vitalComment!=null) {
                    vitalComment.Comment = formPca.VitalNote;
                    vitalComment.LastModified = DateTime.Now;
                    }


                    var painComment = existingRecord.PcaComment.FirstOrDefault(c => c.PcaCommentTypeId == 12);
                    if (vitalComment == null)
                    {
                        if (!string.IsNullOrWhiteSpace(formPca.PainNote))
                        {
                            var vNote = _repository.PcaCommentTypes
                                .FirstOrDefault(t => t.PcaCommentTypeName == "Pain Assessment Notes");

                            _repository.AddPcaComment(new PcaComment
                            {
                                PcaId = pca.PcaId,
                                PcaCommentTypeId = vNote?.PcaCommentTypeId ?? 12,
                                Comment = formPca.VitalNote,
                                DateCommentAdded = DateTime.Now,
                                LastModified = DateTime.Now
                            });
                        }
                    }
                    else if(painComment !=null)
                    {
                        painComment.Comment = formPca.PainNote;
                        painComment.LastModified = DateTime.Now;
                    }

                    //loop through caresystemassessments
                    foreach (var formCsa in formPca.Assessments)
                    {
                        var existCsa = existingRecord.CareSystemAssessment
                            .FirstOrDefault(a => a.CareSystemParameterId == formCsa.Key);

                        if (existCsa is null) //the assessment to update does not currently exist
                        {
                            if (formCsa.Value.IsWithinNormalLimits != null)
                            {
                                formCsa.Value.PcaId = pca.PcaId;
                                formCsa.Value.CareSystemParameterId = (short)formCsa.Key;
                                formCsa.Value.LastModified = DateTime.Now;

                                existingRecord.CareSystemAssessment.Add(formCsa.Value);
                            }
                        }
                        else //the assessment DOES exist and needs to be updated properly
                        {
                            existCsa.Comment = formCsa.Value.Comment;
                            existCsa.IsWithinNormalLimits = formCsa.Value.IsWithinNormalLimits;
                        }
                    }
                    
                    _repository.EditPcaRecord(existingRecord);

                    tran.Complete();
                }
            }

            // Return to assessment view
            return RedirectToAction("ViewAssessment",
                new { assessmentId = formPca.PcaRecord.PcaId });
        }
       

        /// <summary>
        /// Preprocessing to the PCA view model to load tooltips, units, and routes dropdowns.
        /// Also load pain scale tab and secondary systems/systems review tab.
        /// </summary>
        /// <param name="formPca"></param>
        /// <returns></returns>
        private AssessmentFormPageModel PrepareAssessmentFormPageModel(AssessmentFormPageModel formPca = null)
        {
            if (formPca is null)
                formPca = new AssessmentFormPageModel();

            AddTooltips();
            AddUnits();
            AddRoutes();

            formPca.PainScales = _repository.PainScaleTypes
                .Include(ps => ps.PainParameters)
                .ThenInclude(pp => pp.PainRatings)
                .ToList();
            formPca.PainScales.ForEach(ps =>
                ps.PainParameters.ToList().ForEach(pp =>
                    pp.PainRatings.ToList().ForEach(pr =>
                    {
                        if (!formPca.PainRatings.ContainsKey(pr.PainRatingId))
                            formPca.PainRatings.Add(pr.PainRatingId, null);
                    })));

            formPca.SecondarySystemTypes = _repository.CareSystemAssessmentTypes
                .Include(cs => cs.CareSystemParameters)
                .ToList();
            formPca.SecondarySystemTypes.ForEach(s =>
                s.CareSystemParameters.ToList().ForEach(sp =>
                {
                    if (!formPca.Assessments.ContainsKey(sp.CareSystemParameterId))
                        formPca.Assessments.Add(sp.CareSystemParameterId,
                            new CareSystemAssessment
                            {
                                CareSystemParameterId = sp.CareSystemParameterId,
                                IsWithinNormalLimits = null
                            });
                }));

            return formPca;
        }


        /// <summary>
        /// Populate the informational messages 
        /// </summary>
        private void AddTooltips()
        {
            ViewBag.TempWnl = "WNL: 97°F or higher AND  no higher than 101°F";
            ViewBag.BpWnl =
                "WNL: <br />Less than 120 / 80;<br />" +
                "ELEVATED: <br />120-129 / less than 80 <br />" +
                "STAGE 1 HIGH BP: <br />130-139 / 80-89 <br />" +
                "STAGE 1 HIGH BP: <br />140 and above<br /> / 90 and above <br />" +
                "HYPERTENSION CRISIS: <br />Higher than 180<br /> / higher than 120";
            ViewBag.PulseWnl = "WNL: 60 BPM or higher AND  no higher than 100 BPM";
            ViewBag.RespWnl = "WNL: 15/min or higher AND  no higher than 30/min";
            ViewBag.PulseOxWnl = "WNL: 90% or higher";
            ViewBag.WeightWnl =
                "Document whether the patient has experienced any unusual/unexplained weight loss or weight gain";
            ViewBag.FacesExplaination =
                "•	Explain to the person that each face represents a person who has no pain (hurt), or some, or a lot of pain. <br />" +
                "•	Face 0 doesn’t hurt at all. <br />" +
                "•	Face 2 hurts just a little bit. <br />" +
                "•	Face 4 hurts a little bit more. <br />" +
                "•	Face 6 hurts even more. <br />" +
                "•	Face 8 hurts a whole lot. <br />" +
                "•	Face 10 hurts as much as you can imagine although you don’t have to be crying to have this worst pain. <br />" +
                "•	Ask the person to choose the face that best depicts the pain they are experiencing.";
        }


        /// <summary>
        /// Populate the units dropdowns
        /// </summary>
        private void AddUnits()
        {
            ViewBag.WeightUnits = new List<SelectListItem>
            {
                new SelectListItem("Kilograms", WeightUnit.Kilograms.ToString(), true),
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


        /// <summary>
        /// Populate the routes dropdowns
        /// </summary>
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