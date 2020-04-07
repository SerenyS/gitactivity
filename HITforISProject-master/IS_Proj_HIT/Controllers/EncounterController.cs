using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IS_Proj_HIT.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT.Controllers
{
    public class EncounterController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;
        public int PageSize = 8;
        public EncounterController(IWCTCHealthSystemRepository repo) => _repository = repo;

        public ViewResult Index(string filter, int encounterPage = 1)
        {
            var patientEncounters = _repository.Encounters.Join(_repository.Patients,
                                                                encounter => encounter.Mrn,
                                                                patient => patient.Mrn,
                                                                (encounter, patient) => new
                                                                {
                                                                    Mrn = encounter.Mrn,
                                                                    EncounterId = encounter.EncounterId,
                                                                    AdmitDateTime = encounter.AdmitDateTime,
                                                                    FirstName = patient.FirstName,
                                                                    LastName = patient.LastName,
                                                                    FacilityName = _repository.
                                                                                   Facilities.FirstOrDefault(
                                                                                       b => b.FacilityId == encounter.FacilityId).
                                                                                   Name,
                                                                    DischargeDateTime = encounter.DischargeDateTime,
                                                                    RoomNumber = encounter.RoomNumber
                                                                }).ToList();

            ViewBag.Filter = filter;
            if (filter == "CheckedIn")
                patientEncounters = patientEncounters.Where(e => e.DischargeDateTime == null).ToList();

            var viewPatientEncounters =
                patientEncounters.OrderByDescending(e => e.AdmitDateTime)
                                 .Select(e => new EncounterPatientViewModel(e.Mrn,
                                                                            e.EncounterId,
                                                                            e.AdmitDateTime,
                                                                            e.FirstName,
                                                                            e.LastName,
                                                                            e.FacilityName,
                                                                            e.DischargeDateTime.ToString(),
                                                                            e.RoomNumber));

            return View(viewPatientEncounters);
        }

        public ViewResult PatientEncounters(string patientMRN)
        {
            var patientEncounters = _repository.Encounters
                .Join(_repository.Patients,
                encounter => encounter.Mrn,
                patient => patient.Mrn,
                (encounter, patient) => new
                {
                    Mrn = encounter.Mrn,
                    EncounterId = encounter.EncounterId,
                    AdmitDateTime = encounter.AdmitDateTime,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    FacilityName = _repository.Facilities.FirstOrDefault(b => b.FacilityId == encounter.FacilityId).Name,
                    DischargeDateTime = ((encounter.DischargeDateTime == null) ? "Patient has not yet been discharged" : encounter.DischargeDateTime.ToString())

                })
                .Where(patientEncounterMRN => patientEncounterMRN.Mrn == patientMRN).ToList();
            List<EncounterPatientViewModel> viewPatientEncounters = new List<EncounterPatientViewModel>();
            for (int i = 0; i < patientEncounters.Count(); i++)
            {
                EncounterPatientViewModel thisPatientEncounter = new EncounterPatientViewModel(patientEncounters[i].Mrn,
                    patientEncounters[i].EncounterId, patientEncounters[i].AdmitDateTime,
                    patientEncounters[i].FirstName, patientEncounters[i].LastName, patientEncounters[i].FacilityName,
                    patientEncounters[i].DischargeDateTime);
                viewPatientEncounters.Add(thisPatientEncounter);
            }
            return View(viewPatientEncounters);
        }

        public IActionResult ViewEncounter(long encounterId, bool isPatientEncounter)
        {
            var encounter = _repository.Encounters
                                       .Include(e => e.Facility)
                                       .Include(e => e.Department)
                                       .Include(e => e.AdmitType)
                                       .Include(e => e.EncounterPhysicians)
                                       .Include(e => e.EncounterType)
                                       .Include(e => e.PlaceOfService)
                                       .Include(e => e.PointOfOrigin)
                                       .Include(e => e.DischargeDispositionNavigation)
                                       .Include(e => e.EncounterPhysicians)
                                       .Include(e => e.PcaRecords)
                                       .FirstOrDefault(b => b.EncounterId == encounterId);
            if (encounter is null)
                return RedirectToAction("Index", new { filter = isPatientEncounter ? "CheckedIn" : string.Empty });

            encounter.EncounterPhysicians = _repository.EncounterPhysicians
                                                       .Include(p => p.Physician)
                                                       .FirstOrDefault(p => p.EncounterPhysiciansId == encounter.EncounterPhysiciansId);

            var patient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(p => p.Mrn == encounter.Mrn);

            ViewBag.isPatientEncounter = isPatientEncounter;

            return View(new ViewEncounterPageModel
            {
                Encounter = encounter,
                Patient = patient
            });
        }

        // Deletes Encounter
        public IActionResult DeleteEncounter(long encounterId)
        {
            var encounter = _repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId);
            _repository.DeleteEncounter(encounter);
            return RedirectToAction("Index");
        }

        // Displays the Edit Encounter page
        public IActionResult EditEncounter(long encounterId, bool isPatientEncounter)
        {
            ViewBag.isPatientEncounter = isPatientEncounter;
            var encounter = _repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId);
            ViewBag.EncounterId = encounterId;
            ViewBag.AdmitDateTime = "" + encounter.AdmitDateTime.Year + "-" +
                ((encounter.AdmitDateTime.Month < 10) ? "0" + encounter.AdmitDateTime.Month.ToString() : encounter.AdmitDateTime.Month.ToString()) + "-" +
                ((encounter.AdmitDateTime.Day < 10) ? "0" + encounter.AdmitDateTime.Day.ToString() : encounter.AdmitDateTime.Day.ToString()) + "T" +
                ((encounter.AdmitDateTime.Hour < 10) ? "0" + encounter.AdmitDateTime.Hour.ToString() : encounter.AdmitDateTime.Hour.ToString()) + ":" +
                ((encounter.AdmitDateTime.Minute < 10) ? "0" + encounter.AdmitDateTime.Minute.ToString() : encounter.AdmitDateTime.Minute.ToString());
            ViewBag.EncounterMRN = encounter.Mrn;
            ViewBag.LastModified = DateTime.Today.AddYears(-1);
            ViewBag.Patient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == encounter.Mrn);
            
            //If you wanted to get the tool tips, you'd need to do this:
            //repository.AdmitTypes.FirstOrDefault(b => b.AdmitTypeId == id).Explaination
            //can also probably make these queries into a function if you can figure out how to make the respository types generic
            var queryAdmitTypes = _repository.AdmitTypes.Select(at => new { at.AdmitTypeId, at.Description });
            queryAdmitTypes = queryAdmitTypes.OrderBy(n => n.Description);
            ViewBag.AdmitTypes = new SelectList(queryAdmitTypes.AsEnumerable(), "AdmitTypeId", "Description", 0);

            var queryDepartments = _repository.Departments.Select(dep => new { dep.DepartmentId, dep.Name });
            queryDepartments = queryDepartments.OrderBy(n => n.Name);
            ViewBag.Departments = new SelectList(queryDepartments.AsEnumerable(), "DepartmentId", "Name", 0);

            var queryDischarges = _repository.Discharges.Select(dis => new { dis.DischargeId, dis.Name });
            queryDischarges = queryDischarges.OrderBy(n => n.Name);
            ViewBag.Discharges = new SelectList(queryDischarges.AsEnumerable(), "DischargeId", "Name", 0);

            var queryEncounterTypes = _repository.EncounterTypes.Select(ent => new { ent.EncounterTypeId, ent.Name });
            queryEncounterTypes = queryEncounterTypes.OrderBy(n => n.Name);
            ViewBag.EncounterTypes = new SelectList(queryEncounterTypes.AsEnumerable(), "EncounterTypeId", "Name", 0);

            var queryPlacesOfService = _repository.PlaceOfService.Select(pos => new { pos.PlaceOfServiceId, pos.Description });
            queryPlacesOfService = queryPlacesOfService.OrderBy(n => n.Description);
            ViewBag.PlacesOfService = new SelectList(queryPlacesOfService.AsEnumerable(), "PlaceOfServiceId", "Description", 0);

            var queryPointsOfOrigin = _repository.PointOfOrigin.Select(poo => new { poo.PointOfOriginId, poo.Description });
            queryPointsOfOrigin = queryPointsOfOrigin.OrderBy(n => n.Description);
            ViewBag.PointsOfOrigin = new SelectList(queryPointsOfOrigin.AsEnumerable(), "PointOfOriginId", "Description", 0);

            var queryFacility = _repository.Facilities.Select(fac => new { fac.FacilityId, fac.Name });
            queryFacility = queryFacility.OrderBy(n => n.Name);
            ViewBag.Facility = new SelectList(queryFacility.AsEnumerable(), "FacilityId", "Name", 0);

            var queryEncounterPhysicians = _repository.EncounterPhysicians.Select(EnP => new { EnP.EncounterPhysiciansId, Name = (_repository.Physicians.FirstOrDefault(b => b.PhysicianId == EnP.PhysicianId).FirstName + " " + _repository.Physicians.FirstOrDefault(b => b.PhysicianId == EnP.PhysicianId).LastName) });
            queryEncounterPhysicians = queryEncounterPhysicians.OrderBy(n => n.Name);
            ViewBag.EncounterPhysicians = new SelectList(queryEncounterPhysicians.AsEnumerable(), "EncounterPhysiciansId", "Name", 0);

            return View(encounter);
        }

        // Save edits to patient record from Edit Patients page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEncounter(Encounter model, string id)
        {
            if (!ModelState.IsValid) return View();

            model.LastModified = DateTime.Now;
            _repository.EditEncounter(model);
            return RedirectToAction("ViewEncounter",
                new {encounterId = model.EncounterId, isPatientEncounter = false});
        }

        // Save edits to patient record from Edit a specific Patients encounters page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPatientEncounter(Encounter model, string id)
        {
            if (ModelState.IsValid)
            {
                model.LastModified = DateTime.Now;
                _repository.EditEncounter(model);
                string myUrl = "/Encounter/PatientEncounters?patientMRN=" + model.Mrn;
                return Redirect(myUrl);
            }
            return View();


        }
    }
}
