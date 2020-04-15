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
    public class DetailsModelCareSystemType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelCareSystemType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public CareSystemType CareSystemType { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemType = await _context.CareSystemType.FirstOrDefaultAsync(m => m.CareSystemTypeId == id);

            if (CareSystemType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
