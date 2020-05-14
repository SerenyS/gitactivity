using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelSpecialty : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DeleteModelSpecialty(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Specialty Specialty { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            Specialty = await _context.Specialty.FirstOrDefaultAsync(m => m.SpecialtyId == id);

            if (Specialty == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specialty = await _context.Specialty.FindAsync(id);

            if (Specialty != null)
            {
                // See if any Physician records exist with this type 
                bool usingExists = _context.Encounter.Any(e => e.EncounterPhysicians.Physician.SpecialtyId == Specialty.SpecialtyId);
                if (usingExists)
                {
                    Console.WriteLine("Physician records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Physician records exist using this record. Delete not available.";
                    return Page();
                }

                _context.Specialty.Remove(Specialty);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
