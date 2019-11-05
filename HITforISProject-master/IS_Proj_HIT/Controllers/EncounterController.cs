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

        public ViewResult Index(int encounterPage = 1 /*, string searchString*/)

        {
            var encounters = (from e in repository.Encounters select e).ToList();
            List<String> encounterFirstnames = new List<string>();
            List<String> encounterLastnames = new List<string>();
            var patients = (from p in repository.Patients select p).ToList();
            for (int ii = 0; ii < encounters.Count(); ii++)
            {
                for (int i = 0; i < patients.Count(); i++)
                {
                    if (encounters[ii].Mrn == patients[i].Mrn)
                    {
                        encounterFirstnames.Add(patients.ElementAt(i).FirstName);
                        encounterLastnames.Add(patients.ElementAt(i).LastName);
                    }
                }
            }
            ViewData["FirstNames"] = encounterFirstnames;
            ViewData["LastNames"] = encounterLastnames;

            return View(encounters.ToList());
        }

        // Deletes Encounter
        public IActionResult DeleteEncounter(long encounterId)
        {
            var encounter = repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId);
            repository.DeleteEncounter(encounter);
            return RedirectToAction("Index");
        }


        public IActionResult AddEncounter(string id)
        {
            ViewBag.EncounterMRN = repository.Patients.FirstOrDefault(b => b.Mrn == id).Mrn;
            ViewBag.LastModified = DateTime.Today.AddYears(-1);
            ViewBag.PatientFirstName = repository.Patients.FirstOrDefault(b => b.Mrn == id).FirstName;
            ViewBag.PatientMiddleName = repository.Patients.FirstOrDefault(b => b.Mrn == id).MiddleName;
            ViewBag.PatientLastName = repository.Patients.FirstOrDefault(b => b.Mrn == id).LastName;
            ViewBag.PatientDob = repository.Patients.FirstOrDefault(b => b.Mrn == id).Dob;
            DateTime now = DateTime.Now;
            TimeSpan pAge = now.Subtract(ViewBag.PatientDob);
            if (pAge.Days > 365)
            {
                ViewBag.PatientAge = (pAge.Days / 365) + " Years";
            }
            else
            {
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
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEncounter(Encounter model)
        {
            if (ModelState.IsValid)
            {
                if (repository.Encounters.Any(p => p.EncounterId == model.EncounterId))
                {
                    ModelState.AddModelError("", "Encounter Id must be unique");
                }
                else
                {
                    model.LastModified = @DateTime.Now;
                    repository.AddEncounter(model);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // Displays the Edit Encounter page
        public IActionResult EditEncounter(string id)
        {
            ViewBag.EncounterId = repository.Encounters.FirstOrDefault(b => b.Mrn == id).EncounterId;
            ViewBag.AdmitDateTime = repository.Encounters.FirstOrDefault(b => b.Mrn == id).AdmitDateTime;
            ViewBag.EncounterMRN = repository.Patients.FirstOrDefault(b => b.Mrn == id).Mrn;
            ViewBag.LastModified = DateTime.Today.AddYears(-1);
            ViewBag.PatientFirstName = repository.Patients.FirstOrDefault(b => b.Mrn == id).FirstName;
            ViewBag.PatientMiddleName = repository.Patients.FirstOrDefault(b => b.Mrn == id).MiddleName;
            ViewBag.PatientLastName = repository.Patients.FirstOrDefault(b => b.Mrn == id).LastName;
            ViewBag.PatientDob = repository.Patients.FirstOrDefault(b => b.Mrn == id).Dob;
            DateTime now = DateTime.Now;
            TimeSpan pAge = now.Subtract(ViewBag.PatientDob);
            if (pAge.Days > 365)
            {
                ViewBag.PatientAge = (pAge.Days / 365) + " Years";
            }
            else
            {
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

            return View(repository.Encounters.FirstOrDefault(e => e.Mrn == id));

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
