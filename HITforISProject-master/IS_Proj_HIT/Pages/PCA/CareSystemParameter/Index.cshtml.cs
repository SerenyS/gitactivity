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
    public class IndexModelCareSystemParameter : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public IndexModelCareSystemParameter(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<CareSystemParameter> CareSystemParameter { get;set; }

        public async Task OnGetAsync()
        {
            CareSystemParameter = await _context.CareSystemParameter
                .Include(c => c.CareSystemType).ToListAsync();
        }
    }
}
