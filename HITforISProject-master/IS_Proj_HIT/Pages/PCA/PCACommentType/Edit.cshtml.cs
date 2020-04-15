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

    public class EditModelPcaCommentType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public EditModelPcaCommentType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PcaCommentType PcaCommentType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PcaCommentType = await _context.PcacommentType.FirstOrDefaultAsync(m => m.PcaCommentTypeId == id);

            if (PcaCommentType == null)
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

            _context.Attach(PcaCommentType).State = EntityState.Modified;

            try
            {
                PcaCommentType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PcaCommentTypeExists(PcaCommentType.PcaCommentTypeId))
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

        private bool PcaCommentTypeExists(int id)
        {
            return _context.PcacommentType.Any(e => e.PcaCommentTypeId == id);
        }
    }
}
