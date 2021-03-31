﻿using System;
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
    public class DetailsModelPainScaleType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelPainScaleType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PainScaleType PainScaleType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainScaleType = await _context.PainScaleTypes.FirstOrDefaultAsync(m => m.PainScaleTypeId == id);

            if (PainScaleType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
