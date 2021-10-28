using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT.Controllers
{

    [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar, HIT Clerk, Nursing Student, Read Only")]
    public class EncounterController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly WCTCHealthSystemContext _db;
        public int PageSize = 8;
        public EncounterController(IWCTCHealthSystemRepository repo, WCTCHealthSystemContext db) 
        {
            _repository = repo;
            _db = db;
        } 

        // Loads PCA Screen, Filters by facility
        // Used in: Navbar (_Layout) and Home Page, ViewDischarge, ViewEncounter (if patient is still checked in?)
        public ViewResult CheckedIn()
        {
            var currentUser = _repository.UserTables.FirstOrDefault(u => u.Email == User.Identity.Name);
            var currentUserFacility = _repository.UserFacilities.FirstOrDefault(e => e.UserId == currentUser.UserId);
            var facilities = _repository.Facilities;
            var secCheck = 0;

            var isAdmin = User.IsInRole("Administrator");
            
            var model = _repository.Encounters
                .Where(e => e.DischargeDateTime == null)
                .OrderByDescending(e => e.AdmitDateTime)
                .Join(_repository.Patients,
                    e => e.Mrn,
                    p => p.Mrn,
                    (e, p) =>
                        new EncounterPatientViewModel(e.Mrn,
                            e.EncounterId,
                            e.AdmitDateTime,
                            p.FirstName,
                            p.LastName,
                            e.Facility.Name,
                            e.DischargeDateTime.ToString(),
                            e.RoomNumber));

            if (!isAdmin && currentUserFacility == null) {
                model = _repository.Encounters
                .Where(e => e.DischargeDateTime == null && (e.FacilityId == 0))
                .OrderByDescending(e => e.AdmitDateTime)
                .Join(_repository.Patients,
                    e => e.Mrn,
                    p => p.Mrn,
                    (e, p) =>
                        new EncounterPatientViewModel(e.Mrn,
                            e.EncounterId,
                            e.AdmitDateTime,
                            p.FirstName,
                            p.LastName,
                            e.Facility.Name,
                            e.DischargeDateTime.ToString(),
                            e.RoomNumber));

                ViewBag.ErrorMessage = "You do not currently have an assigned facility.";
            }

            if (!isAdmin && currentUserFacility != null) {

                var currentFacilCheck = facilities.FirstOrDefault(p => p.Name == "WCTC Healthcare Center SECURE");
                
                if (currentUserFacility.FacilityId == currentFacilCheck.FacilityId) {
                    secCheck = facilities.FirstOrDefault(p => p.Name == "WCTC Healthcare Center SIM").FacilityId;
                }

                currentFacilCheck = facilities.FirstOrDefault(p => p.Name == "WCTC HC Nursing SECURE");
                
                if (currentUserFacility.FacilityId == currentFacilCheck.FacilityId) {
                    secCheck = facilities.FirstOrDefault(p => p.Name == "WCTC HC Nursing SIM").FacilityId;
                }

                currentFacilCheck = facilities.FirstOrDefault(p => p.Name == "WCTC HC MedAssist SECURE");

                if (currentUserFacility.FacilityId == currentFacilCheck.FacilityId) {
                    secCheck = facilities.FirstOrDefault(p => p.Name == "WCTC HC MedAssist SIM").FacilityId;
                }

                ViewBag.UserFacil = currentUserFacility.FacilityId;
                ViewBag.SecCheck = secCheck;

                model = _repository.Encounters
                .Where(e => e.DischargeDateTime == null && (e.FacilityId == currentUserFacility.FacilityId || e.FacilityId == secCheck))
                .OrderByDescending(e => e.AdmitDateTime)
                .Join(_repository.Patients,
                    e => e.Mrn,
                    p => p.Mrn,
                    (e, p) =>
                        new EncounterPatientViewModel(e.Mrn,
                            e.EncounterId,
                            e.AdmitDateTime,
                            p.FirstName,
                            p.LastName,
                            e.Facility.Name,
                            e.DischargeDateTime.ToString(),
                            e.RoomNumber));
            }

            return View(model);
        }

        // View ProgressNotes
        // Used in: PatientBanner
        // UNUSED SINCE PROGRESS NOTES ARE NOT CURRENTLY FUNCTIONAL
        public IActionResult ProgressNotes(long id){
            var desiredEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.FirstOrDefault(u => u.Mrn == desiredEncounter.Mrn);
            
            var desiredProgressNotes = _db.ProgressNotes.Where(u => u.EncounterId == id);
            
            ViewBag.EncounterId = id;

            ViewBag.Patient = _repository.Patients
            .Include(p => p.PatientAlerts)
            .FirstOrDefault(b => b.Mrn == desiredEncounter.Mrn);

             Patient patient = new Patient()
            {
                Mrn = desiredEncounter.Mrn,
                Dob = desiredPatient.Dob
                

            };

            Encounter encounter = new Encounter()
            {
                EncounterId = id
            };



            if(desiredProgressNotes.Any()){
                return View(desiredProgressNotes);
            }
            else{
                return View();
            }
        }

        // View Edit Progress Note
        // Used in: ProgressNotes
        // NO CURRENT FUNCTION
        public IActionResult EditProgressNotes(){

            return View();
        }

        // View Discharge 
        // Used in: EncounterMenu
        // May not currently work?
        public IActionResult ViewDischarge(long encounterId)
        {
            ViewData["ErrorMessage"] = "";

            var encounter = _repository.Encounters
                .Include(e => e.Facility)
                .Include(e => e.Department)
                .Include(e => e.AdmitType)
                .Include(e => e.EncounterPhysicians.Physician)
                .Include(e => e.EncounterType)
                .Include(e => e.PlaceOfService)
                .Include(e => e.PointOfOrigin)
                .Include(e => e.DischargeDispositionNavigation)
                .Include(e => e.Pcarecords)
                .FirstOrDefault(b => b.EncounterId == encounterId);
            if (encounter is null)
                return RedirectToAction("CheckedIn");

            var patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == encounter.Mrn);

            return View(new ViewDischargePageModel
            {
                Encounter = encounter,
                Patient = patient
            });
        }

        // View encounter page
        // Used in: PCAController, CheckedIn, EditEncounter (to return to view), HistoryAndPhysical (currently unused), PatientDetails, View/Create/UpdatePCAAssessment
        public IActionResult ViewEncounter(long encounterId)
        {
            ViewData["ErrorMessage"] = "";

            var id = User.Identity.Name;

            var encounter = _repository.Encounters
                .Include(e => e.Facility)
                .Include(e => e.Department)
                .Include(e => e.AdmitType)
                .Include(e => e.EncounterPhysicians.Physician)
                .Include(e => e.EncounterType)
                .Include(e => e.PlaceOfService)
                .Include(e => e.PointOfOrigin)
                .Include(e => e.DischargeDispositionNavigation)
                .Include(e => e.Pcarecords)
                .FirstOrDefault(b => b.EncounterId == encounterId);
            if (encounter is null)
                return RedirectToAction("CheckedIn");

            var patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == encounter.Mrn);

            return View(new ViewEncounterPageModel
            {
                Encounter = encounter,
                Patient   = patient
            });
        }

        // Displays add encounter page
        // Used in: PatientDetails
        [Authorize(Roles = "Administrator, Nursing Faculty, Registrar, HIT Faculty")]
        public IActionResult AddEncounter(string id)
        {
            ViewBag.Patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(b => b.Mrn == id);

            AddDropdowns();
            return View();
        }

        // Deletes Encounter, cannot delete if PCA records exist, redirects to PCA
        // Used in: CheckedIn, ViewEncounter
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteEncounter(long encounterId)
        {
            // Check for any PCAs created for this encounter
            bool usingExists = _repository.PcaRecords.Any(p => p.EncounterId == encounterId);
            if (usingExists)
            {
                Console.WriteLine("PCA records exist using this record.");
                ViewData["ErrorMessage"] = "PCA records exist using this record. Delete not available.";
                var model = _repository.Encounters
                    .Where(e => e.DischargeDateTime == null)
                    .OrderByDescending(e => e.AdmitDateTime)
                    .Join(_repository.Patients,
                        e => e.Mrn,
                        p => p.Mrn,
                        (e, p) =>
                            new EncounterPatientViewModel(e.Mrn,
                                e.EncounterId,
                                e.AdmitDateTime,
                                p.FirstName,
                                p.LastName,
                                e.Facility.Name,
                                e.DischargeDateTime.ToString(),
                                e.RoomNumber));

                return View("../Encounter/CheckedIn", model);
            }

            var encounter = _repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId);
            _repository.DeleteEncounter(encounter);
            return RedirectToAction("CheckedIn");
        }

        // Displays the Edit Encounter page
        // Used in: CheckedIn, ViewDischarge, ViewEncounter
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar")]
        public IActionResult EditEncounter(long encounterId)
        {
            var encounter = _repository.Encounters
                .FirstOrDefault(b => b.EncounterId == encounterId);
            ViewBag.Patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(b => b.Mrn == encounter.Mrn);

            AddDropdowns();

            return View(encounter);
        }

        // Create new encounter
        // Used in: AddEncounter
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public IActionResult AddEncounter(Encounter model)
        {
            if (_repository.Encounters.Any(p => p.EncounterId == model.EncounterId))
            {
                ModelState.AddModelError("", "Encounter Id must be unique");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Patient = _repository.Patients
                    .Include(p => p.PatientAlerts)
                    .FirstOrDefault(b => b.Mrn == model.Mrn);
                AddDropdowns();

                return View(model);
            }
            
            model.AdmitDateTime = DateTime.Now;
            model.LastModified = DateTime.Now;
            _repository.AddEncounter(model);

            return RedirectToAction("ViewEncounter", "Encounter", new {encounterId = model.EncounterId});
        }

        // Save edits to patient record from Edit Patients page
        // Used in: EditEncounter
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar")]
        public IActionResult EditEncounter(Encounter model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Patient = _repository.Patients
                    .Include(p => p.PatientAlerts)
                    .FirstOrDefault(b => b.Mrn == model.Mrn);

                AddDropdowns();

                return View(model);
            }

            model.LastModified = DateTime.Now;
            _repository.EditEncounter(model);
            return RedirectToAction("ViewEncounter",
                new {encounterId = model.EncounterId, allowCheckedInRedirect = true});
        }

        // add dropdowns to encounter views, only user's facility(ies) can be selected
        // Controller method to display dropdowns
        private void AddDropdowns()
        {
            var queryAdmitTypes = _repository.AdmitTypes
                .OrderBy(n => n.Description)
                .Select(n => new {n.AdmitTypeId, n.Description})
                .ToList();
            ViewBag.AdmitTypes = new SelectList(queryAdmitTypes, "AdmitTypeId", "Description", 0);

            var queryDepartments = _repository.Departments
                .OrderBy(n => n.Name)
                .Select(dep => new {dep.DepartmentId, dep.Name})
                .ToList();
            ViewBag.Departments = new SelectList(queryDepartments, "DepartmentId", "Name", 0);
            
            var queryDischarges = _repository.Discharges
                .OrderBy(n => n.Name)
                .Select(dis => new {dis.DischargeId, dis.Name})
                .ToList();
            ViewBag.Discharges = new SelectList(queryDischarges, "DischargeId", "Name", 0);

            var queryEncounterTypes = _repository.EncounterTypes
                .OrderBy(n => n.Name)
                .Select(ent => new {ent.EncounterTypeId, ent.Name})
                .ToList();
            ViewBag.EncounterTypes = new SelectList(queryEncounterTypes, "EncounterTypeId", "Name", 0);

            var queryPlacesOfService = _repository.PlaceOfService
                .OrderBy(n => n.Description)
                .Select(pos => new {pos.PlaceOfServiceId, pos.Description})
                .ToList();
            ViewBag.PlacesOfService = new SelectList(queryPlacesOfService, "PlaceOfServiceId", "Description", 0);

            var queryPointsOfOrigin = _repository.PointOfOrigin
                .OrderBy(n => n.Description)
                .Select(poo => new {poo.PointOfOriginId, poo.Description})
                .ToList();
            ViewBag.PointsOfOrigin = new SelectList(queryPointsOfOrigin, "PointOfOriginId", "Description", 0);

            var currentUser = _repository.UserTables.FirstOrDefault(u => u.Email == User.Identity.Name);
            var currentUserFacility = _repository.UserFacilities.FirstOrDefault(e => e.UserId == currentUser.UserId);
            var isAdmin = User.IsInRole("Administrator");
            var queryFacility = _repository.Facilities
                    .OrderBy(n => n.Name)
                    .Select(fac => new {fac.FacilityId, fac.Name})
                    .ToList();
            var facilities = _repository.Facilities;
            
            if (!isAdmin) {
                var secCheck = 0;
                // non admins may get an error if they don't have a facility
                
                var currentFacilCheck = facilities.FirstOrDefault(p => p.Name == "WCTC Healthcare Center SECURE");
                
                if (currentUserFacility.FacilityId == currentFacilCheck.FacilityId) {
                    secCheck = facilities.FirstOrDefault(p => p.Name == "WCTC Healthcare Center SIM").FacilityId;
                }

                currentFacilCheck = facilities.FirstOrDefault(p => p.Name == "WCTC HC Nursing SECURE");
                
                if (currentUserFacility.FacilityId == currentFacilCheck.FacilityId) {
                    secCheck = facilities.FirstOrDefault(p => p.Name == "WCTC HC Nursing SIM").FacilityId;
                }

                currentFacilCheck = facilities.FirstOrDefault(p => p.Name == "WCTC HC MedAssist SECURE");

                if (currentUserFacility.FacilityId == currentFacilCheck.FacilityId) {
                    secCheck = facilities.FirstOrDefault(p => p.Name == "WCTC HC MedAssist SIM").FacilityId;
                }

                queryFacility = _repository.Facilities
                    .Where(e => e.FacilityId == currentUserFacility.FacilityId && e.FacilityId == secCheck)
                    .OrderBy(n => n.Name)
                    .Select(fac => new {fac.FacilityId, fac.Name})
                    .ToList();
            }

            ViewBag.Facility = new SelectList(queryFacility, "FacilityId", "Name", 0);

            var queryEncounterPhysicians = _repository.EncounterPhysicians
                .OrderBy(n => n.Physician.LastName)
                .Select(p => new {p.EncounterPhysiciansId, Name = $"{p.Physician.FirstName} {p.Physician.LastName}"})
                .ToList();
            ViewBag.EncounterPhysicians = new SelectList(queryEncounterPhysicians, "EncounterPhysiciansId", "Name", 0);
        }


        // Displays History and Physical view? Appears to be left in progress
        // Used in: PatientBanner
        public ViewResult HistoryAndPhysical(long id)
        {
            var desiredPatientEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.FirstOrDefault(u => u.Mrn == desiredPatientEncounter.Mrn);
            
            
            ViewBag.EncounterId = id;

            ViewBag.Patient = _repository.Patients
            .Include(p => p.PatientAlerts)
            .FirstOrDefault(b => b.Mrn == desiredPatientEncounter.Mrn);



            Patient patient = new Patient()
            {
                Mrn = desiredPatientEncounter.Mrn,
                Dob = desiredPatient.Dob
                

            };

            Encounter encounter = new Encounter()
            {
                EncounterId = id
            };

            ViewEncounterPageModel model = new ViewEncounterPageModel()
            {
                Patient = patient,
                Encounter = encounter
                

            };

            

            return View(model);
        }

        // Add Physician Assessment
        // NO CURRENT FUNCTION 
        [Authorize(Roles = "Administrator, Nursing Faculty, Registrar, HIT Faculty")]
        public IActionResult AddPhysicianAssessment(string id)
        {
            return RedirectToAction();
        }

        // Add Physician Repotr
        // NO CURRENT FUNCTION
        public IActionResult AddPhysicianReport()
        {
            throw new NotImplementedException();
        }
    }
}