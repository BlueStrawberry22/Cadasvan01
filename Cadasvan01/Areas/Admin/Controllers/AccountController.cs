using Cadasvan01.Data;
using Cadasvan01.Extensions;
using Cadasvan01.Models;
using Cadasvan01.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cadasvan01.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminAccountController : Controller
    {
        public readonly UserManager<Usuario> _userManager;
        public readonly ApplicationDbContext _context;

        public AdminAccountController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            //recupera os tipos do enum e monta um selectlist
            ViewData["SelectTipo"] = SelectListExtensions.MontarSelectListParaEnum(new Usuario().Tipo);

            //consulta nas cidades em ordem alfabética
            var cidades = await _context.Cidades.OrderBy(o => o.Nome).ToListAsync();

            //monta um selectList com as cidades
            ViewData["Cidades"] = new SelectList(cidades, "CidadeId", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NomeCompleto = model.NomeCompleto,
                    CPF = model.CPF,
                    Tipo = Enums.UsuarioEnum.Motorista,
                    Placa = model.Placa,
                    CNH = model.CNH,
                    CidadeId = model.CidadeId,
                    Endereco = model.Endereco
                    
                };
                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded) 
                {
                    await _userManager.AddToRoleAsync(user, "Motorista");
                    return RedirectToAction("Index", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["SelectTipo"] = SelectListExtensions.MontarSelectListParaEnum(new Usuario().Tipo);
            var cidades = await _context.Cidades.OrderBy(o => o.Nome).ToListAsync();
            ViewData["Cidades"] = new SelectList(cidades, "CidadeId", "Nome");

            return View();
        }
    }
}
