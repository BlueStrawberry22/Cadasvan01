using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Cadasvan01.Services;
using Cadasvan01.Data;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Authorize(Roles="Motorista")]
    
    public class MotoristaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public MotoristaController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var motoristaId = _userManager.GetUserId(User);
            var motorista = await _context.Usuarios
                .Include(u => u.Alunos)
                .ThenInclude(a => a.Cidade)
                .FirstOrDefaultAsync(u => u.Id == motoristaId);

            if (motorista == null)
            {
                return NotFound();
            }

            var model = new MotoristaIndexViewModel
            {
                Motorista = motorista,
                AlunosVinculados = motorista.Alunos.ToList()
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AlunosVinculados()
        {
            var motoristaId = _userManager.GetUserId(User);
            var motorista = await _context.Usuarios
                .Include(m => m.Alunos)
                    .ThenInclude(a => a.Cidade)
                .FirstOrDefaultAsync(m => m.Id == motoristaId);

            if (motorista == null)
            {
                return NotFound();
            }

            var viewModel = new MotoristaIndexViewModel
            {
                Motorista = motorista,
                AlunosVinculados = motorista.Alunos.ToList()
            };

            return View(viewModel);
        }
    }


    public class MotoristaIndexViewModel
    {
        public Usuario Motorista { get; set; }
        public List<Usuario> AlunosVinculados { get; set; }
    }

}