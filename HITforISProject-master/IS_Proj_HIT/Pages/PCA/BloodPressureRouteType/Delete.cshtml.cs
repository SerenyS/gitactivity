﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models.PCA;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelBloodPressureRouteType : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public DeleteModelBloodPressureRouteType(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BloodPressureRouteType BloodPressureRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BloodPressureRouteType = await _context.BloodPressureRouteType.FirstOrDefaultAsync(m => m.BloodPressureRouteTypeId == id);

            if (BloodPressureRouteType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BloodPressureRouteType = await _context.BloodPressureRouteType.FindAsync(id);

            if (BloodPressureRouteType != null)
            {
                _context.BloodPressureRouteType.Remove(BloodPressureRouteType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
