using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Mvc;

namespace IS_Proj_HIT.Controllers
{
    public class PCAController : Controller
    {
        public IActionResult Index() => View();

        //todo: Expand this to display the form to enter a PCA, parameters tenative
        public IActionResult CreateAssessment(Encounter encounter, Patient patient) => View();

        //todo: Expand this method to display the form to update an existing PCA, parameters are tenative
        public IActionResult UpdateAssessment(Pcarecord pca,Encounter encounter, Patient patient) => View();
    }
}