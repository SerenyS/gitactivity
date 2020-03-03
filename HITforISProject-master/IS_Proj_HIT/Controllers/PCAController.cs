using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using isprojectHiT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .FirstOrDefault(pca => pca.Pcaid == assessmentId);
            if (assessment is null)
                return RedirectToAction("Index", "Encounter",
                    new {filter = "CheckedIn"});

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
        public IActionResult CreateAssessment(long encounterId, string patientMrn)
        {
            var encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == encounterId);
            var patient = _repository.Patients.FirstOrDefault(p => p.Mrn == patientMrn);
            if (encounter is null || patient is null)
                return RedirectToAction("ViewEncounter", "Encounter",
                    new {encounterId = encounterId, isPatientEncounter = false});

            ViewBag.Encounter = encounter;
            ViewBag.Patient = patient;
            return View(new AssessmentFormPageModel());
        }

        /// <summary>
        /// todo: Expand this method to display the form to update an existing PCA, parameters are tenative
        /// </summary>
        /// <param name="assessmentId">PCA Id for Db Lookup</param>
        public IActionResult UpdateAssessment(int assessmentId) => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAssessment(AssessmentFormPageModel formPca)
        {
            //Do whatever Asp.Net redirect back to form here, I don't remember it and I'm tired. :)
            if (!ModelState.IsValid)
                return View("CreateAssessment", formPca);

            var pca = formPca.ToPcaRecord();
            IList<CareSystemAssessment> assessments;
            if (pca.Pcaid is 0)
            {
                //Create if pca from form does not include an ID
                _repository.AddPcaRecord(pca);

                assessments = formPca.ToSystemAssessments(pca.Pcaid);
                _repository.AddAssessments(assessments);
            }
            else
            {
                //Update Pca and assessments
                _repository.EditPcaRecord(pca);

                assessments = _repository.SystemAssessments.Where(a => a.Pcaid == pca.Pcaid).ToList();

                var formAssessments = formPca.ToSystemAssessments();
                foreach (var current in assessments)
                {
                    var formVersion = formAssessments.FirstOrDefault(a =>
                        a.CareSystemAssessmentTypeId == current.CareSystemAssessmentTypeId);
                    if (formVersion is null) continue;
                    if (current.CareSystemComment == formVersion.CareSystemComment) continue;

                    //need to determine how to find Within Normal(Defined) limits
                    //WdlEx = model.Height != null && model.Height != 0,
                    current.CareSystemComment = formVersion.CareSystemComment;
                    current.LastModified = formVersion.LastModified;
                    current.WdlEx = formVersion.WdlEx;
                    _repository.EditAssessment(current);
                }
            }

            return RedirectToAction("ViewAssessment",
                new {assessmentId = pca.Pcaid});
        }
    }
}