using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Cadasvan01.Models;

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    public class AlunoPresencasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunoPresencasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aluno/AlunoPresencas
        public async Task<IActionResult> Index()
        {
            var motoristaID = "motoristaID";
            var usuariosConfirmados = await _context.Presencas.Where(w => w.DataViagem == DateTime.Now)
                .ToListAsync();

            var applicationDbContext = _context.Presencas.Include(p => p.Motorista).Include(p => p.Usuario);
            return View(await applicationDbContext.ToListAsync());
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

        // GET: Aluno/AlunoPresencas/Create
        public IActionResult Create()
        {
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto"); ;
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Aluno), "Id", "NomeCompleto");
            return View();
        }

        // POST: Aluno/AlunoPresencas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresencaId,UsuarioId,MotoristaId,DataViagem,ConfirmaIda,ConfirmaVolta")] Presenca presenca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(presenca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", presenca.MotoristaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", presenca.UsuarioId);
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
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto"); ;
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Aluno), "Id", "NomeCompleto");
            return View(presenca);
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
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", presenca.MotoristaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NomeCompleto", presenca.UsuarioId);
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
