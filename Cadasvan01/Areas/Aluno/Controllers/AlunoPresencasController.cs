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
        // GET: Aluno/AlunoPresencas/CreatePartial
        public IActionResult CreatePartial()
        {
            var alunoId = _userManager.GetUserId(User);
            var aluno = _context.Usuarios
                .Include(u => u.Motorista)
                .FirstOrDefault(u => u.Id == alunoId);

            if (aluno?.MotoristaId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var presenca = new Presenca
            {
                UsuarioId = alunoId,
                MotoristaId = aluno.MotoristaId,
                DataViagem = DateTime.Now
            };

            ViewData["MotoristaNome"] = aluno.Motorista.Nome;

            return PartialView("_PresencaAlunoPartial", presenca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePartial(Presenca presenca)
        {
            var alunoId = _userManager.GetUserId(User);
            presenca.UsuarioId = alunoId;

            var aluno = _context.Usuarios
                .Include(u => u.Motorista)
                .FirstOrDefault(u => u.Id == alunoId);

            if (aluno?.MotoristaId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            presenca.MotoristaId = aluno.MotoristaId;

            if (presenca.DataViagem == DateTime.MinValue)
            {
                presenca.DataViagem = DateTime.Now.Date;
            }

            if (ModelState.IsValid)
            {
                _context.Add(presenca);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Aluno");
            }

            ViewData["MotoristaNome"] = aluno.Motorista.Nome;
            return PartialView("_PresencaAlunoPartial", presenca);
        }


        // GET: Aluno/AlunoPresencas
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

        // GET: Aluno/AlunoPresencas/Details/5
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

        

        // GET: Aluno/AlunoPresencas/Edit/5
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

            var aluno = await _context.Usuarios.Include(u => u.Motorista).FirstOrDefaultAsync(u => u.Id == alunoId);

            if (aluno?.MotoristaId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["MotoristaNome"] = aluno.Motorista.Nome;
            ViewData["UsuarioId"] = alunoId;

            return View(presenca); ;
        }

        // POST: Aluno/AlunoPresencas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresencaId,UsuarioId,MotoristaId,DataViagem,ConfirmaIda,ConfirmaVolta")] Presenca presenca)
        {
            if (id != presenca.PresencaId)
            {
                return NotFound();
            }

            var alunoId = _userManager.GetUserId(User);

            var aluno = await _context.Usuarios.Include(u => u.Motorista).FirstOrDefaultAsync(u => u.Id == alunoId);

            if (aluno?.MotoristaId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            presenca.UsuarioId = alunoId;
            presenca.MotoristaId = aluno.MotoristaId;

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

            ViewData["MotoristaNome"] = aluno.Motorista.Nome;
            return View(presenca);
        }

        // GET: Aluno/AlunoPresencas/Delete/5
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

        // POST: Aluno/AlunoPresencas/Delete/5
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