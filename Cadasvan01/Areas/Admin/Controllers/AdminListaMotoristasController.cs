using Cadasvan01.Data;
using Cadasvan01.Enums;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cadasvan01.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminListaMotoristasController : Controller
    {
        public readonly UserManager<Usuario> _userManager;
        public readonly ApplicationDbContext _context;

        public AdminListaMotoristasController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListaMotoristas()
        {
            var motoristas = _context.Usuarios
                .Where(u => u.Tipo == UsuarioEnum.Motorista)
                .ToList();

            return View(motoristas);
        }

        public IActionResult EditarMotorista(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorista = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == id);
            if (motorista == null)
            {
                return NotFound();
            }

            return View(motorista);
        }

        public IActionResult DetalhesMotorista(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorista = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == id);
            if (motorista == null)
            {
                return NotFound();
            }

            return View(motorista);
        }

        public IActionResult DeletarMotorista(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorista = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == id);
            if (motorista == null)
            {
                return NotFound();
            }

            return View(motorista);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletarConfirmado(string id)
        {
            var motorista = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == id);
            if (motorista == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(motorista);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
