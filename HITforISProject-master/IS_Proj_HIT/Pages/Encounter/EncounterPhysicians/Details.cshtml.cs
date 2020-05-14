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
    public class DetailsModelEncounterPhysicians : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelEncounterPhysicians(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public EncounterPhysicians EncounterPhysicians { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EncounterPhysicians = await _context.EncounterPhysicians
                .Include(e => e.Physician)
                .Include(e => e.PhysicianRole).FirstOrDefaultAsync(m => m.EncounterPhysiciansId == id);

            if (EncounterPhysicians == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
