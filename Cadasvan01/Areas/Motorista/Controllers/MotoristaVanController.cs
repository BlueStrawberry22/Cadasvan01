using System.IO;
using System.Threading.Tasks;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Cadasvan01.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

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
                    var van = new Van
                    {
                        Modelo = model.Modelo,
                        Cor = model.Cor,
                        Placa = model.Placa,
                        MotoristaId = user.Id
                    };

                    if (model.Foto != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/vans");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var image = Image.Load(model.Foto.OpenReadStream()))
                        {
                            // Redimensiona a imagem
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Size = new Size(300, 300), // Define o tamanho desejado
                                Mode = ResizeMode.Crop
                            }));

                            // Salva a imagem redimensionada
                            await image.SaveAsync(filePath, new JpegEncoder());
                        }

                        van.Foto = "/images/vans/" + uniqueFileName;
                    }

                    _context.Vans.Add(van);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Motorista");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditarVan(int id)
        {
            var van = await _context.Vans.FindAsync(id);
            if (van == null)
            {
                return NotFound();
            }

            var model = new MotoristaVanViewModel
            {
                Id = van.Id,
                Modelo = van.Modelo,
                Cor = van.Cor,
                Placa = van.Placa,
                CaminhoFotoVan = van.Foto
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditarVan(MotoristaVanViewModel model)
        {
            if (ModelState.IsValid)
            {
                var van = await _context.Vans.FindAsync(model.Id);
                if (van == null)
                {
                    return NotFound();
                }

                van.Modelo = model.Modelo;
                van.Cor = model.Cor;
                van.Placa = model.Placa;

                if (model.Foto != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/vans");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var image = Image.Load(model.Foto.OpenReadStream()))
                    {
                        // Redimensiona a imagem
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(300, 300), // Define o tamanho desejado
                            Mode = ResizeMode.Crop
                        }));

                        // Salva a imagem redimensionada
                        await image.SaveAsync(filePath, new JpegEncoder());
                    }

                    van.Foto = "/images/vans/" + uniqueFileName;
                }

                _context.Update(van);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Motorista");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarVan(int id)
        {
            var van = await _context.Vans.FindAsync(id);
            if (van != null)
            {
                _context.Vans.Remove(van);
                await _context.SaveChangesAsync();
            }
            return Ok();
        } 
    }
}
