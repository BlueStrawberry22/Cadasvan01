using Cadasvan01.Data;
using Cadasvan01.Extensions;
using Cadasvan01.Models;
using Cadasvan01.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Models;


namespace Cadasvan01.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        public readonly UserManager<Usuario> _userManager;
        public readonly SignInManager<Usuario> _signInManager;
        public readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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
                //criando um usuario com os dados que vem da viewModel
                var user = new Usuario
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NomeCompleto = model.NomeCompleto,
                    CPF = model.CPF,
                    Tipo = Enums.UsuarioEnum.Motorista,
                    CNH = model.CNH,
                    Placa = model.Placa,
                    CidadeId = model.CidadeId,
                    Endereco = model.Endereco

                };
                //usando identity para criar usuário
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
            return View();
        }
    }
}
