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
    public class EditModelPhysician : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public EditModelPhysician(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Physician Physician { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Physician = await _context.Physicians
                .Include(p => p.Address)
                .Include(p => p.ProviderType)
                .Include(p => p.Specialty).FirstOrDefaultAsync(m => m.PhysicianId == id);

            if (Physician == null)
            {
                return NotFound();
            }
           ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "Address1");
           ViewData["ProviderTypeId"] = new SelectList(_context.ProviderTypes, "ProviderTypeId", "Name");
           ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Physician).State = EntityState.Modified;

            try
            {
                Physician.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicianExists(Physician.PhysicianId))
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

        private bool PhysicianExists(int id)
        {
            return _context.Physicians.Any(e => e.PhysicianId == id);
        }
    }
}
