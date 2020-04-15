using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models.PCA;

namespace IS_Proj_HIT
{
    public class DetailsModelPcaCommentType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelPcaCommentType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

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
    }
}
