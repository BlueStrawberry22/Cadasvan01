using Cadasvan01.Data;
using Cadasvan01.Enums;
using Cadasvan01.Extensions;
using Cadasvan01.Models;
using Cadasvan01.Services;
using Cadasvan01.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cadasvan01.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly ViaCEPService _viaCEPService;

        public AccountController(ApplicationDbContext context,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IWebHostEnvironment webHostEnviroment,
            ViaCEPService viaCEPService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnviroment = webHostEnviroment;
            _viaCEPService = viaCEPService;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Senha, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (await _userManager.IsInRoleAsync(user, "Motorista"))
                {
                    return RedirectToAction("Index", "Motorista");
                }
                else if (await _userManager.IsInRoleAsync(user, "Aluno"))
                {
                    return RedirectToAction("Index", "Aluno");
                }
                else
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEndereco(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                return BadRequest("CEP não pode ser nulo ou vazio.");
            }

            var endereco = await _viaCEPService.ConsultarCEP(cep);

            if (endereco == null)
            {
                return NotFound("Endereço não encontrado para o CEP fornecido.");
            }

            return Ok(endereco);
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImagemPerfil != null)
                {
                    string folder = "images/perfil";
                    folder += Guid.NewGuid().ToString() + "_" + model.ImagemPerfil.FileName;

                    model.CaminhoImagemPerfil = "/" + folder;

                    string serverFolder = Path.Combine(_webHostEnviroment.WebRootPath, folder);

                    await model.ImagemPerfil.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                var user = new Usuario
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NomeCompleto = model.NomeCompleto,
                    CPF = model.CPF,
                    Tipo = UsuarioEnum.Aluno,
                    Placa = model.Placa ?? string.Empty,
                    CidadeId = model.CidadeId,
                    CEP = model.CEP,
                    Celular1 = model.Celular1,
                    Celular2 = model.Celular2,
                    Endereco = model.Endereco,
                    CaminhoImagemPerfil = model.CaminhoImagemPerfil,
                };

                var result = await _userManager.CreateAsync(user, model.Senha);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Aluno");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Aluno");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["SelectTipo"] = SelectListExtensions.MontarSelectListParaEnum(new Usuario().Tipo);
            var cidades = await _context.Cidades.OrderBy(o => o.Nome).ToListAsync();
            ViewData["Cidades"] = new SelectList(cidades, "CidadeId", "Nome");

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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