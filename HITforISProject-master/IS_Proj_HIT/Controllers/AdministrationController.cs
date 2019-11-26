using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using isprojectHiT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IS_Proj_HIT.Controllers
{
    public class AdministrationController : Controller
    {
        
        private IWCTCHealthSystemRepository repository;
        private RoleManager<IdentityRole> roleManager { get; }
        private UserManager<IdentityUser> userManager { get; }
        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<IdentityUser> userManager,
                                        IWCTCHealthSystemRepository repo) 
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.repository = repo;
        }


        [HttpGet]
        public IActionResult RegisterDetails()
        {    
            ViewBag.LastModified = DateTime.Now;
            
            return View();
        }

        [HttpPost]
        public IActionResult RegisterDetails(UserTable model)
        {
            //get AspNetUsersID Guid to use as a foreign key
            model.AspNetUsersID = userManager.GetUserId(HttpContext.User);
            //get email from current user
            model.Email = User.Identity.Name;      

            if (ModelState.IsValid)
            {
                model.LastModified = @DateTime.Now;
                repository.AddUser(model);
                
                return RedirectToRoute(new
                {
                    controller = "Administration",
                    action = "EditRegisterDetails",
                    model = "RegisterDetailsViewModel"
                });
            }  

            return View();
        }

        [HttpGet]
        public IActionResult EditRegisterDetails(UserTable model)
        {
            var updateDetails = new RegisterDetailsViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProgramEnrolledIn = model.ProgramEnrolledIn,
                Instructor = model.Instructor


            };
   
            ViewBag.LastModified = DateTime.Now;
            return View(updateDetails);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
             
            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name

            };

            foreach (var user in userManager.Users)
            {
                if( await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            foreach(var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);

            }
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {

            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
            
        }

        public IActionResult ListAppUsers()
        {
            var appUsers = "users go here";
                return View(appUsers);
        }
    }
}
