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

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelO2deliveryType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DeleteModelO2deliveryType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public O2deliveryType O2deliveryType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            O2deliveryType = await _context.O2deliveryTypes.FirstOrDefaultAsync(m => m.O2deliveryTypeId == id);

            if (O2deliveryType == null)
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

            O2deliveryType = await _context.O2deliveryTypes.FindAsync(id);

            if (O2deliveryType != null)
            {
                // See if any PCA records exist with this type
                bool usingExists = _context.Pcarecords.Any(p => p.O2deliveryTypeId == O2deliveryType.O2deliveryTypeId);
                if (usingExists)
                {
                    Console.WriteLine("PCA records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "PCA records exist using this record. Delete not available.";
                    return Page();
                }

                _context.O2deliveryTypes.Remove(O2deliveryType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
