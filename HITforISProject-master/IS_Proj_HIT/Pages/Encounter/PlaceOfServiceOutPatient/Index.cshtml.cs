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
    public class IndexModelPlaceOfServiceOutPatient : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public IndexModelPlaceOfServiceOutPatient(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PlaceOfServiceOutPatient> PlaceOfServiceOutPatient { get;set; }

        public async Task OnGetAsync()
        {
            PlaceOfServiceOutPatient = await _context.PlaceOfServiceOutPatient.ToListAsync();
        }
    }
}
