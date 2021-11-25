using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    public class SelectFacilityModel : PageModel
    {
        private readonly IWCTCHealthSystemRepository _repository;

        public SelectFacilityModel(IWCTCHealthSystemRepository repository)
        {
            _repository = repository;
            Facilities = new List<Facility>();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string FacilityName { get; set; }
        }

        public List<Facility> Facilities { get; set; }
        public void  OnGet(int id, string returnUrl = null)
        {
            Url.Content("~/");

            var userPrograms = _repository.UserPrograms.Where(u => u.UserId == id).ToList();

            if (userPrograms.Count == 1)
            {
                // Determines facilities to be used in screen
                var programFacilities = _repository.ProgramFacilities.Where(p => p.ProgramId == userPrograms[0].ProgramId).ToList();
                foreach (var facilityProgram in programFacilities)
                    Facilities.Add( _repository.Facilities.FirstOrDefault(f => f.FacilityId == facilityProgram.FacilityId));
            }
        }

        public IActionResult OnPost(int id, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");

            var oldUserFacility = _repository.UserFacilities.FirstOrDefault(f => f.UserId == id);
            if (oldUserFacility != null)
            {
                _repository.DeleteUserFacility(oldUserFacility);
            }

            var newFacility = _repository.Facilities.FirstOrDefault(f => f.Name == Input.FacilityName);
            if (newFacility != null)
                _repository.AddUserFacility(new UserFacility()
                {
                    FacilityId = newFacility.FacilityId,
                    UserId = id,
                    LastModified = DateTime.Now
                });

            return LocalRedirect(returnUrl);
        }
    }
}
