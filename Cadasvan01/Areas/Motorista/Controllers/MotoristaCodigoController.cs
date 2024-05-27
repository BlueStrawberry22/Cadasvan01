using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    public class MotoristaCodigoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;

        public MotoristaCodigoController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GerarCodigo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerarCodigoPost()
        {
            var motoristaId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (motoristaId == null)
            {
                return Unauthorized();
            }

            var codigo = new CodigoVinculacao
            {
                Codigo = Guid.NewGuid().ToString().Substring(0, 8),
                MotoristaId = motoristaId
            };

            _context.CodigosVinculacao.Add(codigo);
            _context.SaveChanges();

            ViewBag.Codigo = codigo.Codigo;
            return View("GerarCodigo");
        }
    }
}
