using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.ViewModels;
using isprojectHiT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using IS_Proj_HIT.Extensions.Alerts;
using IS_Proj_HIT.ViewModels;
using System.Diagnostics;

namespace IS_Proj_HIT.Controllers
{
    public class EncounterController : Controller
    {
        private IWCTCHealthSystemRepository repository;
        public int PageSize = 8;
        public EncounterController(IWCTCHealthSystemRepository repo) => repository = repo;

        public ViewResult Index(string filter, int encounterPage = 1)

        {
            var patientEncounters = repository.Encounters
                .Join(repository.Patients,
                encounter => encounter.Mrn,
                patient => patient.Mrn,
                (encounter, patient) => new
                {
                    Mrn = encounter.Mrn,
                    EncounterId = encounter.EncounterId,
                    AdmitDateTime = encounter.AdmitDateTime,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    FacilityName = repository.Facilities.FirstOrDefault(b => b.FacilityId == encounter.FacilityId).Name,
                    DischargeDateTime = ((encounter.DischargeDate == null) || (encounter.DischargeTime == null)) ? "Patient has not yet been discharged" : "" + encounter.DischargeDate + " " + encounter.DischargeTime

                }).ToList();

            if (filter == "CheckedIn")
            {
                var tempList = patientEncounters;
                foreach (var e in patientEncounters.ToList())
                {
                    if (e.DischargeDateTime != "Patient has not yet been discharged")
                    {
                        tempList.Remove(e);
                    }
                }
                patientEncounters = tempList;
            }

            List<EncounterPatientViewModel> viewPatientEncounters = new List<EncounterPatientViewModel>();
            for(int i = 0; i < patientEncounters.Count(); i++)
            {
                EncounterPatientViewModel thisPatientEncounter = new EncounterPatientViewModel(patientEncounters[i].Mrn,
                    patientEncounters[i].EncounterId, patientEncounters[i].AdmitDateTime,
                    patientEncounters[i].FirstName, patientEncounters[i].LastName, patientEncounters[i].FacilityName,
                    patientEncounters[i].DischargeDateTime);
                viewPatientEncounters.Add(thisPatientEncounter);
            }
            return View(viewPatientEncounters);
        }

        // Deletes Encounter
        public IActionResult DeleteEncounter(long encounterId)
        {
            var encounter = repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId);
            repository.DeleteEncounter(encounter);
            return RedirectToAction("Index");
        }


        // Displays the Edit Encounter page
        public IActionResult EditEncounter(long encounterId)
        {
            ViewBag.EncounterId = repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId).EncounterId;
            ViewBag.AdmitDateTime = repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId).AdmitDateTime;
            ViewBag.EncounterMRN = repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId).Mrn;
            string encounterMrn = ViewBag.EncounterMRN;
            ViewBag.LastModified = DateTime.Today.AddYears(-1);
            ViewBag.PatientFirstName = repository.Patients.FirstOrDefault(b => b.Mrn == encounterMrn).FirstName;
            ViewBag.PatientMiddleName = repository.Patients.FirstOrDefault(b => b.Mrn == encounterMrn).MiddleName;
            ViewBag.PatientLastName = repository.Patients.FirstOrDefault(b => b.Mrn == encounterMrn).LastName;
            ViewBag.PatientDob = repository.Patients.FirstOrDefault(b => b.Mrn == encounterMrn).Dob;
            DateTime now = DateTime.Now;
            TimeSpan pAge = now.Subtract(ViewBag.PatientDob);
            if (pAge.Days > 365)
            {
                ViewBag.CurrentPatientAge = (byte)(pAge.Days / 365);
                ViewBag.PatientAge = ViewBag.CurrentPatientAge + " Years";
            }
            else
            {
                ViewBag.CurrentPatientAge = 0;
                ViewBag.PatientAge = pAge.Days + " Days";
            }
            //If you wanted to get the tool tips, you'd need to do this:
            //repository.AdmitTypes.FirstOrDefault(b => b.AdmitTypeId == id).Explaination
            ViewBag.AdmitTypes = repository.AdmitTypes.Select(at =>
                                new SelectListItem
                                {
                                    Value = at.AdmitTypeId.ToString(),
                                    Text = at.Description,

                                }).ToList();
            ViewBag.Departments = repository.Departments.Select(dep =>
                                new SelectListItem
                                {
                                    Value = dep.DepartmentId.ToString(),
                                    Text = dep.Name,

                                }).ToList();
            ViewBag.Discharges = repository.Discharges.Select(dis =>
                                new SelectListItem
                                {
                                    Value = dis.DischargeId.ToString(),
                                    Text = dis.Name,

                                }).ToList();
            ViewBag.EncounterTypes = repository.EncounterTypes.Select(ent =>
                               new SelectListItem
                               {
                                   Value = ent.EncounterTypeId.ToString(),
                                   Text = ent.Name,

                               }).ToList();
            ViewBag.PlacesOfService = repository.PlaceOfService.Select(pos =>
                                new SelectListItem
                                {
                                    Value = pos.PlaceOfServiceId.ToString(),
                                    Text = pos.Description,

                                }).ToList();
            ViewBag.PointsOfOrigin = repository.PointOfOrigin.Select(poo =>
                                new SelectListItem
                                {
                                    Value = poo.PointOfOriginId.ToString(),
                                    Text = poo.Description,

                                }).ToList();
            ViewBag.Facility = repository.Facilities.Select(fac =>
                                new SelectListItem
                                {
                                    Value = fac.FacilityId.ToString(),
                                    Text = fac.Name,

                                }).ToList();
            ViewBag.EncounterPhysicians = repository.EncounterPhysicians.Select(EnP =>
                               new SelectListItem
                               {
                                   Value = EnP.EncounterPhysiciansId.ToString(),
                                   Text = (repository.Physicians.FirstOrDefault(b => b.PhysicianId == EnP.PhysicianId).FirstName + " " + repository.Physicians.FirstOrDefault(b => b.PhysicianId == EnP.PhysicianId).LastName),

                               }).ToList();

            return View(repository.Encounters.FirstOrDefault(e => e.EncounterId == encounterId));
        }

        // Save edits to patient record from Edit Patients page
        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult EditEncounter(Encounter model, string id)
        {
            if (ModelState.IsValid)
            {
                model.LastModified = @DateTime.Now;
                repository.EditEncounter(model);
                Debug.WriteLine("find me! " + Request);
                // string myUrl = "Details/" + model.Mrn;
                return Redirect("/Encounter");
            }
            return View();


        }
    }

}
