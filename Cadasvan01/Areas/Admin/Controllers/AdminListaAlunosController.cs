using Cadasvan01.Data;
using Cadasvan01.Enums;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cadasvan01.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminListaAlunosController : Controller
    {
        public readonly UserManager<Usuario> _userManager;
        public readonly ApplicationDbContext _context;

        public AdminListaAlunosController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult ListaAlunos()
        {
            var alunos = _context.Usuarios
                .Where(u => u.Tipo == UsuarioEnum.Aluno)
                .ToList();

            return View(alunos);
        }

        public IActionResult DetalhesAlunos(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == id);
            if (aluno== null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        public IActionResult DeletarAluno(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletarConfirmado(string id)
        {
            var aluno = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == id);
            if (aluno== null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(aluno);
            _context.SaveChanges();

            return RedirectToAction("ListaAlunos", "AdminListaAlunos");
        }
    }
}
