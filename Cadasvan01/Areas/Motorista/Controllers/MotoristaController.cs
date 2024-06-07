using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Authorize(Roles = "Motorista")]
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
            var today = DateTime.Today;

            var motorista = await _context.Usuarios
                .Include(u => u.Alunos)
                .FirstOrDefaultAsync(u => u.Id == motoristaId);

            if (motorista == null)
            {
                return NotFound();
            }

            var presencasHoje = await _context.Presencas
                .Include(p => p.Usuario)
                .Where(p => p.MotoristaId == motoristaId && p.DataViagem.Date == today)
                .ToListAsync();

            var model = new MotoristaIndexViewModel
            {
                Motorista = motorista,
                AlunosVinculados = motorista.Alunos.ToList(),
                PresencasHoje = presencasHoje
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AlunosVinculados()
        {
            var motoristaId = _userManager.GetUserId(User);
            var motorista = await _context.Usuarios
                .Include(m => m.Alunos)
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

        [HttpPost]
        public async Task<IActionResult> DesvincularAluno(string alunoId)
        {
            var motoristaId = _userManager.GetUserId(User);
            var aluno = await _context.Usuarios.FirstOrDefaultAsync(a => a.Id == alunoId && a.MotoristaId == motoristaId);

            if (aluno == null)
            {
                return NotFound();
            }

            aluno.MotoristaId = null;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AlunosVinculados));
        }
    }

    public class MotoristaIndexViewModel
    {
        public Usuario Motorista { get; set; }
        public List<Usuario> AlunosVinculados { get; set; }
        public List<Presenca> PresencasHoje { get; set; }
    }
}
