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
    public class PatientController : Controller
    {
        private IWCTCHealthSystemRepository repository;
        public int PageSize = 8;
        public PatientController(IWCTCHealthSystemRepository repo) => repository = repo;

        // Displays list of patients
        public ViewResult Index(string firstname, int patientPage = 1) => View(new ListPatientsViewModel
        {
            Patients = repository.Patients
                .Include(p => p.Religion)
                .Include(p => p.Gender)
                .Include(p => p.Ethnicity)
                .Include(p => p.MaritalStatus)
                .OrderBy(p => p.FirstName)
                .Skip((patientPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = patientPage,
                ItemsPerPage = PageSize,
                TotalItems = repository.Patients.Count()
            }
        });



        // Displays the Add Patient entry page
        public IActionResult AddPatient()
        {

            //Run stored procedure from SQL database to generate the MRN number
            using (var context = new WCTCHealthSystemContext())
            {
                var data = context.Patient.FromSql("EXECUTE dbo.GetNextMRN");
                ViewBag.MRN = data.FirstOrDefault().Mrn;

            }

            ViewBag.LastModified = DateTime.Today.AddYears(-1);


            // Do it this way if you need to have nothing selected as default
            var query = repository.Religions.Select(r => new { r.ReligionId, r.Name });
            ViewBag.Religions = new SelectList(query.AsEnumerable(), "ReligionId", "Name", 0);

            ViewBag.Religions = repository.Religions.Select(r =>
                                 new SelectListItem
                                 {
                                     Value = r.ReligionId.ToString(),
                                     Text = r.Name
                                 }).ToList();

            ViewBag.Sexes = repository.Sexes.Select(s =>
                                 new SelectListItem
                                 {
                                     Value = s.SexId.ToString(),
                                     Text = s.Name
                                 }).ToList();

            ViewBag.Gender = repository.Genders.Select(g =>
                                 new SelectListItem
                                 {
                                     Value = g.GenderId.ToString(),
                                     Text = g.Name
                                 }).ToList();

            ViewBag.Ethnicity = repository.Ethnicities.Select(e =>
                                 new SelectListItem
                                 {
                                     Value = e.EthnicityId.ToString(),
                                     Text = e.Name
                                 }).ToList();

            ViewBag.MaritalStatus = repository.MaritalStatuses.Select(m =>
                                 new SelectListItem
                                 {
                                     Value = m.MaritalStatusId.ToString(),
                                     Text = m.Name
                                 }).ToList();

            ViewBag.Employment = repository.Employments.Select(e =>
                                new SelectListItem
                                {
                                    Value = e.EmploymentId.ToString(),
                                    Text = (e.EmployerName + " - " + e.Occupation).ToString()
                                }).ToList();
            return View();

        }


        // Click Create button on Add Patient page adds new patient from Add Patient page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatient(Patient model)
        {
            //ViewBag.LastModified = DateTime.Today.AddYears(-1);
            if (ModelState.IsValid)
            {
                if (repository.Patients.Any(p => p.Mrn == model.Mrn))
                {
                    ModelState.AddModelError("", "MRN Id must be unique");
                }
                else
                {
                    model.LastModified = @DateTime.Now;
                    repository.AddPatient(model);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        // Deletes Patient
        public IActionResult DeletePatient(string id)
        {
            repository.DeletePatient(repository.Patients.FirstOrDefault(b => b.Mrn == id));
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
                    Debug.WriteLine("find me! " + Request.Form["Facility"]);
                    model.LastModified = @DateTime.Now;
                    Debug.WriteLine("MRN: " + model.Mrn);
                    Debug.WriteLine("Facility: " + model.FacilityId);
                    Debug.WriteLine("EncounterType: " + model.EncounterTypeId);
                    repository.AddEncounter(model);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }



        public IActionResult AddEmployment()
        {
            ViewBag.LastModified = DateTime.Today.AddYears(-1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployment(Employment model)
        {
            if (ModelState.IsValid)
            {
                if (repository.Employments.Any(e => e.EmploymentId == model.EmploymentId))
                {
                    ModelState.AddModelError("", "Employment ID must be unique");
                }
                else
                {
                    model.LastModified = @DateTime.Now;
                    repository.AddEmployment(model);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // Displays the Edit Patient page
        public IActionResult Edit(string id)
        {
            //ViewBag.ReligionID = repository.Patients.Include(p => p.Religion).FirstOrDefault(p => p.Mrn == id);
            ViewBag.MaritalID = repository.Patients.Include(p => p.MaritalStatus).FirstOrDefault(p => p.Mrn == id);
            ViewBag.SexID = repository.Patients.Include(p => p.Sex).FirstOrDefault(p => p.Mrn == id);
            ViewBag.GenderID = repository.Patients.Include(p => p.Gender).FirstOrDefault(p => p.Mrn == id);
            ViewBag.EthnicityID = repository.Patients.Include(p => p.Ethnicity).FirstOrDefault(p => p.Mrn == id);

            ViewBag.LastModified = DateTime.Today.AddYears(-1);

            //var query = repository.Religions.Select(r => new { r.ReligionId, r.Name });
            //ViewBag.Religions = new SelectList(query.AsEnumerable(), "ReligionId", "Name", 0);

            ViewBag.Religions = repository.Religions.Select(r =>
                                 new SelectListItem
                                 {
                                     Value = r.ReligionId.ToString(),
                                     Text = r.Name
                                 }).ToList();

            ViewBag.Sexes = repository.Sexes.Select(s =>
                                 new SelectListItem
                                 {
                                     Value = s.SexId.ToString(),
                                     Text = s.Name
                                 }).ToList();

            ViewBag.Gender = repository.Genders.Select(g =>
                                 new SelectListItem
                                 {
                                     Value = g.GenderId.ToString(),
                                     Text = g.Name
                                 }).ToList();

            ViewBag.Ethnicity = repository.Ethnicities.Select(e =>
                                 new SelectListItem
                                 {
                                     Value = e.EthnicityId.ToString(),
                                     Text = e.Name
                                 }).ToList();

            ViewBag.MaritalStatus = repository.MaritalStatuses.Select(m =>
                                 new SelectListItem
                                 {
                                     Value = m.MaritalStatusId.ToString(),
                                     Text = m.Name
                                 }).ToList();

            ViewBag.Employment = repository.Employments.Select(e =>
                                new SelectListItem
                                {
                                    Value = e.EmploymentId.ToString(),
                                    Text = (e.EmployerName + " - " + e.Occupation).ToString()
                                }).ToList();

            return View(repository.Patients.FirstOrDefault(p => p.Mrn == id));

        }

        // Save edits to patient record from Edit Patients page
        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient model, string id)
        {
            if (ModelState.IsValid)
            {
                model.LastModified = @DateTime.Now;
                repository.EditPatient(model);
                // I added the string id to the IActionResult above to pass in the model's Mrn
                // Then I constructed the redirect URL pointing to the Details page/id
                string myUrl = "Details/" + model.Mrn;
                return Redirect(myUrl);
            }
            return View();


        }


        // Pick record to send to Details page
        public IActionResult Details(string id)
        {
            Console.WriteLine("Trying to save(PatientController)"); //Look here
            ViewBag.ReligionID = repository.Patients.Include(p => p.Religion).FirstOrDefault(p => p.Mrn == id);
            ViewBag.MaritalID = repository.Patients.Include(p => p.MaritalStatus).FirstOrDefault(p => p.Mrn == id);
            ViewBag.SexID = repository.Patients.Include(p => p.Sex).FirstOrDefault(p => p.Mrn == id);
            ViewBag.GenderID = repository.Patients.Include(p => p.Gender).FirstOrDefault(p => p.Mrn == id);
            ViewBag.EthnicityID = repository.Patients.Include(p => p.Ethnicity).FirstOrDefault(p => p.Mrn == id);

            return View(repository.Patients.FirstOrDefault(p => p.Mrn == id));

        }

        //List Alerts for the currently selected MRN
        public IActionResult ListAlerts(string id)
        {

            ViewBag.myMrn = id;
            return View(new ListAlertsViewModel
            {
                PatientAlerts = repository.PatientAlerts
                //.Include(p => p.PatientRestrictions)
                //.Include(p => p.PatientAdvancedDirectives)
                .Include(p => p.AlertType)
                //.Include(p => p.PatientAllergy)
                //.Include(p => p.FallRisk)
                .Where(p => p.Mrn == id)
            });
        }

        public RedirectToRouteResult BackToDetails(string id) =>
        RedirectToRoute(new
        {
            controller = "Patient",
            action = "Details",
            ID = id
        });


        //Open AddAlert pop-up window
        // Commented out
        public IActionResult DisplayAddAlert(string id)
        {
            ViewBag.myMrn = id;
            ViewBag.LastModified = DateTime.Today;
            //ViewBag.AlertType = repository.PatientAlerts.Include(p => p.AlertType).FirstOrDefault(p=>p.Mrn == id);
            if (repository.PatientAlerts.FirstOrDefault(b => b.Mrn == id) == null)
            {
                ViewBag.MRN = id;
            }
            else
            {
                ViewBag.MRN = repository.PatientAlerts.FirstOrDefault(b => b.Mrn == id).Mrn;
            }

            ViewBag.LastModified = @DateTime.Now;

            ViewBag.AlertType = repository.AlertTypes.Select(a =>
                    new SelectListItem
                    {
                        Value = a.AlertId.ToString(),
                        Text = a.Name
                    }).ToList();

            ViewBag.Restriction = repository.Restrictions.Include(r=>r.PatientRestrictions).Select(r =>
                      new SelectListItem
                      {
                          Value = r.RestrictionId.ToString(),
                          Text = r.Name
                      }).ToList();

            //var query = repository.FallRisk.Select(r => new { r.FallRiskId, r.FallRisk.Name });
            //ViewBag.PatientFallRisk = new SelectList(query.AsEnumerable(), "FallRiskId", "Name", 0);
            ViewBag.PatientFallRisk = repository.FallRisks.Include(r => r.PatientFallRisks).Select(r =>
                   new SelectListItem
                   {
                       Value = r.FallRiskId.ToString(),
                       Text = r.Name
                   }).ToList();



            ViewBag.Allergens = repository.Allergens.Include(r => r.PatientAllergy)
            .Select(r =>
               new SelectListItem
               {
                   Value = r.AllergenId.ToString(),
                   Text = r.AllergenName
               }).ToList();



            ViewBag.Reactions = repository.Reactions.Include(r => r.PatientAllergy).Select(r =>
                  new SelectListItem
                  {
                      Value = r.ReactionId.ToString(),
                      Text = r.Name
                  }).ToList();

            return View();

        }

        

        [HttpPost]
        [ActionName("AddAlert")]
        [ValidateAntiForgeryToken]
        public IActionResult AddAlert(AlertsViewModel model)
        {
            Console.WriteLine("Trying to save(PatientController)");
            //ViewBag.LastModified = DateTime.Today.AddYears(-1);
            if (ModelState.IsValid)
            {
                ViewBag.AlertType = repository.PatientAlerts.Include(p => p.AlertType);
                ViewBag.FallRiskID = repository.PatientAlerts.Include(p=> p.PatientFallRisks);
                ViewBag.LastModified = @DateTime.Now;
                

                ViewBag.AlertType = repository.AlertTypes.Select(a =>
                        new SelectListItem
                        {
                            Value = a.AlertId.ToString(),
                            Text = a.Name
                        }).ToList();
                

                ViewBag.Restriction = repository.Restrictions.Include(r => r.PatientRestrictions).Select(r =>
                        new SelectListItem
                        {
                            Value = r.RestrictionId.ToString(),
                            Text = r.Name
                        }).ToList();

                //var query = repository.FallRisk.Select(r => new { r.FallRiskId, r.Description });
                //ViewBag.PatientFallRisk = new SelectList(query.AsEnumerable(), "FallRiskId", "Description", 0);
                ViewBag.PatientFallRisk = repository.FallRisks.Include(r => r.PatientFallRisks).Select(r =>
                   new SelectListItem
                   {
                       Value = r.FallRiskId.ToString(),
                       Text = r.Name
                   }).ToList();



                repository.AddAlert(model);
                string myUrl = "ListAlerts/" + model.Mrn;
                return Redirect(myUrl);

            }
            return View();
        }
    }

}
