using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models.PCA;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class EditModelPainRating : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public EditModelPainRating(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainRating PainRating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainRating = await _context.PainRating
                .Include(p => p.PainParameter).FirstOrDefaultAsync(m => m.PainRatingId == id);

            if (PainRating == null)
            {
                return NotFound();
            }
           ViewData["PainParameterId"] = new SelectList(_context.PainParameter, "PainParameterId", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PainRating).State = EntityState.Modified;

            try
            {
                PainRating.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PainRatingExists(PainRating.PainRatingId))
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

        private bool PainRatingExists(int id)
        {
            return _context.PainRating.Any(e => e.PainRatingId == id);
        }
    }
}
