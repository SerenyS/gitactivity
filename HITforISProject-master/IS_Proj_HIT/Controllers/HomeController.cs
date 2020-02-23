using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using isprojectHiT.Models;

namespace IS_Proj_HIT.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index() => View();

        public IActionResult PatientLookup() => View();

        public IActionResult BritRedirect() => Redirect("https://hcsdev.wctc.edu:4443");

        public IActionResult Privacy() => View();

    }
}
