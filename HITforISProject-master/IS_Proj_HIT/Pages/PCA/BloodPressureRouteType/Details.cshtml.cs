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
    public class DetailsModelBloodPressureRouteType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelBloodPressureRouteType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

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
    }
}
