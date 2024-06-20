using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Cadasvan01.Services;
using Cadasvan01.Data;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

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
                .Include(u => u.Vans)
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

            var viagensAtivas = await _context.Viagens
                .Where(v => v.MotoristaId == motoristaId && v.Ativa)
                .ToListAsync();

            var model = new MotoristaIndexViewModel
            {
                Motorista = motorista,
                AlunosVinculados = motorista.Alunos.ToList(),
                PresencasHoje = presencasOrdenadas,
                ViagensAtivas = viagensAtivas,
                Vans = motorista.Vans.ToList(),
                VanSelecionada = motorista.VanSelecionada
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

        [HttpPost]
        public async Task<IActionResult> SelecionarVan(string van)
        {
            var motoristaId = _userManager.GetUserId(User);
            var motorista = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == motoristaId);

            if (motorista == null)
            {
                return NotFound();
            }

            motorista.VanSelecionada = van;
            await _context.SaveChangesAsync();

            return Ok(new { VanSelecionada = motorista.VanSelecionada });
        }
    }

    public class MotoristaIndexViewModel
    {
        public Usuario Motorista { get; set; }
        public List<Presenca> PresencasHoje { get; set; }
        public List<Usuario> AlunosVinculados { get; set; }
        public IEnumerable<Viagem> ViagensAtivas { get; set; }
        public List<Van> Vans { get; set; } 
        public string VanSelecionada { get; set; }
    }

}
