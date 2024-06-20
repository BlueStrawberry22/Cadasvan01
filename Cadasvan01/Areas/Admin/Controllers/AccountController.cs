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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing; 
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Cadasvan01.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly ViaCEPService _viaCEPService;

        public AdminAccountController(ApplicationDbContext context,
            UserManager<Usuario> userManager,
            IWebHostEnvironment webHostEnviroment,
            ViaCEPService viaCEPService)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnviroment = webHostEnviroment;
            _viaCEPService = viaCEPService;
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
        public async Task<IActionResult> Register(MotoristaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    CNH = model.CNH,
                    Tipo = Enums.UsuarioEnum.Motorista,
                    CidadeId = model.CidadeId,
                    Itinerario = model.Itinerario,
                    Celular1 = model.Celular1,
                    Celular2 = model.Celular2,
                    Vans = new List<Van>()
                };

                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Motorista");

                    foreach (var vanModel in model.Vans)
                    {
                        if (vanModel.Foto != null)
                        {
                            string uploadsFolder = Path.Combine(_webHostEnviroment.WebRootPath, "images/vans");
                            if (!Directory.Exists(uploadsFolder))
                            {
                                Directory.CreateDirectory(uploadsFolder);
                            }

                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + vanModel.Foto.FileName;
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            using (var image = Image.Load(vanModel.Foto.OpenReadStream()))
                            {
                                image.Mutate(x => x.Resize(new ResizeOptions
                                {
                                    Size = new Size(300, 300),
                                    Mode = ResizeMode.Crop
                                }));
                                await image.SaveAsync(filePath, new JpegEncoder());
                            }

                            user.Vans.Add(new Van
                            {
                                Modelo = vanModel.Modelo,
                                Cor = vanModel.Cor,
                                Placa = vanModel.Placa,
                                Foto = "/images/vans/" + uniqueFileName,
                                MotoristaId = user.Id
                            });
                        }
                        else
                        {
                            user.Vans.Add(new Van
                            {
                                Modelo = vanModel.Modelo,
                                Cor = vanModel.Cor,
                                Placa = vanModel.Placa,
                                MotoristaId = user.Id
                            });
                        }
                    }

                    _context.Update(user);
                    await _context.SaveChangesAsync();

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

            return View(model);
        }
    }
}
