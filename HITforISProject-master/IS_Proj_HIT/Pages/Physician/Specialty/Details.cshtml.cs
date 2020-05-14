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
    public class DetailsModelSpecialty : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelSpecialty(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public Specialty Specialty { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specialty = await _context.Specialty.FirstOrDefaultAsync(m => m.SpecialtyId == id);

            if (Specialty == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
