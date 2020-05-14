using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class EditModelEncounterPhysicians : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public EditModelEncounterPhysicians(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EncounterPhysicians EncounterPhysicians { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EncounterPhysicians = await _context.EncounterPhysicians
                .Include(e => e.Physician)
                .Include(e => e.PhysicianRole).FirstOrDefaultAsync(m => m.EncounterPhysiciansId == id);

            if (EncounterPhysicians == null)
            {
                return NotFound();
            }
           ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "LastName");
           ViewData["PhysicianRoleId"] = new SelectList(_context.PhysicianRole, "PhysicianRoleId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EncounterPhysicians).State = EntityState.Modified;

            try
            {
                EncounterPhysicians.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncounterPhysiciansExists(EncounterPhysicians.EncounterPhysiciansId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EncounterPhysiciansExists(long id)
        {
            return _context.EncounterPhysicians.Any(e => e.EncounterPhysiciansId == id);
        }
    }
}
