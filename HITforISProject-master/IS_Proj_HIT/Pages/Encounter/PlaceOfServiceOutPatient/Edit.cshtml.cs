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
    public class EditModelPlaceOfServiceOutPatient : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public EditModelPlaceOfServiceOutPatient(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlaceOfServiceOutPatient PlaceOfServiceOutPatient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlaceOfServiceOutPatient = await _context.PlaceOfServiceOutPatient.FirstOrDefaultAsync(m => m.PlaceOfServiceId == id);

            if (PlaceOfServiceOutPatient == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PlaceOfServiceOutPatient).State = EntityState.Modified;

            try
            {
                PlaceOfServiceOutPatient.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceOfServiceOutPatientExists(PlaceOfServiceOutPatient.PlaceOfServiceId))
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

        private bool PlaceOfServiceOutPatientExists(int id)
        {
            return _context.PlaceOfServiceOutPatient.Any(e => e.PlaceOfServiceId == id);
        }
    }
}
