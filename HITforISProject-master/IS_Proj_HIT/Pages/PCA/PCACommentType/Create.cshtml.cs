using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models.PCA;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{

    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]

    public class CreateModelPcaCommentType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public CreateModelPcaCommentType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PcaCommentType PcaCommentType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PcacommentType.Add(PcaCommentType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}