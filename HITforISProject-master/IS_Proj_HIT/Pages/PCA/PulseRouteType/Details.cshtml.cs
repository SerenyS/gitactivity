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
    public class DetailsModelPulseRouteType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelPulseRouteType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PulseRouteType PulseRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PulseRouteType = await _context.PulseRouteType.FirstOrDefaultAsync(m => m.PulseRouteTypeId == id);

            if (PulseRouteType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
