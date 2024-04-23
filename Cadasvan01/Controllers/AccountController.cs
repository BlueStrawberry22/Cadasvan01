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
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            //logout
            await _signInManager.SignOutAsync();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _signInManager.PasswordSignInAsync(user, model.Senha, model.RememberMe, false);
            if (result.Succeeded)
            {

                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                return View();
            }
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
                    Tipo = model.Tipo,
                    CNH = model.CNH,
                    Placa = model.Placa,
                    CidadeId = model.CidadeId,
                    Endereco = model.Endereco

                };
                //usando identity para criar usuário
                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded) //se sucesso manda ele fazer login e redireciona para a home
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
