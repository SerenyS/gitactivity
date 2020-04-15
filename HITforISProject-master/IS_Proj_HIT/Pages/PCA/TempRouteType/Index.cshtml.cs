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
    public class IndexModelTempRouteType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public IndexModelTempRouteType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<TempRouteType> TempRouteType { get;set; }

        public async Task OnGetAsync()
        {
            TempRouteType = await _context.TempRouteType.ToListAsync();
        }
    }
}
