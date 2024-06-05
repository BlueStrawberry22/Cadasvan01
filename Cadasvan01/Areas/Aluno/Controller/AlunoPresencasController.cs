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

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    public class AlunoPresencasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public AlunoPresencasController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            var alunoId = _userManager.GetUserId(User);

            var presencasAluno = await _context.Presencas
                .Include(p => p.Motorista)
                .Include(p => p.Usuario)
                .Where(p => p.UsuarioId == alunoId)
                .ToListAsync();

            return View(presencasAluno);
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

        public IActionResult Create()
        {
            var alunoId = _userManager.GetUserId(User);

            // Buscar o motorista vinculado ao aluno
            var aluno = _context.Usuarios
                .Include(u => u.Motorista)
                .FirstOrDefault(u => u.Id == alunoId);

            if (aluno?.MotoristaId == null)
            {
                // Caso não haja motorista vinculado, redirecionar ou exibir mensagem de erro
                return RedirectToAction("Index", "Home");
            }

            var presenca = new Presenca
            {
                UsuarioId = alunoId,
                MotoristaId = aluno.MotoristaId // Atribuir automaticamente o motorista vinculado
            };

            // Passar o motorista vinculado para a View
            ViewData["MotoristaNome"] = aluno.Motorista.NomeCompleto;

            return View(presenca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Presenca presenca)
        {
            var alunoId = _userManager.GetUserId(User);

            presenca.UsuarioId = alunoId;

            // Buscar o motorista vinculado ao aluno
            var aluno = _context.Usuarios
                .Include(u => u.Motorista)
                .FirstOrDefault(u => u.Id == alunoId);

            if (aluno?.MotoristaId == null)
            {
                // Caso não haja motorista vinculado, redirecionar ou exibir mensagem de erro
                return RedirectToAction("Index", "Home");
            }

            presenca.MotoristaId = aluno.MotoristaId;

            if (ModelState.IsValid)
            {
                _context.Add(presenca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Passar o motorista vinculado para a View
            ViewData["MotoristaNome"] = aluno.Motorista.NomeCompleto;

            return View(presenca);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presenca = await _context.Presencas.FindAsync(id);
            if (presenca == null)
            {
                return NotFound();
            }

            var alunoId = _userManager.GetUserId(User);

            if (presenca.UsuarioId != alunoId)
            {
                return Unauthorized();
            }

            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto", presenca.MotoristaId);

            ViewData["UsuarioId"] = alunoId;

            return View(presenca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresencaId,UsuarioId,MotoristaId,DataViagem,ConfirmaIda,ConfirmaVolta")] Presenca presenca)
        {
            if (id != presenca.PresencaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presenca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresencaExists(presenca.PresencaId))
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
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto", presenca.MotoristaId);

            return View(presenca);
        }

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presenca = await _context.Presencas.FindAsync(id);
            if (presenca != null)
            {
                _context.Presencas.Remove(presenca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresencaExists(int id)
        {
            return _context.Presencas.Any(e => e.PresencaId == id);
        }
    }
}
