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
    [Authorize]
    public class EncounterController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;
        public int PageSize = 8;
        public EncounterController(IWCTCHealthSystemRepository repo) => _repository = repo;

        public ViewResult CheckedIn()
        {
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

            return View(model);
        }

        public IActionResult ViewEncounter(long encounterId)
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
                .Include(e => e.PcaRecords)
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

        [Authorize(Roles = "Administrator, Nursing Student")]
        public IActionResult AddEncounter(string id)
        {
            ViewBag.Patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(b => b.Mrn == id);

            AddDropdowns();
            return View();
        }

        // Deletes Encounter
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
        [Authorize(Roles = "Administrator, NursingStudent")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, NursingStudent")]
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Student")]
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

            var queryFacility = _repository.Facilities
                .OrderBy(n => n.Name)
                .Select(fac => new {fac.FacilityId, fac.Name})
                .ToList();
            ViewBag.Facility = new SelectList(queryFacility, "FacilityId", "Name", 0);

            var queryEncounterPhysicians = _repository.EncounterPhysicians
                .OrderBy(n => n.Physician.LastName)
                .Select(p => new {p.EncounterPhysiciansId, Name = $"{p.Physician.FirstName} {p.Physician.LastName}"})
                .ToList();
            ViewBag.EncounterPhysicians = new SelectList(queryEncounterPhysicians, "EncounterPhysiciansId", "Name", 0);
        }
    }
}