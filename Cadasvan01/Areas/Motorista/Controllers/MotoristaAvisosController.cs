using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Authorize(Roles = "Motorista")]
    public class MotoristaAvisosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public MotoristaAvisosController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var motoristaId = _userManager.GetUserId(User);
            var avisos = await _context.Avisos
                .Where(a => a.MotoristaId == motoristaId)
                .ToListAsync();

            return View(avisos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                aviso.MotoristaId = _userManager.GetUserId(User);
                _context.Add(aviso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aviso);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var aviso = await _context.Avisos.FindAsync(id);
            if (aviso == null)
            {
                return NotFound();
            }
            return View(aviso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Aviso aviso)
        {
            if (id != aviso.AvisoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var avisoOriginal = await _context.Avisos.AsNoTracking().FirstOrDefaultAsync(a => a.AvisoId == id);
                    if (avisoOriginal == null)
                    {
                        return NotFound();
                    }

                    aviso.MotoristaId = avisoOriginal.MotoristaId; // Preserve o MotoristaId

                    _context.Update(aviso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvisoExists(aviso.AvisoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aviso);
        }

        [HttpGet]
        public async Task<IActionResult> Deletar(int? id)
        {
            var aviso = await _context.Avisos
                .FirstOrDefaultAsync(m => m.AvisoId == id);
            if (aviso == null)
            {
                return NotFound();
            }
            return View(aviso);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirma(int id)
        {
            var aviso = await _context.Avisos.FindAsync(id);
            if (aviso != null)
            {
                _context.Avisos.Remove(aviso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvisoExists(int id)
        {
            return _context.Avisos.Any(e => e.AvisoId == id);
        }
    }
}
