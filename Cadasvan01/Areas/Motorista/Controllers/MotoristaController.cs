using Microsoft.AspNetCore.Mvc;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    public class MotoristaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
