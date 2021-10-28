using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Identity;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    public class SetProgramModel : PageModel
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public SetProgramModel(IWCTCHealthSystemRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
            Programs = new List<Models.Program>();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string ProgramName { get; set; }
        }

        public List<Models.Program> Programs { get; set; }
        public void OnGet()
        {
            Programs = _repository.Programs.ToList();
        }

        public async Task<IActionResult> OnPost(int id, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");

            var userTable = _repository.UserTables.FirstOrDefault(u => u.UserId == id);
            var selectedProgram = _repository.Programs.FirstOrDefault(p => p.Name == Input.ProgramName);

            _repository.AddUserProgram(new UserProgram()
            {
                UserId = id,
                ProgramId = selectedProgram.ProgramId
            });

            var aspNetUser = await _userManager.FindByIdAsync(userTable.AspNetUsersId);
            var currentRoles = await _userManager.GetRolesAsync(aspNetUser);

            var isFaculty = false;
            foreach (var role in currentRoles)
            {
                if ((role == "HIT Faculty" || role == "Nursing Faculty") && role != "Administrator")
                {
                    isFaculty = true;
                }
            }

            if (isFaculty)
            {
                return RedirectToPage("./SelectFacility", new { Id = id, ReturnUrl = returnUrl });
            }
            else
            { 
                var programFacilities = _repository.ProgramFacilities.Where(p => p.ProgramId == selectedProgram.ProgramId).ToList();
                Facility assignedFacility = null;
                foreach (var programFacility in programFacilities)
                {
                    var facility = _repository.Facilities.FirstOrDefault(f => f.FacilityId == programFacility.FacilityId);
                    if (facility.Name.Contains("SIM"))
                    {
                        assignedFacility = programFacility.Facility;
                    }
                }
                _repository.AddUserFacility(new UserFacility()
                {
                    UserId = userTable.UserId,
                    FacilityId = assignedFacility.FacilityId,
                    LastModified = DateTime.Now
                });
            }

            return LocalRedirect(returnUrl);
        }
    }
}
