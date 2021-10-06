using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class CreateModelPhysician : PageModel
    {
        private readonly IS_Proj_HIT.Models.Data.WCTCHealthSystemContext _context;

        public CreateModelPhysician(IS_Proj_HIT.Models.Data.WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        //ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "Address1");
        ViewData["ProviderTypeId"] = new SelectList(_context.ProviderTypes, "ProviderTypeId", "Name");
        ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "SpecialtyId", "Name");
            return Page();
        }

        [BindProperty]
        public Physician Physician { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            /*
            List<Address> foundAddresses = new List<Address>();

            foreach(Address a in _context.Addresses) {
                if (a.Address1.Equals(Physician.AddressId)) {
                    foundAddresses.Add(a);
                }
            }

            if (!foundAddresses.IsEmpty()) {
                Address newAddress = new Address();
                newAddress.Address1 = Physician.AddressId;
                newAddress.LastModified = DateTime.Now;
                _context.Addresses.Add(newAddress);
                Physician.AddressId = newAddress.AddressId;
            }
            */

            Physician.LastModified = DateTime.Now;
            _context.Physicians.Add(Physician);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}