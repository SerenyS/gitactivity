using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Authorization;
using System.Transactions;
using System.Data.Common;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelPainScaleType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DeleteModelPainScaleType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainScaleType PainScaleType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            PainScaleType = await _context.PainScaleTypes
                .Include(pp=>pp.PainParameters)
                .FirstOrDefaultAsync(m => m.PainScaleTypeId == id);

            if (PainScaleType == null)
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

            PainScaleType = await _context.PainScaleTypes
                .Include(pp => pp.PainParameters)
                .FirstOrDefaultAsync(m => m.PainScaleTypeId == id);

            if (PainScaleType != null)
            {
                // See if any PCA records exist for this Pain Scale Type
                bool PcaExists = _context.Pcarecords.Any(p => p.PainScaleTypeId== PainScaleType.PainScaleTypeId);
                if (PcaExists)
                {
                    Console.WriteLine("PCA records exist using these records.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "PCA records exist using these records. Delete not available.";
                    return Page();
                }

                // See if any Pain Assessments exist using the Pain Parameters
                foreach (PainParameter pp in PainScaleType.PainParameters)
                {
                    bool paExists = _context.PcapainAssessments.Any(p => p.PainParameterId == pp.PainParameterId);
                    if (paExists)
                    {
                        Console.WriteLine("Pain assessment records exist using these records.");
                        ViewData["RegularMessage"] = "";
                        ViewData["ErrorMessage"] = "Pain assessment records exist using these records. Delete not available.";
                        return Page();
                    }
                }

                using (var tran = new TransactionScope())
                {
                    foreach (PainParameter pp in PainScaleType.PainParameters)
                    {
                        _context.PainParameters.Remove(pp);
                    }
                    _context.PainScaleTypes.Remove(PainScaleType);
                    _context.SaveChanges();

                    tran.Complete();
                }

            }

            return RedirectToPage("./Index");
        }
    }
}
