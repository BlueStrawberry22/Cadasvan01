using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Cadasvan01.Data;
using Microsoft.AspNetCore.Authorization;

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Roles = "Aluno")]
    public class AlunoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public AlunoController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var alunoId = _userManager.GetUserId(User);
            var aluno = await _context.Usuarios
                .Include(u => u.Cidade)
                .FirstOrDefaultAsync(u => u.Id == alunoId);

            if (aluno == null)
            {
                return NotFound();
            }

            Usuario motorista = null;
            List<Aviso> avisos = new List<Aviso>();

            if (!string.IsNullOrEmpty(aluno.MotoristaId))
            {
                motorista = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == aluno.MotoristaId);
                avisos = await _context.Avisos
                    .Where(a => a.MotoristaId == aluno.MotoristaId)
                    .OrderByDescending(a => a.DataPublicacao)
                    .ToListAsync();
            }

            var model = new AlunoIndexViewModel
            {
                Aluno = aluno,
                Motorista = motorista,
                Avisos = avisos
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> InfosMotorista(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var motorista = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (motorista == null)
            {
                return NotFound();
            }

            return View(motorista);
        }
    }


    public class AlunoIndexViewModel
    {
        public Usuario Aluno { get; set; }
        public Usuario Motorista { get; set; }
        public List<Aviso> Avisos { get; set; }
    }
}
