using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Identity;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    public class MotoristaPresencasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public MotoristaPresencasController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var motoristaId = _userManager.GetUserId(User);

            var presencasMotorista = await _context.Presencas
                .Include(p => p.Motorista)
                .Include(p => p.Usuario)
                .Where(p => p.MotoristaId == motoristaId) 
                .ToListAsync();

            return View(presencasMotorista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presenca = await _context.Presencas
                .Include(p => p.Motorista)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PresencaId == id);
            if (presenca == null)
            {
                return NotFound();
            }

            return View(presenca);
        }

    }
}
