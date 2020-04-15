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
    public class EditModelBloodPressureRouteType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public EditModelBloodPressureRouteType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BloodPressureRouteType BloodPressureRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BloodPressureRouteType = await _context.BloodPressureRouteType.FirstOrDefaultAsync(m => m.BloodPressureRouteTypeId == id);

            if (BloodPressureRouteType == null)
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

            _context.Attach(BloodPressureRouteType).State = EntityState.Modified;

            try
            {
                BloodPressureRouteType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodPressureRouteTypeExists(BloodPressureRouteType.BloodPressureRouteTypeId))
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

        private bool BloodPressureRouteTypeExists(byte id)
        {
            return _context.BloodPressureRouteType.Any(e => e.BloodPressureRouteTypeId == id);
        }
    }
}
