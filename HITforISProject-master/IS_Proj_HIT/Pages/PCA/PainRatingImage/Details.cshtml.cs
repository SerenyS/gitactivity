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
    public class DetailsModelPainRatingImage : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DetailsModelPainRatingImage(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PainRatingImage PainRatingImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainRatingImage = await _context.PainRatingImages
                .Include(p => p.PainRating).FirstOrDefaultAsync(m => m.PainRatingId == id);

            if (PainRatingImage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
