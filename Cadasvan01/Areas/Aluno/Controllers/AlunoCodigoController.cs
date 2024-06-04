using Cadasvan01.Data;
using Cadasvan01.Enums;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Roles = "Aluno")]
    public class AlunoCodigoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<AlunoCodigoController> _logger;

        public AlunoCodigoController(ApplicationDbContext context, UserManager<Usuario> userManager, ILogger<AlunoCodigoController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult VincularMotorista()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VincularMotorista(string codigo)
        {
            _logger.LogInformation("Post VincularMotorista called with code: {Codigo}", codigo);

            var codigoVinculacao = _context.CodigosVinculacao.FirstOrDefault(c => c.Codigo == codigo);
            if (codigoVinculacao != null)
            {
                var alunoId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (alunoId == null)
                {
                    return Unauthorized();
                }

                var aluno = _context.Usuarios.FirstOrDefault(a => a.Id == alunoId && a.Tipo == UsuarioEnum.Aluno);
                if (aluno == null)
                {
                    return NotFound();
                }

                aluno.MotoristaId = codigoVinculacao.MotoristaId;

                _context.CodigosVinculacao.Remove(codigoVinculacao);
                _context.SaveChanges();

                return RedirectToAction("Index", "Aluno");
            }

            _logger.LogWarning("Invalid code: {Codigo}", codigo);
            ModelState.AddModelError("", "Código inválido.");
            return View();
        }

        [HttpPost]
        public IActionResult DesvincularMotorista()
        {
            var alunoId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (alunoId == null)
            {
                return Unauthorized();
            }

            var aluno = _context.Usuarios.FirstOrDefault(a => a.Id == alunoId && a.Tipo == UsuarioEnum.Aluno);
            if (aluno == null)
            {
                return NotFound();
            }

            aluno.MotoristaId = null;
            _context.SaveChanges();

            return RedirectToAction("Index", "Aluno");
        }
    }
}
