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

    public class DeleteModelPulseRouteType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DeleteModelPulseRouteType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PulseRouteType PulseRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            PulseRouteType = await _context.PulseRouteTypes.FirstOrDefaultAsync(m => m.PulseRouteTypeId == id);

            if (PulseRouteType == null)
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

            PulseRouteType = await _context.PulseRouteTypes.FindAsync(id);

            if (PulseRouteType != null)
            {
                // See if any PCA records exist with this type
                bool usingExists = _context.Pcarecords.Any(p => p.PulseRouteTypeId == PulseRouteType.PulseRouteTypeId);
                if (usingExists)
                {
                    Console.WriteLine("PCA records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "PCA records exist using this record. Delete not available.";
                    return Page();
                }

                _context.PulseRouteTypes.Remove(PulseRouteType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
