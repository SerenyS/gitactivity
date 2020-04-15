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
    public class EditModelCareSystemParameter : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public EditModelCareSystemParameter(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CareSystemParameter CareSystemParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemParameter = await _context.CareSystemParameter
                .Include(c => c.CareSystemType).FirstOrDefaultAsync(m => m.CareSystemParameterId == id);

            if (CareSystemParameter == null)
            {
                return NotFound();
            }
           ViewData["CareSystemTypeId"] = new SelectList(_context.CareSystemType, "CareSystemTypeId", "Name", "NormalLimitsDescription");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CareSystemParameter).State = EntityState.Modified;

            try
            {
                CareSystemParameter.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareSystemParameterExists(CareSystemParameter.CareSystemParameterId))
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

        private bool CareSystemParameterExists(short id)
        {
            return _context.CareSystemParameter.Any(e => e.CareSystemParameterId == id);
        }
    }
}
