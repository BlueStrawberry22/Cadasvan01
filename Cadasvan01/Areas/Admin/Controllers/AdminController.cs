using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Cadasvan01.Services;
using Cadasvan01.Data;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Enums;

namespace Cadasvan01.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public readonly UserManager<Usuario> _userManager;
        public readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, UserManager<Usuario> userManager)
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
    }
    
}
