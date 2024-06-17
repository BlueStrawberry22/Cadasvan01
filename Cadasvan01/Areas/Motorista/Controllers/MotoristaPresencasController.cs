using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Identity;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Route("Motorista/[controller]")]
    public class MotoristaPresencasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public MotoristaPresencasController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<List<Usuario>> GetAlunosVinculados(string motoristaId)
        {
            return await _context.Usuarios
                .Where(u => u.MotoristaId == motoristaId)
                .ToListAsync();
        }

        [HttpGet("Historico")]
        public async Task<IActionResult> Historico(string alunoId)
        {
            var motoristaId = _userManager.GetUserId(User);
            var alunosVinculados = await GetAlunosVinculados(motoristaId);
            ViewBag.AlunosVinculados = alunosVinculados;

            var query = _context.Presencas
                .Include(p => p.Motorista)
                .Include(p => p.Usuario)
                .Where(p => p.MotoristaId == motoristaId);

            if (!string.IsNullOrEmpty(alunoId))
            {
                query = query.Where(p => p.UsuarioId == alunoId);
            }

            var historicoPresencas = await query
                .OrderBy(p => p.DataViagem)
                .ToListAsync();

            return View(historicoPresencas);
        }
    }
}
