using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;

namespace IS_Proj_HIT
{
    public class IndexModelPhysician : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public IndexModelPhysician(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<Physician> Physician { get;set; }

        public async Task OnGetAsync()
        {
            Physician = await _context.Physician
                .Include(p => p.Address)
                .Include(p => p.ProviderType)
                .Include(p => p.Specialty).ToListAsync();
        }
    }
}
