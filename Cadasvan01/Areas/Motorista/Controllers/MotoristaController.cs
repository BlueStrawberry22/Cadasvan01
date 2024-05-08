using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    public class MotoristaController : Controller
    {
        [Area("Motorista")]
        [Authorize(Roles = "Motorista")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
