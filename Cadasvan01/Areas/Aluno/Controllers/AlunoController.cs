using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Microsoft.AspNetCore.Authorization;

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Roles = "Aluno")]
    public class AlunoController : Controller
    {
        public readonly UserManager<Usuario> _userManager;
        public readonly SignInManager<Usuario> _signInManager;
        public readonly ApplicationDbContext _context;
        public readonly IWebHostEnvironment _webHostEnviroment;

        public AlunoController(ApplicationDbContext context,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnviroment = webHostEnviroment;
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
        public async Task<int> ContarAvisosNaoLidos()
        {
            var alunoId = _userManager.GetUserId(User);
            var aluno = await _context.Usuarios.FindAsync(alunoId);

            if (aluno == null || aluno.MotoristaId == null)
            {
                return 0;
            }

            return await _context.Avisos
                .Where(a => a.MotoristaId == aluno.MotoristaId && !a.Lido)
                .CountAsync();
        }

    }


    public class AlunoIndexViewModel
    {
        public Usuario Aluno { get; set; }
        public Usuario Motorista { get; set; }
        public List<Aviso> Avisos { get; set; }
    }
}
