using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    public class DeleteModelCareSystemType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DeleteModelCareSystemType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        // Note: The CareSystemType model contains  public virtual ICollection<CareSystemParameter> CareSystemParameters { get; set; }
        [BindProperty]
        public CareSystemType CareSystemType { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            CareSystemType = await _context.CareSystemType
                .Include(csp=>csp.CareSystemParameters)
                .FirstOrDefaultAsync(m => m.CareSystemTypeId == id);

            if (CareSystemType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemType = await _context.CareSystemType
                .Include(csp => csp.CareSystemParameters)
                .FirstOrDefaultAsync(m => m.CareSystemTypeId == id);

            if (CareSystemType != null)
            {
                // See if any care system assessments exist using these parameters
                foreach (CareSystemParameter csp in CareSystemType.CareSystemParameters)
                {
                    bool csaExists = _context.CareSystemAssessment.Any(c => c.CareSystemParameterId == csp.CareSystemParameterId);
                    if (csaExists)
                    {
                        Console.WriteLine("Assessments exist using these records.");
                        ViewData["RegularMessage"] = "";
                        ViewData["ErrorMessage"] = "Assessments exist using these records. Delete not available.";
                        return Page();
                    }
                }

                using (var tran = new TransactionScope())
                {
                    foreach (CareSystemParameter csp in CareSystemType.CareSystemParameters)
                    {
                        _context.CareSystemParameter.Remove(csp);
                    }
                    _context.CareSystemType.Remove(CareSystemType);
                    _context.SaveChanges();

                    tran.Complete();
                }
                
            }
            
            return RedirectToPage("./Index");
        }
    }

}