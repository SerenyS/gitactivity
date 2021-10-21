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
    public class EditModelFacilityProgram : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public EditModelFacilityProgram(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProgramFacility ProgramFacility { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProgramFacility = await _context.ProgramFacilities
                .Include(p => p.Facility)
                .Include(p => p.Program).FirstOrDefaultAsync(m => m.ProgramFacilitiesId == id);

            if (ProgramFacility == null)
            {
                return NotFound();
            }
           ViewData["FacilityId"] = new SelectList(_context.Facilities, "FacilityId", "Name");
           ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProgramFacility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramFacilityExists(ProgramFacility.ProgramFacilitiesId))
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

        private bool ProgramFacilityExists(int id)
        {
            return _context.ProgramFacilities.Any(e => e.ProgramFacilitiesId == id);
        }
    }
}
