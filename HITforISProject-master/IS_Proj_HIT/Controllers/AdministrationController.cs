using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using isprojectHiT.Models;
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
        public IActionResult CreateRole()
        {
            return View();
        }

        //public ViewResult RegisterDetails()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult RegisterDetails()
        {
            //ViewBag.Facilities = repository.Facilities.Select(f =>
            //                     new SelectListItem
            //                     {
            //                         Value = f.FacilityId.ToString(),
            //                         Text = f.Name
            //                     }).ToList();

            ViewBag.LastModified = DateTime.Now;

            return View();
        }

        [HttpPost]
        public IActionResult RegisterDetails(UserTable model)
        {

            var user = userManager.FindByEmailAsync(User.Identity.Name);
            var guidID = userManager.GetUserId(HttpContext.User);
            model.AspNetUsersID = guidID;
            //model.UserId = user.Id;
            model.Email = User.Identity.Name;
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            TryValidateModel(model);

            if (ModelState.IsValid)
            {
                //Debug.WriteLine("find me! " + Request.Form["Facility"]);
                model.LastModified = @DateTime.Now;
                //Debug.WriteLine("MRN: " + model.Mrn);
                //Debug.WriteLine("Facility: " + model.FacilityId);
                //Debug.WriteLine("EncounterType: " + model.EncounterTypeId);
                repository.AddUser(model);
                //return RedirectToAction("Index");
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });
            }


            foreach (var i in errors)
            {
                Debug.WriteLine("*******" + errors.ToString());
                ModelState.AddModelError("", errors.ToString());
            }

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

                foreach (IdentityError error in result.Errors)
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
                if (await userManager.IsInRoleAsync(user, role.Name))
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
            foreach (var user in userManager.Users)
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
