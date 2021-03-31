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
    public class DetailsModelPlaceOfServiceOutPatient : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelPlaceOfServiceOutPatient(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PlaceOfServiceOutPatient PlaceOfServiceOutPatient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlaceOfServiceOutPatient = await _context.PlaceOfServiceOutPatients.FirstOrDefaultAsync(m => m.PlaceOfServiceId == id);

            if (PlaceOfServiceOutPatient == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
