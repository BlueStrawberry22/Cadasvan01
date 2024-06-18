using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.ViewModel;
using System.Threading.Tasks;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Authorize("Motorista")]
    public class MotoristaVanController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;

        public MotoristaVanController(UserManager<Usuario> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SelecionarVan(string van)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.VanSelecionada = van;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            return Ok(new { VanSelecionada = user.VanSelecionada });
        }

        [HttpGet]
        public IActionResult AdicionarVan()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarVan(MotoristaVanViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (string.IsNullOrEmpty(user.ModeloVan1))
                    {
                        user.ModeloVan1 = model.Modelo;
                        user.CorVan1 = model.Cor;
                        user.PlacaVan1 = model.Placa;
                    }
                    else
                    {
                        user.ModeloVan2 = model.Modelo;
                        user.CorVan2 = model.Cor;
                        user.PlacaVan2 = model.Placa;
                    }
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Motorista");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarVan(string van)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (user.ModeloVan1 == van)
                {
                    user.ModeloVan1 = null;
                    user.CorVan1 = null;
                    user.PlacaVan1 = null;
                }
                else if (user.ModeloVan2 == van)
                {
                    user.ModeloVan2 = null;
                    user.CorVan2 = null;
                    user.PlacaVan2 = null;
                }
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
