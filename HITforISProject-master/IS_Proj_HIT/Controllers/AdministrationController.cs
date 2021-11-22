using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // Return to Admin Index
        public IActionResult Index() => View();

        // Administrator data entry for PCA
        // Used in: Administration Tools - PCA indexes
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

        // Administrator data entry for Encounter
        // Used in: Administration Tools - Encounter indexes
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
                typeof(PointOfOrigin).Name,
                typeof(Models.Program).Name,
                typeof(ProgramFacility).Name
            };
            return View(entityNames);
        }
        #endregion

        // Administrator data entry for Physician
        // Used in: Administration Tools - Physician indexes
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

        // Displays EditRegister details page
        // Used when clicked on your e-mail in the nav-bar
        // Used in: Login, Register, Home Page, LoginPartial
        #region User Details
        [Authorize(Roles = "Administrator")]
        public IActionResult EditRegisterDetails()
        {
            //find current user
            var id = _userManager.GetUserId(HttpContext.User);

            //select the information I want to display
            var dbUser = _repository.UserTables.FirstOrDefault(u => u.AspNetUsersId == id);

            return View(dbUser);
        }

        // Edits and saved register details
        // Used in: EditUserDetails, EditRegisterDetails
        [HttpPost]
        public IActionResult EditRegisterDetails(UserTable model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.AspNetUsersId))
                    model.AspNetUsersId = _userManager.GetUserId(HttpContext.User);
                if (string.IsNullOrWhiteSpace(model.Email))
                    model.Email = User.Identity.Name;

                // A quick fix until start date and end date are properly implemented
                model.StartDate = DateTime.Now;
                model.EndDate = DateTime.Now;

                model.LastModified = DateTime.Now;
                if (model.UserId is 0)
                    _repository.AddUser(model);
                else
                    _repository.EditUser(model);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        #endregion

        #region Roles
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]

        public IActionResult ViewRoles() => View(_roleManager.Roles);


        //Testing Listing the Correct Users - Chris P - 2/25/21
        //Used in: ViewUsers
        public async Task<IActionResult> ViewUsers()
        {
            var users = _repository.UserTables;
            return View(users);

        }


        //Retrieves user list
        //List users - Chris P - 2/27/21 edited by jason Motl 9-21-21 
        //Used in: Admin Details, Admin Index, ListUsers
        [HttpGet]
        public  IActionResult ListUsers()
        {
            //var users = _repository.UserTables;

            var model = _repository.UserTables.Select(u => new UsersPlusViewModel
            {
                UserId = u.UserId,
                UserName = u.Email,
                StartDate = u.StartDate,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AspNetUsersId = u.AspNetUsersId

            }).OrderByDescending(u => u.UserName).ToList();

            return View(model);
        }

        public async Task<IActionResult> ResetUserPassword(int id)
        {
            var userTable = _repository.UserTables.FirstOrDefault(u => u.UserId == id);
            var aspUser = await _userManager.FindByIdAsync(userTable.AspNetUsersId);
            string code = await _userManager.GeneratePasswordResetTokenAsync(aspUser);

            return RedirectToPage($"/Account/ResetPassword", new { area = "Identity", code, Id = id});
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

        // View edit role
        // Used in: EditUsersInRole, ViewRoles
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

        // Displays user details
        // Used in: UserList?
        public IActionResult Details(int id)
        {

            // grabs the user from the UserTable
            var user = _repository.UserTables.FirstOrDefault(u => u.UserId == id);
            // grabs the string AspNetUsersId from the AspNetUserRoles table
            var bridgeId = _db.AspNetUserRoles.FirstOrDefault(u => u.UserId == user.AspNetUsersId);

            var roleName = "";
            // try/catch prevents error from being thrown if there is no role assigned to the user
            try{
                roleName = _db.AspNetRoles.FirstOrDefault(u => u.Id == bridgeId.RoleId).Name;
            }catch{
                roleName = "Not Assigned";
            }


            var model = new UsersPlusViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ProgramEnrolledIn = user.ProgramEnrolledIn,
                StartDate = user.StartDate,

                EndDate = user.EndDate,
                RoleName = roleName

            };

            return View(model);
        }

        // View edit users in role
        // Used in: EditRole, List/ViewUsers
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

        // Create role
        // Used in: ViewRoles
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

        // Edit role
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

        // Edit users in role
        // EditUsersInRole with model AND ID
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

        // Edit user in administration
        // Only accessible from clicking edit, does not currently work if Details -> Edit
        // Adds user facility?
         #endregion
        public IActionResult EditUserDetails(EditUserViewModel viewModel)
        {
            var user = _repository.UserTables.FirstOrDefault(u => u.UserId == viewModel.UserId);

            ViewBag.ProgramList = new List<SelectListItem>();
            var programs = _repository.Programs;
            foreach (var program in programs)
            {
                ViewBag.ProgramList.Add(new SelectListItem { Text = program.Name, Value = program.ProgramId.ToString() });
            }

            ViewBag.FacilityList = new List<SelectListItem>();
            var facilities = _repository.Facilities;
            foreach (var facility in facilities)
            {
                ViewBag.FacilityList.Add(new SelectListItem { Text = facility.Name, Value = facility.FacilityId.ToString() });
            }

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

            var hasProgram = _repository.UserPrograms.Any(p => p.UserId == user.UserId);
            if (viewModel.ProgramId != 0 && !hasProgram)
            {
                _repository.AddUserProgram(new UserProgram { UserId = user.UserId, ProgramId = viewModel.ProgramId });
            }
            else if (viewModel.ProgramId != 0 && hasProgram)
            {
                _repository.EditUserProgram(new UserProgram { UserId = user.UserId, ProgramId = viewModel.ProgramId });
            }

            var hasFacility = _repository.UserFacilities.Any(f => f.UserId == user.UserId);
            if (viewModel.FacilityId != 0 && !hasFacility)
            {
                _repository.AddUserFacility(new UserFacility { UserId = user.UserId, FacilityId = viewModel.FacilityId });
            }
            else if (viewModel.FacilityId != 0 && hasFacility)
            {
                var currentUserFacility = _repository.UserFacilities.FirstOrDefault(f => f.UserId == user.UserId);
                _repository.DeleteUserFacility(currentUserFacility);
                _repository.AddUserFacility(new UserFacility { UserId = user.UserId, FacilityId = viewModel.FacilityId });
            }

            return View(viewModel);
        }

        public IActionResult ResetSecurityQuestions(int id)
        {
            return RedirectToPage($"/Account/AddSecurityQuestions", new { area = "Identity", Id = id });
        }
    }
}
