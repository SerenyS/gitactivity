using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models.PCA;
using Microsoft.AspNetCore.Authorization;
using System.Transactions;
using System.Data.Common;


namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelPainParameter : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DeleteModelPainParameter(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainParameter PainParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            PainParameter = await _context.PainParameter
                .Include(p => p.PainScaleType)
                .Include(pr=>pr.PainRatings)
                .FirstOrDefaultAsync(m => m.PainParameterId == id);

            if (PainParameter == null)
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

            PainParameter = await _context.PainParameter
                            .Include(p => p.PainScaleType)
                            .Include(pr => pr.PainRatings)
                            .FirstOrDefaultAsync(m => m.PainParameterId == id);

            if (PainParameter != null)
            {
                // See if there are any pain assessments using this pain parameter
                bool paExists = _context.PcaPainAssessment.Any(p => p.PainParameterId == PainParameter.PainScaleTypeId);
                if (paExists)
                {
                    Console.WriteLine("Pain assessment records exist using these records.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Pain assessment records exist using these records. Delete not available.";
                    return Page();
                }

                using (var tran = new TransactionScope())
                {
                    foreach (PainRating pr in PainParameter.PainRatings)
                    {
                        _context.PainRating.Remove(pr);
                    }

                    _context.PainParameter.Remove(PainParameter);
                    _context.SaveChanges();

                    tran.Complete();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
