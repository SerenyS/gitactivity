﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class CreateModelPainRating : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public CreateModelPainRating(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PainParameterId"] = new SelectList(_context.PainParameters, "PainParameterId", "Description");
            return Page();
        }

        [BindProperty]
        public PainRating PainRating { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PainRating.LastModified = DateTime.Now;
            _context.PainRatings.Add(PainRating);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}