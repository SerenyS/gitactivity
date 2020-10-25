using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models.ViewModels;
using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT.Controllers
{

    public class PatientController : Controller
    {
        private IWCTCHealthSystemRepository repository;
        public int PageSize = 8;
        public PatientController(IWCTCHealthSystemRepository repo) => repository = repo;

        // Displays list of patients
        public ActionResult Index(string searchLast, string searchFirst, string searchSSN,
            string searchMRN, DateTime searchDOB, DateTime searchDOBBefore, string sortOrder)
        {
            // Put in a wildcard if user didn't search on these fields
            searchLast ??= " ";
            searchFirst ??= " ";
            searchSSN ??= " ";
            searchMRN ??= " ";

            if (searchDOB.GetHashCode() == 0)
            {
                searchDOB = new DateTime(1898, 1, 1);
            }

            if (searchDOBBefore.GetHashCode() == 0)
            {
                searchDOBBefore = new DateTime(2030, 1, 1);
            }

            var patients = repository.Patients
                .Include(p => p.Religion)
                .Include(p => p.Gender)
                .Include(p => p.Ethnicity)
                .Include(p => p.MaritalStatus)
                .Where(p => p.LastName.Contains(searchLast)
                            && p.FirstName.Contains(searchFirst)
                            && p.Ssn.Contains(searchSSN)
                            && p.Mrn.Contains(searchMRN)
                            && p.Dob >= searchDOB
                            && p.Dob <= searchDOBBefore);

            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MrnSortParm = sortOrder == "mrn" ? "mrn_desc" : "mrn";
            ViewBag.DobSortParm = sortOrder == "dob" ? "dob_desc" : "dob";

            ViewBag.sortOrder = sortOrder;
            ViewBag.searchLast = searchLast;
            ViewBag.searchFirst = searchFirst;
            ViewBag.searchSSN = searchSSN;
            ViewBag.searchDOB = searchDOB;
            ViewBag.searchDOBBefore = searchDOBBefore;

            switch (sortOrder)
            {
                case "mrn":
                    patients = patients.OrderBy(p => p.Mrn);
                    break;
                case "mrn_desc":
                    patients = patients.OrderByDescending(p => p.Mrn);
                    break;
                case "dob":
                    patients = patients.OrderBy(p => p.Dob);
                    break;
                case "dob_desc":
                    patients = patients.OrderByDescending(p => p.Dob);
                    break;
                case "name_desc":
                    patients = patients.OrderByDescending(p => p.LastName);
                    break;
                default:
                    patients = patients.OrderBy(p => p.LastName);
                    ViewBag.sortOrder = "name";
                    break;
            }

            return View(new ListPatientsViewModel
            {
                Patients = patients
            });
        }

        
        public IActionResult PatientSearch() => View();

        // Displays the Add Patient entry page
        [Authorize(Roles = "Administrator, Nursing Faculty")]
        public IActionResult AddPatient()
        {
            //Run stored procedure from SQL database to generate the MRN number
            using (var context = new WCTCHealthSystemContext())
            {
                var data = context.Patient.FromSql("EXECUTE dbo.GetNextMRN");
                ViewBag.MRN = data.FirstOrDefault()?.Mrn;
              
            }

            
           

            AddDropdowns();

            //TODO: ADD EMPLOYMENT ENTRY TO THE CREATE/EDIT PATIENT FLOW
            //var queryEmployment = repository.Employments.Select(r => new { r.EmploymentId, r.Occupation });
            //ViewBag.Employment = new SelectList(queryEmployment.AsEnumerable(), "MaritalStatusId", "Name", 0);
            //ViewBag.Employment = repository.Employments.Select(e =>
            //                    new SelectListItem
            //                    {
            //                        Value = e.EmploymentId.ToString(),
            //                        Text = (e.EmployerName + " - " + e.Occupation).ToString()
            //                    }).ToList();
            return View();
        }

        // Click Create button on Add Patient page adds new patient from Add Patient page
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator, Nursing Faculty")]
        public IActionResult AddPatient(Patient model)
        {
            model.LastModified = @DateTime.Now;
            if (ModelState.IsValid)
            {
                if (repository.Patients.Any(p => p.Mrn == model.Mrn))
                {
                    ModelState.AddModelError("", "MRN Id must be unique");
                }
                else
                {
                    var languages = Request.Form["language"];
                    var languageId = short.Parse(languages[0]);
                    var query = repository.Languages.Where(lang => lang.LanguageId == languageId).ToList();

                    if (query.Any() && query != null)
                    {
                        model.PatientLanguage.Add(
                         new PatientLanguage
                         {
                             LanguageId = query[0].LanguageId,
                             Mrn = model.Mrn,
                             IsPrimary = 1,
                             LastModified = DateTime.Now

                         });
                    }

                    var races = Request.Form["race"];
                    var raceId = short.Parse(races[0]);
                    var selectedRace = repository.Races.FirstOrDefault(race => race.RaceId == raceId);
                    if (selectedRace != null)
                    {
                        model.PatientRace.Add(
                           new PatientRace
                           {
                               RaceId = selectedRace.RaceId,
                               Mrn = model.Mrn,
                               LastModified = DateTime.Now

                           });
                        
                    }
                    
                    repository.AddPatient(model);
                    TempData["msg"] = "A new patient was successfully created.";
                    string myUrl = "Details/" + model.Mrn;
                    return Redirect(myUrl);
                    //return RedirectToAction("Index");
                }
            }

            return View();
        }

        // Deletes Patient
        [Authorize(Roles = "Administrator")]

        public IActionResult DeletePatient(string id)
        {
            ViewBag.PatientAlertExists = repository.PatientAlerts.FirstOrDefault(b => b.Mrn == id);
            if (ViewBag.PatientAlertExists != null)
            {
                TempData["msg1"] = "You cannot delete a patient with patient alerts.";
                return RedirectToRoute(new
                {
                    controller = "Patient",
                    action = "Details",
                    ID = id
                });
            }
            else
            {
                TempData["msg1"] = "The selected patient was deleted.";
                repository.DeletePatient(repository.Patients.FirstOrDefault(b => b.Mrn == id));
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });
            }

            //return RedirectToAction("Index", "Home");
        }

        // Displays the Edit Patient page
        [Authorize(Roles = "Administrator, Nursing Student, Nursing Faculty")]
        public IActionResult Edit(string id)
        {
            var model = repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == id);

            AddDropdowns(model);

            return View(model);
        }

        // Save edits to patient record from Edit Patients page
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Nursing Faculty")] // Nursing Student, ? Don't rememebr if student was supposed to be able to edit the patient.
        public IActionResult Edit(Patient model)
        {
            if (!ModelState.IsValid) return View(model.Mrn);

            model.LastModified = DateTime.Now;

            if (repository.PatientLanguages.Any(l => l.IsPrimary == 1 && l.Mrn == model.Mrn))
            {
                var languageToChange = repository.PatientLanguages.FirstOrDefault(pl => pl.Mrn == model.Mrn && pl.IsPrimary == 1);
                languageToChange.IsPrimary = 0;
            }
            var languages = Request.Form["language"];
            var languageId = short.Parse(languages[0]);
            var query = repository.Languages.Where(lang => lang.LanguageId == languageId).ToList();

            if (query.Any() && query != null)
            {

                repository.AddPatientLanguage(
                 new PatientLanguage
                 {
                     LanguageId = query[0].LanguageId,
                     Mrn = model.Mrn,
                     IsPrimary = 1,
                     LastModified = DateTime.Now

                 });
            }
            repository.EditPatient(model);
            return RedirectToAction("Details", new {id = model.Mrn});
        }

        // Pick record to send to Details page
        public IActionResult Details(string id)
        {
            var model = repository.Patients
                .Include(p => p.PatientAlerts)
                .Include(p => p.Religion)
                .Include(p => p.MaritalStatus)
                .Include(p => p.Sex)
                .Include(p => p.Gender)
                .Include(p => p.Ethnicity)
                .Include(p => p.Encounter).ThenInclude(e => e.Facility)
                .FirstOrDefault(p => p.Mrn == id);

            var primaryLanguageQuery = repository.PatientLanguages.Where(l => l.Mrn == model.Mrn)
                .Join(repository.Languages, pl => pl.LanguageId, lang => lang.LanguageId, (pl, lang)
                => new
                {
                    lang.Name,
                    pl.IsPrimary

                }).ToList();

            if (primaryLanguageQuery.Any() && primaryLanguageQuery != null)
            {
               foreach(var l in primaryLanguageQuery)
                {
                    if (l.IsPrimary == 1)
                    {
                        ViewBag.PrimaryLanguage = l.Name.ToString();
                    }
                }
                
            }

            var patientRaceQuery = repository.PatientRaces.Where(r => r.Mrn == model.Mrn)
                .Join(repository.Races, pr => pr.RaceId, race => race.RaceId, (pr, race)
                => new
                {
                    race.Name

                }).ToList();

            if (patientRaceQuery.Any() && patientRaceQuery != null)
            {
                foreach(var race in patientRaceQuery)
                {
                    ViewBag.PatientRace = race.Name.ToString();
                }
            }

            return View(model);
        }

        //List Alerts for the currently selected MRN
        public IActionResult ListAlerts(string id, string sortOrder)
        {
            // Remember the user's original request
            ViewBag.ReturnUrl = Request.Headers["Referer"].ToString();

            //ViewBag.CommentSortParm = String.IsNullOrEmpty(sortOrder) ? "byComments" : "byCommentsDesc";
            ViewBag.StartSortParm = sortOrder == "byStartDate" ? "byStartDateDesc" : "byStartDate";
            ViewBag.ActiveSortParm = sortOrder == "byActive" ? "byActiveDesc" : "byActive";
            //ViewBag.LastModifiedTypeSortParm = sortOrder == "byLastModified" ? "byLastModified" : "";
            ViewBag.AlertTypeSortParm = sortOrder == "byAlertType" ? "byAlertTypeDesc" : "byAlertType";

            // Existing
            ViewBag.myMrn = id;
            //ViewBags for Patient Banner at top of page
            ViewBag.Patient = repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == id);


            if (sortOrder == "byAlertType" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Alert Type Ascending";
            }
            else if (sortOrder == "byAlertTypeDesc" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Alert Type Descending";
            }
            else if (sortOrder == "byStartDate" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Start Date Ascending";
            }

            else if (sortOrder == "byStartDateDesc" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Start Date Descending";
            }

            else if (sortOrder == "byActive" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Active Ascending";
            }

            else if (sortOrder == "byActiveDesc" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Active Descending";
            }

            else
            {
                TempData["msg"] = "";
            }


            if (sortOrder == "byStartDate")
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderBy(p => p.StartDate)
                });
            }

            if (sortOrder == "byStartDateDesc")
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderByDescending(p => p.StartDate)
                });
            }

            if (sortOrder == "byAlertType")
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderBy(p => p.AlertType.Name)
                });
            }

            if (sortOrder == "byAlertTypeDesc")
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderByDescending(p => p.AlertType.Name)
                });
            }

            if (sortOrder == "byActive")
            {
                {
                    sortOrder = "";
                    return View(new ListAlertsViewModel
                    {
                        PatientAlerts = repository.PatientAlerts
                            .Include(p => p.AlertType)
                            .Where(p => p.Mrn == id)
                            .OrderBy(p => p.IsActive)
                    });
                }
            }

            if (sortOrder == "byActiveDesc")
            {
                {
                    sortOrder = "";
                    return View(new ListAlertsViewModel
                    {
                        PatientAlerts = repository.PatientAlerts
                            .Include(p => p.AlertType)
                            .Where(p => p.Mrn == id)
                            .OrderByDescending(p => p.IsActive)
                    });
                }
            }
            else
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderByDescending(p => p.LastModified)
                });
            }
        }

        public IActionResult BackToCaller(string id, string returnUrl)
        {
            if (returnUrl.Length > 0)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Details", "Patient", id);
            }
        }

        public RedirectToRouteResult BackToDetails(string id) =>
            RedirectToRoute(new
            {
                controller = "Patient",
                action = "Details",
                ID = id
            });

        public RedirectToRouteResult BackToListAlerts(string id) =>
            RedirectToRoute(new
            {
                controller = "Patient",
                action = "ListAlerts",
                ID = id
            });

        // Load page for adding patient alerts
        [Authorize(Roles = "Administrator, Nursing Faculty, Nursing Student")]
        public IActionResult CreateAlert(string id, string returnUrl)
        {
            ViewBag.myMrn = id;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LastModified = DateTime.Today;
            //ViewBags for Patient Banner at top of page
            ViewBag.Patient = repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == id);

            if (repository.PatientAlerts.FirstOrDefault(b => b.Mrn == id) == null)
            {
                ViewBag.MRN = id;
            }
            else
            {
                ViewBag.MRN = repository.PatientAlerts.FirstOrDefault(b => b.Mrn == id).Mrn;
            }

            ViewBag.LastModified = @DateTime.Now;

            ViewBag.AlertType = repository.AlertTypes.OrderBy(a => a.Name).Select(a =>
                new SelectListItem
                {
                    Value = a.AlertId.ToString(),
                    Text = a.Name
                }).ToList();

            ViewBag.Restriction = repository.Restrictions.OrderBy(r => r.Name).Include(r => r.PatientRestrictions)
                .Select(r =>
                    new SelectListItem
                    {
                        Value = r.RestrictionId.ToString(),
                        Text = r.Name
                    }).ToList();

            //var query = repository.FallRisk.Select(r => new { r.FallRiskId, r.FallRisk.Name });
            //ViewBag.PatientFallRisk = new SelectList(query.AsEnumerable(), "FallRiskId", "Name", 0);
            ViewBag.PatientFallRisk = repository.FallRisks.OrderBy(r => r.Name).Include(r => r.PatientFallRisks).Select(
                r =>
                    new SelectListItem
                    {
                        Value = r.FallRiskId.ToString(),
                        Text = r.Name
                    }).ToList();


            ViewBag.Allergens = repository.Allergens.OrderBy(r => r.AllergenName).Include(r => r.PatientAllergy)
                .Select(r =>
                    new SelectListItem
                    {
                        Value = r.AllergenId.ToString(),
                        Text = r.AllergenName
                    }).ToList();


            ViewBag.Reactions = repository.Reactions.OrderBy(r => r.Name).Include(r => r.PatientAllergy).Select(r =>
                new SelectListItem
                {
                    Value = r.ReactionId.ToString(),
                    Text = r.Name
                }).ToList();

            return View();
        }

        [HttpPost]
        [ActionName("CreateAlert")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAlert(AlertsViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                ViewBag.AlertType = repository.PatientAlerts.Include(p => p.AlertType);
                ViewBag.FallRiskID = repository.PatientAlerts.Include(p => p.PatientFallRisks);
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
                //                 return Redirect(myUrl);
                //ViewBags for Patient Banner at top of page
                string id = model.Mrn;
                ViewBag.Patient = repository.Patients.Include(p => p.PatientAlerts)
                    .FirstOrDefault(b => b.Mrn == model.Mrn);

                return Redirect(ViewBag.ReturnUrl);
            }

            return View();
        }

        // Displays the Edit Patient page
        public IActionResult EditPatientAlert(int id, string mrn, string returnUrl)
        {
            ViewBag.myMrn = mrn;
            ViewBag.ReturnUrl = returnUrl;
            //ViewBags for Patient Banner at top of page
            ViewBag.Patient = repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == mrn);

            //ViewBag.LastModified = DateTime.Today.AddYears(-1);
            ViewBag.AlertTypeId = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).AlertTypeId;


            var myAlertType = (from pa in repository.PatientAlerts
                join at in repository.AlertTypes on pa.AlertTypeId equals at.AlertId
                where pa.PatientAlertId == id
                select new
                {
                    alertType = at.Name
                }).FirstOrDefault();

            ViewBag.MyAlertType = myAlertType.alertType;

            ViewBag.Comments = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).Comments;
            ViewBag.StartDate = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).StartDate
                .ToString("MM/dd/yyyy");

            ViewBag.EndDate = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).EndDate;
            //ViewBag.EndDate = checkEndDate != null ? checkEndDate : "N/A";

            var checkActive = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).IsActive;
            ViewBag.Active = checkActive == true ? "Yes" : "No";

            var myFallRisk = (from pa in repository.PatientAlerts
                join pf in repository.PatientFallRisks on pa.PatientAlertId equals pf.PatientAlertId
                join fr in repository.FallRisks on pf.FallRiskId equals fr.FallRiskId
                where pf.PatientAlertId == id
                select new
                {
                    fallrisk = fr.Name
                }).FirstOrDefault();

            if (myFallRisk == null)
            {
                ViewBag.FallRisk = "";
            }
            else
            {
                ViewBag.FallRisk = myFallRisk.fallrisk;
            }


            var myRestriction = (from pa in repository.PatientAlerts
                join pr in repository.PatientRestrictions on pa.PatientAlertId equals pr.PatientAlertId
                join re in repository.Restrictions on pr.RestrictionTypeId equals re.RestrictionId
                where pr.PatientAlertId == id
                select new
                {
                    theRestriction = re.Name
                }).FirstOrDefault();

            if (myRestriction == null)
            {
                ViewBag.RestrictionValue = "";
            }
            else
            {
                ViewBag.RestrictionValue = myRestriction.theRestriction;
            }

            var myAllergen = (from pa in repository.PatientAlerts
                join pal in repository.PatientAllergy on pa.PatientAlertId equals pal.PatientAlertId
                join al in repository.Allergens on pal.AllergenId equals al.AllergenId
                where pal.PatientAlertId == id
                select new
                {
                    allergen = al.AllergenName
                }).FirstOrDefault();

            if (myAllergen == null)
            {
                ViewBag.AllergenValue = "";
            }
            else
            {
                ViewBag.AllergenValue = myAllergen.allergen;
            }

            var myReaction = (from pa in repository.PatientAlerts
                join pal in repository.PatientAllergy on pa.PatientAlertId equals pal.PatientAlertId
                join rea in repository.Reactions on pal.ReactionId equals rea.ReactionId
                where pal.PatientAlertId == id
                select new
                {
                    reaction = rea.Name
                }).FirstOrDefault();

            if (myReaction == null)
            {
                ViewBag.ReactionValue = "";
            }
            else
            {
                ViewBag.ReactionValue = myReaction.reaction;
            }

            ViewBag.AlertType = repository.AlertTypes.OrderBy(a => a.Name).Select(a =>
                new SelectListItem
                {
                    Value = a.AlertId.ToString(),
                    Text = a.Name
                }).ToList();

            //ViewBag.PatientFallRisk = repository.FallRisks.Include(r => r.PatientFallRisks).Select(r =>
            //       new SelectListItem
            //       {
            //           Value = r.FallRiskId.ToString(),
            //           Text = r.Name
            //       }).ToList();

            //ViewBag.Restriction = repository.Restrictions.Include(r => r.PatientRestrictions).Select(r =>
            //            new SelectListItem
            //            {
            //                Value = r.RestrictionId.ToString(),
            //                Text = r.Name
            //            }).ToList();

            //ViewBag.Allergens = repository.Allergens.OrderBy(r => r.AllergenName).Include(r => r.PatientAllergy)
            //.Select(r =>
            //   new SelectListItem
            //   {
            //       Value = r.AllergenId.ToString(),
            //       Text = r.AllergenName
            //   }).ToList();


            return View(repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id));
        }

        [HttpPost]
        [ActionName("UpdateAlert")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAlert(PatientAlerts model, int id)
        {
            //TODO: THIS DOES NOT WORK. ALERTS ARE NOT EDITABLE. !!!!!!!!!!!!!
            Console.WriteLine("Trying to save(PatientController)");
            if (!ModelState.IsValid) return RedirectToAction("ListAlerts", new {id = model.Mrn});

            model.LastModified = DateTime.Now;
            ViewBag.AlertType = repository.PatientAlerts.Include(p => p.AlertType);
            ViewBag.FallRiskID = repository.PatientAlerts.Include(p => p.PatientFallRisks);
            ViewBag.LastModified = DateTime.Now;

            repository.EditAlert(model);
            return RedirectToAction("ListAlerts", new {id = model.Mrn});
        }

        public void AddDropdowns(Patient model = null)
        {
            var queryReligion = repository.Religions
                .OrderBy(r => r.Name)
                .Select(r => new {r.ReligionId, r.Name})
                .ToList();
            ViewBag.Religions = new SelectList(queryReligion, "ReligionId", "Name", 0);

            var querySex = repository.Sexes
                .OrderBy(r => r.Name)
                .Select(r => new {r.SexId, r.Name})
                .ToList();
            ViewBag.Sexes = new SelectList(querySex, "SexId", "Name", 0);

            var queryGender = repository.Genders
                .OrderBy(r => r.Name)
                .Select(r => new {r.GenderId, r.Name})
                .ToList();
            ViewBag.Gender = new SelectList(queryGender, "GenderId", "Name", 0);

            var queryEthnicity = repository.Ethnicities
                .OrderBy(r => r.Name)
                .Select(r => new {r.EthnicityId, r.Name})
                .ToList();
            ViewBag.Ethnicity = new SelectList(queryEthnicity, "EthnicityId", "Name", 0);

            var queryMaritalStatus = repository.MaritalStatuses
                .OrderBy(r => r.Name)
                .Select(r => new {r.MaritalStatusId, r.Name})
                .ToList();
            ViewBag.MaritalStatus = new SelectList(queryMaritalStatus, "MaritalStatusId", "Name", 0);

            var queryLanguages = repository.Languages
                .OrderBy(r => r.Name)
                .Select(r => new { r.LanguageId, r.Name })
                .ToList();
            if (model != null)
            {
                var patientHasPrimaryLanguage = repository.PatientLanguages.Any(l => l.Mrn == model.Mrn && l.IsPrimary == 1);
                ViewBag.Languages = patientHasPrimaryLanguage ?
                    new SelectList(queryLanguages, "LanguageId", "Name", repository.PatientLanguages.FirstOrDefault(l => l.Mrn == model.Mrn).LanguageId) :
                    new SelectList(queryLanguages, "LanguageId", "Name", 0);
            }
            else
            {
                ViewBag.Languages = new SelectList(queryLanguages, "LanguageId", "Name", 0);
            }

            var queryRaces = repository.Races
                .OrderBy(r => r.Name)
                .Select(r => new { r.RaceId, r.Name })
                .ToList();
            ViewBag.Races = new SelectList(queryRaces, "RaceId", "Name", 0);


        }

    }
}