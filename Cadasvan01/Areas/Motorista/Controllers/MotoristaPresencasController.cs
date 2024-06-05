using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Cadasvan01.Models;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    public class MotoristaPresencasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotoristaPresencasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Motorista/MotoristaPresencas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Presencas.Include(p => p.Motorista).Include(p => p.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Motorista/MotoristaPresencas/Details/5
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
