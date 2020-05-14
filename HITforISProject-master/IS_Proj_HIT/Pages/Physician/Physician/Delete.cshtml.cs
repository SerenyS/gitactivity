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
    public class DeleteModelPhysician : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DeleteModelPhysician(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Physician Physician { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            Physician = await _context.Physician
                .Include(p => p.Address)
                .Include(p => p.ProviderType)
                .Include(p => p.Specialty).FirstOrDefaultAsync(m => m.PhysicianId == id);

            if (Physician == null)
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

            Physician = await _context.Physician.FindAsync(id);

            if (Physician != null)
            {
                // See if any Encounter records exist with this type 
                bool usingExists = _context.Encounter.Any(e => e.EncounterPhysicians.PhysicianId == Physician.PhysicianId);
                if (usingExists)
                {
                    Console.WriteLine("Encounter records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Encounter records exist using this record. Delete not available.";
                    return Page();
                }

                _context.Physician.Remove(Physician);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
