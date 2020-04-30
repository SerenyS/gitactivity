using Microsoft.AspNetCore.Mvc;

namespace IS_Proj_HIT.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index() => View();

        public IActionResult BritRedirect() => Redirect("https://hcsdev.wctc.edu:4443");

        public IActionResult Privacy() => View();

    }
}
