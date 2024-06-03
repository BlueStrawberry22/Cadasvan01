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

        // GET: Motorista/MotoristaPresencas
        public async Task<IActionResult> Index()
        {

            var motoristaID = _userManager.GetUserId(User);
            var motorista = await _context.Usuarios.FindAsync(motoristaID);
            if (motorista == null || motorista.MotoristaId == null)
            {
                return NotFound();
            }

            var presencas = await _context.Presencas
                .Where(a => a.UsuarioId == motorista.MotoristaId)
                .ToListAsync();

            var applicationDbContext = _context.Presencas.Include(p => p.Motorista).Include(p => p.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // está comentado pois primerio precisamos ver se está funcionando o jeito casual 


     /*   public async Task<IActionResult> Details(int? id)
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
        }*/

    }
}
