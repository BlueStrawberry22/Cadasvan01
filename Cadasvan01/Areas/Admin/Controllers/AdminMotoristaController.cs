using Cadasvan01.Data;
using Cadasvan01.Enums;
using Cadasvan01.Extensions;
using Cadasvan01.Models;
using Cadasvan01.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cadasvan01.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMotoristaController : Controller
    {
        public readonly UserManager<Usuario> _userManager;
        public readonly ApplicationDbContext _context;

        public AdminMotoristaController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        // Ação para criar um perfil de motorista
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewData["SelectTipo"] = SelectListExtensions.MontarSelectListParaEnum(new Usuario().Tipo);

            var cidades = await _context.Cidades.OrderBy(o => o.Nome).ToListAsync();

            ViewData["Cidades"] = new SelectList(cidades, "CidadeId", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MotoristaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var motorista = new Usuario
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NomeCompleto = model.NomeCompleto,
                    CPF = model.CPF,
                    Tipo = model.Tipo,
                    CNH = model.CNH,
                    Placa = model.Placa,
                    CidadeId = model.CidadeId,
                    Endereco = model.Endereco
                };

                var result = await _userManager.CreateAsync(motorista, model.Senha);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(motorista, "Motorista");

                    return RedirectToAction("Index", "Motorista");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
       
    }

}
