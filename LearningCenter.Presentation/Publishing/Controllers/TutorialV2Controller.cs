using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class TutorialV2Controller : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}