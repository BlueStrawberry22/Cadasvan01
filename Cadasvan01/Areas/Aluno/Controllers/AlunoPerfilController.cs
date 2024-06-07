using Cadasvan01.Data;
using Cadasvan01.Models;
using Cadasvan01.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Authorize(Roles = "Aluno")]
    [Area("Aluno")]
    public class AlunoPerfilController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AlunoPerfilController(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Nome= user.Nome,
                CPF = user.CPF,
                CidadeId = user.CidadeId,
                Endereco = user.Endereco,
                CaminhoImagemPerfil = user.CaminhoImagemPerfil
            };

            var cidades = await _context.Cidades.OrderBy(o => o.Nome).ToListAsync();
            ViewData["Cidades"] = new SelectList(cidades, "CidadeId", "Nome");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var cidades = await _context.Cidades.OrderBy(o => o.Nome).ToListAsync();
                ViewData["Cidades"] = new SelectList(cidades, "CidadeId", "Nome");
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            user.Nome= model.Nome;
            user.CPF = model.CPF;
            user.CidadeId = model.CidadeId;
            user.Endereco = model.Endereco;

            if (model.ImagemPerfil != null)
            {
                string folder = "images/perfil";
                folder += Guid.NewGuid().ToString() + "_" + model.ImagemPerfil.FileName;

                model.CaminhoImagemPerfil = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                {
                    await model.ImagemPerfil.CopyToAsync(fileStream);
                }

                user.CaminhoImagemPerfil = model.CaminhoImagemPerfil;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            var cidadesList = await _context.Cidades.OrderBy(o => o.Nome).ToListAsync();
            ViewData["Cidades"] = new SelectList(cidadesList, "CidadeId", "Nome");

            return View(model);
        }
    }
}
