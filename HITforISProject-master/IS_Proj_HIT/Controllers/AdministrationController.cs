using IS_Proj_HIT.Data;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace IS_Proj_HIT.Controllers
{

    [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
    public class AdministrationController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WCTCHealthSystemContext _db;



        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IWCTCHealthSystemRepository repo, WCTCHealthSystemContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _repository = repo;
            _db = db;
        }

        public IActionResult Index() => View();

        #region PCA lookup table management
        [Authorize(Roles = "Administrator")]
        public IActionResult DataEntry()
        {
            var entityNames = new List<string>
            {
                typeof(PcacommentType).Name,
                typeof(BloodPressureRouteType).Name,
                typeof(O2deliveryType).Name,
                typeof(PulseRouteType).Name,
                typeof(TempRouteType).Name,
                typeof(CareSystemParameter).Name,
                typeof(CareSystemType).Name,
                typeof(PainParameter).Name,
                typeof(PainRating).Name,
                typeof(PainRatingImage).Name,
                typeof(PainScaleType).Name
            };
            return View(entityNames);
        }
        #endregion

        #region Encounter lookup table management
        [Authorize(Roles = "Administrator")]
        public IActionResult EncounterDataEntry()
        {
            var entityNames = new List<string>
            {
                typeof(AdmitType).Name,
                typeof(Department).Name,
                typeof(Discharge).Name,
                typeof(EncounterType).Name,
                typeof(Facility).Name,
                typeof(PlaceOfServiceOutPatient).Name,
                typeof(PointOfOrigin).Name
            };
            return View(entityNames);
        }
        #endregion

        #region Physician lookup table management
        [Authorize(Roles = "Administrator")]
        public IActionResult PhysicianDataEntry()
        {
            var entityNames = new List<string>
            {
                typeof(Physician).Name,
                typeof(PhysicianRole).Name,
                typeof(ProviderType).Name,
                typeof(Specialty).Name
            };
            return View(entityNames);
        }
        #endregion

        #region User Details
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditRegisterDetails()
        {
            //find current user
            var id = _userManager.GetUserId(HttpContext.User);

            //select the information I want to display
            var dbUser = _repository.UserTables.FirstOrDefault(u => u.AspNetUsersId == id) ??
                         new UserTable { StartDate = DateTime.Now, EndDate = DateTime.Now };

            //Create or get program list from DB
            ViewBag.ProgramList = new List<SelectListItem>
            {
                new SelectListItem {Text = "HIT/MCS", Value = "HIT/MCS", Selected = true},
                new SelectListItem {Text = "Nursing", Value = "Nursing"}
            };

            //get list of possible instructors from db
            var instructorEmails = new List<string>();
            instructorEmails.AddRange(
                (await _userManager.GetUsersInRoleAsync("HIT Faculty"))
                .Select(u => u.Email));
            instructorEmails.AddRange(
                (await _userManager.GetUsersInRoleAsync("Nursing Faculty"))
                .Select(u => u.Email));
            ViewBag.InstructorList = _repository.UserTables.Where(user => instructorEmails.Contains(user.Email))
                .Select(u => new SelectListItem

                { Text = u.LastName, Value = u.UserId.ToString(), Selected = dbUser.InstructorId == u.UserId }).ToList();

            return View(dbUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditRegisterDetails(UserTable model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.AspNetUsersId))
                    model.AspNetUsersId = _userManager.GetUserId(HttpContext.User);
                if (string.IsNullOrWhiteSpace(model.Email))
                    model.Email = User.Identity.Name;

                model.LastModified = DateTime.Now;
                if (model.UserId is 0)
                    _repository.AddUser(model);
                else
                    _repository.EditUser(model);

                return RedirectToAction("Index", "Home");
            }

            //Create or get program list from DB
            ViewBag.ProgramList = new List<SelectListItem>
            {
                new SelectListItem {Text = "HIT/MCS", Value = "HIT/MCS", Selected = true},
                new SelectListItem {Text = "Nursing", Value = "Nursing"}
            };

            //get list of possible instructors from db
            var instructorEmails = new List<string>();
            instructorEmails.AddRange(
                (await _userManager.GetUsersInRoleAsync("HIT Faculty"))
                .Select(u => u.Email));
            instructorEmails.AddRange(
                (await _userManager.GetUsersInRoleAsync("Nursing Faculty"))
                .Select(u => u.Email));
            ViewBag.InstructorList = _repository.UserTables.Where(user => instructorEmails.Contains(user.Email))
                .Select(u => new SelectListItem

                { Text = u.LastName, Value = u.UserId.ToString(), Selected = model.InstructorId == u.UserId }).ToList();


            return View(model);
        }

        #endregion

        #region Roles
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]

        public IActionResult ViewRoles() => View(_roleManager.Roles);


        //Testing Listing the Correct Users - Chris P - 2/25/21
        public async Task<IActionResult> ViewUsers()
        {
            var users = _repository.UserTables;
            return View(users);

        }


        //List users - Chris P - 2/27/21
        [HttpGet]
        public  IActionResult ListUsers()
        {
            //var users = _repository.UserTables;

            var model = _repository.UserTables.Select(u => new UsersPlusViewModel
            {
                UserId = u.UserId,
                UserName = u.Email,
                StartDate = u.StartDate

            }).OrderByDescending(u => u.UserName).ToList();

            return View(model);
        }



        //Delete User from AspNetUsers - Chris P - 2/28/21
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);
            

            if(userToDelete == null)
            {
                ViewBag.ErrorMessage = $"User with the Id = {id} cannot be found.";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(userToDelete);
                
                if(result.Succeeded)
                {
                    
                    return RedirectToAction("ListUsers");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListUsers");
                    
            }
        }

        
        public async Task<IActionResult>  DeleteBatch(List<UsersPlusViewModel> userIdsToDelete)
        {
                foreach (var user in userIdsToDelete.Where(u => u.IsSelected))
                {

                    var selectedUser = _userManager.Users.Single(u => u.UserName == user.UserName);
                    
                    var userId = await _userManager.FindByIdAsync(selectedUser.Id);
                    
                    var result =  await _userManager.DeleteAsync(userId);
                
            }

            return RedirectToAction("ListUsers");
            
        }


        public IActionResult CreateRole() => View();

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {id} cannot be found";
                return View("NotFound");
            }


            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = (await _userManager.GetUsersInRoleAsync(role.Name)).Select(u => u.UserName).OrderBy(username => username).ToList()

            };

            return View(model);
        }
        public IActionResult Details(int id)
        {
            var user = _repository.UserTables.FirstOrDefault(u => u.UserId == id);

            var model = new UsersPlusViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ProgramEnrolledIn = user.ProgramEnrolledIn,
                StartDate = user.StartDate,
                EndDate = user.EndDate
            };

            return View(model);
        }
        public IActionResult DetailsNext(int id)
        {
            id += 1;
            var user = _repository.UserTables.FirstOrDefault(u => u.UserId == id);

            var model = new UsersPlusViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ProgramEnrolledIn = user.ProgramEnrolledIn,
                StartDate = user.StartDate,
                EndDate = user.EndDate
            };

            return View(model);
        }
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("NotFound");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);


            var model =  _userManager.Users.AsEnumerable().Select(u => new UserRoleViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                IsSelected = usersInRole.Any(inRole => inRole.UserName == u.UserName)

            }).AsEnumerable().OrderByDescending(u => u.IsSelected).ThenBy(u => u.UserName).ToList();

            foreach (var user in model)
            {
                var dbUser = _repository.UserTables.FirstOrDefault(u => u.AspNetUsersId == user.UserId);
                user.FullName = dbUser != null ? dbUser.FirstName + " " + dbUser.LastName : "No Name On File";
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("NotFound");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var modelUser in model)
            {
                var isCurrentlyInRole = usersInRole.Any(u => u.Id == modelUser.UserId);

                IdentityUser user;
                if (modelUser.IsSelected && !isCurrentlyInRole)
                {
                    user = await _userManager.FindByIdAsync(modelUser.UserId);
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!modelUser.IsSelected && isCurrentlyInRole)
                {
                    user = await _userManager.FindByIdAsync(modelUser.UserId);
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        #endregion
        public async Task<IActionResult> EditUserDetails(int id)
        {
            var user = _repository.UserTables.FirstOrDefault(u => u.UserId == id);
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(user.AspNetUsersId))
                    user.AspNetUsersId = _userManager.GetUserId(HttpContext.User);
                if (string.IsNullOrWhiteSpace(user.Email))
                    user.Email = User.Identity.Name;

                user.LastModified = DateTime.Now;
                if (user.UserId is 0)
                    _repository.AddUser(user);
                else
                    _repository.EditUser(user);
            }

            //Create or get program list from DB
            ViewBag.ProgramList = new List<SelectListItem>
            {
                new SelectListItem {Text = "HIT/MCS", Value = "HIT/MCS", Selected = true},
                new SelectListItem {Text = "Nursing", Value = "Nursing"}
            };

            //get list of possible instructors from db
            var instructorEmails = new List<string>();
            instructorEmails.AddRange(
                (await _userManager.GetUsersInRoleAsync("HIT Faculty"))
                .Select(u => u.Email));
            instructorEmails.AddRange(
                (await _userManager.GetUsersInRoleAsync("Nursing Faculty"))
                .Select(u => u.Email));
            ViewBag.InstructorList = _repository.UserTables.Where(user => instructorEmails.Contains(user.Email))
                .Select(u => new SelectListItem

                { Text = u.LastName, Value = u.UserId.ToString(), Selected = user.InstructorId == u.UserId }).ToList();


            return View(user);
        }
       /* public async Task<IActionResult> EditUserDetails(string id)
        {
            //find current user
            //var id = _userManager.GetUserId(HttpContext.User);

            //select the information I want to display
            var dbUser = _repository.UserTables.FirstOrDefault(u => u.AspNetUsersId == id) ??
                         new UserTable { StartDate = DateTime.Now, EndDate = DateTime.Now };

            //Create or get program list from DB
            ViewBag.ProgramList = new List<SelectListItem>
            {
                new SelectListItem {Text = "HIT/MCS", Value = "HIT/MCS", Selected = true},
                new SelectListItem {Text = "Nursing", Value = "Nursing"}
            };

            //get list of possible instructors from db
            var instructorEmails = new List<string>();
            instructorEmails.AddRange(
                (await _userManager.GetUsersInRoleAsync("HIT Faculty"))
                .Select(u => u.Email));
            instructorEmails.AddRange(
                (await _userManager.GetUsersInRoleAsync("Nursing Faculty"))
                .Select(u => u.Email));
            ViewBag.InstructorList = _repository.UserTables.Where(user => instructorEmails.Contains(user.Email))
                .Select(u => new SelectListItem

                { Text = u.LastName, Value = u.UserId.ToString(), Selected = dbUser.InstructorId == u.UserId }).ToList();

            return View(dbUser);
        }*/

    }
}