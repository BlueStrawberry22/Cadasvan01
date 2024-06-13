using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Cadasvan01.Models;
using Cadasvan01.Data;

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Roles = "Aluno")]
    public class AlunoAvisosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public AlunoAvisosController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Método existente
        public async Task<IActionResult> Index()
        {
            var alunoId = _userManager.GetUserId(User);
            var aluno = await _context.Usuarios.FindAsync(alunoId);
            if (aluno == null || aluno.MotoristaId == null)
            {
                return NotFound();
            }

            var avisos = await _context.Avisos
                .Where(a => a.MotoristaId == aluno.MotoristaId)
                .ToListAsync();

            return PartialView("_AlunoAvisosPartial", avisos);
        }

        // Novo método para verificar o status
        public async Task<IActionResult> VerificarStatus()
        {
            var avisos = await _context.Avisos.ToListAsync();
            return View(avisos);
        }

        [HttpPost]
        public async Task<IActionResult> MarcarComoLido([FromBody] int id)
        {
            var aviso = await _context.Avisos.FindAsync(id);
            if (aviso == null)
            {
                return NotFound();
            }

            aviso.Lido = true;
            _context.Update(aviso);
            await _context.SaveChangesAsync();

            return Ok();
        }
        public async Task<List<Aviso>> ObterAvisosNaoLidos()
        {
            var alunoId = _userManager.GetUserId(User);
            var aluno = await _context.Usuarios.FindAsync(alunoId);

            if (aluno == null || aluno.MotoristaId == null)
            {
                return new List<Aviso>();
            }

            return await _context.Avisos
                .Where(a => a.MotoristaId == aluno.MotoristaId && !a.Lido)
                .ToListAsync();
        }

    }
}
