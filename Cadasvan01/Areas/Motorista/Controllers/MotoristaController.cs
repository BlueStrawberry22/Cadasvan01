using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Cadasvan01.Services;
using Cadasvan01.Data;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            var today = DateTime.Today;
            var motorista = await _context.Usuarios
                .Include(u => u.Alunos)
                .ThenInclude(a => a.Cidade)
                .FirstOrDefaultAsync(u => u.Id == motoristaId);

            if (motorista == null)
            {
                return NotFound();
            }

            var presencasHoje = await _context.Presencas
               .Include(p => p.Usuario)
               .Where(p => p.MotoristaId == motoristaId && p.DataViagem.Date == today)
               .ToListAsync();

            var presencasOrdenadas = presencasHoje
                .OrderByDescending(p => p.ConfirmadoIda && p.ConfirmadoVolta)
                .ThenByDescending(p => p.ConfirmadoIda)
                .ThenByDescending(p => p.ConfirmadoVolta)
                .ToList();

            var model = new MotoristaIndexViewModel
            {
                Motorista = motorista,
                AlunosVinculados = motorista.Alunos.ToList(),
                PresencasHoje = presencasOrdenadas
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

        [HttpPost]
        public async Task<IActionResult> DesvincularAluno(string alunoId)
        {
            var motoristaId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (motoristaId == null)
            {
                return Unauthorized();
            }

            var aluno = await _context.Usuarios.FirstOrDefaultAsync(a => a.Id == alunoId && a.Tipo == UsuarioEnum.Aluno && a.MotoristaId == motoristaId);
            if (aluno == null)
            {
                return NotFound();
            }

            aluno.MotoristaId = null;
            await _context.SaveChangesAsync();

            return RedirectToAction("AlunosVinculados");
        }
    }




    public class MotoristaIndexViewModel
    {
        public Usuario Motorista { get; set; }
        public List<Usuario> AlunosVinculados { get; set; }
        public List<Presenca> PresencasHoje { get; set; }
    }

}