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
    public class DetailsModelProgramFacility : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelProgramFacility(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public ProgramFacility ProgramFacility { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProgramFacility = await _context.ProgramFacilities
                .Include(p => p.Facility)
                .Include(p => p.Program).FirstOrDefaultAsync(m => m.ProgramFacilitiesId == id);

            if (ProgramFacility == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
