using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Roles = "Aluno")]
    public class AlunoViagemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public AlunoViagemController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var alunoId = _userManager.GetUserId(User);
            var aluno = await _context.Usuarios.FindAsync(alunoId);

            if (aluno == null || aluno.MotoristaId == null)
            {
                return NotFound();
            }

            var viagem = await _context.Viagens
                .Where(v => v.MotoristaId == aluno.MotoristaId && v.Ativa)
                .FirstOrDefaultAsync();

            return View();
        }

        public IActionResult VerLocalizacao()
        {
            return View();
        }
    }
}
