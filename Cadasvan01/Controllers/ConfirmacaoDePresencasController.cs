using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cadasvan01.Controllers
{
    [Authorize]
    public class ConfirmacaoDePresencasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfirmacaoDePresencasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConfirmacaoDePresencas

        public async Task<IActionResult> Index()
        {
            var motoristaID = "motoristaID";
            var alunoID = "AlunoId";
            var usuariosConfirmados = await _context.Presencas.Where(w => w.DataViagemConfirmada == DateTime.Now)
                .ToListAsync();


            var applicationDbContext = _context.Presencas.Include(c => c.Motorista).Include(c => c.Aluno);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ConfirmacaoDePresencas/Details/5
        [Authorize(Roles = "Aluno")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmacaoDePresenca = await _context.Presencas
                .Include(c => c.Motorista)
                .Include(c => c.Aluno)
                .FirstOrDefaultAsync(m => m.ConfirmacaoDePresencaID == id);
            if (confirmacaoDePresenca == null)
            {
                return NotFound();
            }

            return View(confirmacaoDePresenca);
        }

        // GET: ConfirmacaoDePresencas/Create
        [Authorize(Roles = "Aluno")]
        public IActionResult Create()
        {
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto"); ;
            ViewData["AlunoId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Aluno), "Id", "NomeCompleto");
            return View();
        }

        // POST: ConfirmacaoDePresencas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Aluno")]

        public async Task<IActionResult> Create(ConfirmacaoDePresenca confirmacaoDePresenca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(confirmacaoDePresenca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", confirmacaoDePresenca.MotoristaId);
            ViewData["AlunoId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", confirmacaoDePresenca.AlunoId);
            return View(confirmacaoDePresenca);
        }

        // GET: ConfirmacaoDePresencas/Edit/5
        [Authorize(Roles = "Aluno")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmacaoDePresenca = await _context.Presencas.FindAsync(id);
            if (confirmacaoDePresenca == null)
            {
                return NotFound();
            }
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", confirmacaoDePresenca.MotoristaId);
            ViewData["AlunoId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", confirmacaoDePresenca.AlunoId);
            return View(confirmacaoDePresenca);
        }

        // POST: ConfirmacaoDePresencas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Aluno")]

        public async Task<IActionResult> Edit(int id, ConfirmacaoDePresenca confirmacaoDePresenca)
        {
            if (id != confirmacaoDePresenca.ConfirmacaoDePresencaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(confirmacaoDePresenca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfirmacaoDePresencaExists(confirmacaoDePresenca.ConfirmacaoDePresencaID))
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
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", confirmacaoDePresenca.MotoristaId);
            ViewData["AlunoId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", confirmacaoDePresenca.AlunoId);
            return View(confirmacaoDePresenca);
        }

        // GET: ConfirmacaoDePresencas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmacaoDePresenca = await _context.Presencas
                .Include(c => c.Motorista)
                .Include(c => c.Aluno)
                .FirstOrDefaultAsync(m => m.ConfirmacaoDePresencaID == id);
            if (confirmacaoDePresenca == null)
            {
                return NotFound();
            }

            return View(confirmacaoDePresenca);
        }

        // POST: ConfirmacaoDePresencas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var confirmacaoDePresenca = await _context.Presencas.FindAsync(id);
            if (confirmacaoDePresenca != null)
            {
                _context.Presencas.Remove(confirmacaoDePresenca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfirmacaoDePresencaExists(int id)
        {
            return _context.Presencas.Any(e => e.ConfirmacaoDePresencaID == id);
        }
    }
}
