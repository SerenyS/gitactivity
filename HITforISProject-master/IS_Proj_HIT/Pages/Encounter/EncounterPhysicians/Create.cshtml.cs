using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class CreateModelEncounterPhysicians : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public CreateModelEncounterPhysicians(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "FirstName");
        ViewData["PhysicianRoleId"] = new SelectList(_context.PhysicianRole, "PhysicianRoleId", "Name");
            return Page();
        }

        [BindProperty]
        public EncounterPhysicians EncounterPhysicians { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            EncounterPhysicians.LastModified = DateTime.Now;
            _context.EncounterPhysicians.Add(EncounterPhysicians);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}