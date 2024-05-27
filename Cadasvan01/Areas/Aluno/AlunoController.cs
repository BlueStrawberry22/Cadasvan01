using Microsoft.AspNetCore.Mvc;

namespace Cadasvan01.Areas.Aluno
{
    public class AlunoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
