using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models;

namespace IS_Proj_HIT
{
    public class IndexModelBloodPressureRouteType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public IndexModelBloodPressureRouteType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<BloodPressureRouteType> BloodPressureRouteType { get;set; }

        public async Task OnGetAsync()
        {
            BloodPressureRouteType = await _context.BloodPressureRouteTypes.ToListAsync();
        }
    }
}
