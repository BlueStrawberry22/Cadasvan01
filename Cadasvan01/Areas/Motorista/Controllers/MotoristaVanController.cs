using System.IO;
using System.Threading.Tasks;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Cadasvan01.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Authorize("Motorista")]
    public class MotoristaVanController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MotoristaVanController(UserManager<Usuario> userManager, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
                    if (model.Foto != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/vans");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Foto.CopyToAsync(fileStream);
                        }
                        user.FotoVan1 = "/images/vans/" + uniqueFileName;
                        user.FotoVan2 = "/images/vans/" + uniqueFileName;
                    }

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
                    user.FotoVan1 = null; // Se houver um campo para a foto da van
                }
                else if (user.ModeloVan2 == van)
                {
                    user.ModeloVan2 = null;
                    user.CorVan2 = null;
                    user.PlacaVan2 = null;
                    user.FotoVan2 = null; // Se houver um campo para a foto da van
                }
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }


    }
}
