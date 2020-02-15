using System.Linq;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using isprojectHiT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT.Controllers
{
    public class PCAController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;

        public PCAController(IWCTCHealthSystemRepository repository) => _repository = repository;

        public IActionResult Index() => View();

        /// <summary>
        ///     Show a detailed view of a single PCA
        /// </summary>
        /// <param name="assessmentId">PCA Id for Db Lookup</param>
        public IActionResult ViewAssessment(int assessmentId)
        {
            var assessment = _repository.PcaRecords
                .Include(pca => pca.Encounter)
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
            var pca = new CareAssessmentPageModel
            {
                Assessment = new Pcarecord(),
                Encounter = _repository.Encounters.FirstOrDefault(e => e.EncounterId == encounterId),
                Patient = _repository.Patients.FirstOrDefault(p => p.Mrn == patientMrn)
            };
            if (pca.Encounter is null || pca.Patient is null)
                return RedirectToAction("ViewEncounter", "Encounter",
                    new {encounterId = encounterId, isPatientEncounter = false});

            return View(pca);
        }

        /// <summary>
        /// todo: Expand this method to display the form to update an existing PCA, parameters are tenative
        /// </summary>
        /// <param name="assessmentId">PCA Id for Db Lookup</param>
        public IActionResult UpdateAssessment(int assessmentId) => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAssessment(CareAssessmentPageModel pca)
        {
            //Todo: Mark notes, need to confirm if accurate
            //do a database save here
            //save for the Pcarecord, Encounter (if anything changes there) and Patient (if anything changes there)
            //DO NOT make unnecessary changes to Encounter/Patient if it's data that's stored on the PCA
            //Maybe some validation before it saves
            //And then check to see if it failed to save correctly, if it did then return to the edit form
            //otherwise, send user back to the encounter page?
            return RedirectToAction("ViewEncounter", "Encounter",
                new {encounterId = pca.Encounter.EncounterId, isPatientEncounter = false});
        }
    }
}