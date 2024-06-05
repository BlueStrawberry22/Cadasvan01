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

        // GET: Aluno/AlunoPresencas
        public async Task<IActionResult> Index()
        {
            var alunoId = _userManager.GetUserId(User);

            // Filtrar as presenças apenas para o aluno atual
            var presencasAluno = await _context.Presencas
                .Include(p => p.Motorista)
                .Include(p => p.Usuario)
                .Where(p => p.UsuarioId == alunoId) // Filtrar pelo ID do aluno
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

        // GET: Aluno/AlunoPresencas/Create
        public IActionResult Create()
        {
            // Obter o ID do aluno atualmente logado
            var alunoId = _userManager.GetUserId(User);

            // Definir o UsuarioId na ViewData para preencher o campo oculto no formulário
            ViewData["UsuarioId"] = alunoId;

            // Carregar a lista de motoristas disponíveis
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto");

            return View();
        }

        // POST: Aluno/AlunoPresencas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Presenca presenca)
        {
            // Obter o ID do aluno atualmente logado
            var alunoId = _userManager.GetUserId(User);

            // Definir o UsuarioId na presenca com o ID do aluno logado
            presenca.UsuarioId = alunoId;

            if (ModelState.IsValid)
            {
                _context.Add(presenca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Carregar a lista de motoristas disponíveis
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto", presenca.MotoristaId);

            // Como o UsuarioId é definido no método GET, não é necessário adicioná-lo aqui

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

            // Obter o ID do aluno atualmente logado
            var alunoId = _userManager.GetUserId(User);

            // Verificar se o aluno logado é o dono da presença
            if (presenca.UsuarioId != alunoId)
            {
                return Unauthorized(); // O aluno não tem permissão para editar esta presença
            }

            // Preencher ViewData apenas para motoristas vinculados ao aluno
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto", presenca.MotoristaId);

            // Definir o UsuarioId na ViewData para preencher o campo oculto no formulário
            ViewData["UsuarioId"] = alunoId;

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
            ViewData["MotoristaId"] = new SelectList(_context.Usuarios.Where(w => w.Tipo == Enums.UsuarioEnum.Motorista), "Id", "NomeCompleto", presenca.MotoristaId);

            // Como o UsuarioId é definido no método GET, não é necessário adicioná-lo aqui

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
