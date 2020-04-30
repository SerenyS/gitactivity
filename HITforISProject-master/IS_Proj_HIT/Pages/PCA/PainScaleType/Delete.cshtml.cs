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

            PainScaleType = await _context.PainScaleType
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

            PainScaleType = await _context.PainScaleType
                .Include(pp => pp.PainParameters)
                .FirstOrDefaultAsync(m => m.PainScaleTypeId == id);

            if (PainScaleType != null)
            {
                try 
                {
                    using (var tran = new TransactionScope())
                    {
                        foreach (PainParameter pp in PainScaleType.PainParameters)
                        {
                            _context.PainParameter.Remove(pp);
                        }
                        _context.PainScaleType.Remove(PainScaleType);
                        _context.SaveChanges();

                        tran.Complete();
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("Assessments exist using these records." + ex.Message);
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Assessments exist using these records. Delete not available.";
                    return Page();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                    {
                        Console.WriteLine("Assessments exist using these records." + ex.Message);
                        ViewData["RegularMessage"] = "";
                        ViewData["ErrorMessage"] = "Assessments exist using these records. Delete not available.";
                        return Page();
                    }
                    else
                    {
                        Console.WriteLine("Exception " + ex.Message);
                        ViewData["RegularMessage"] = "";
                        ViewData["ErrorMessage"] = "There was a problem deleting these records. Please contact your administrator.";
                        return Page();
                    }
                }

            }

            return RedirectToPage("./Index");
        }
    }
}
