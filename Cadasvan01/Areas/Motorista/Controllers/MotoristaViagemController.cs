using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Cadasvan01.ViewModel;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Authorize(Roles = "Motorista")]
    public class MotoristaViagemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public MotoristaViagemController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Motorista/MotoristaViagem
        public async Task<IActionResult> Index()
        {
            var motoristaId = _userManager.GetUserId(User);
            var viagens = await _context.Viagens
                .Where(v => v.MotoristaId == motoristaId && v.Ativa)
                .ToListAsync();

            var motorista = await _context.Usuarios.FindAsync(motoristaId);

            var viewModel = new MotoristaViagemViewModel
            {
                Viagens = viagens,
                VanSelecionada = motorista.VanSelecionada // Preencha a propriedade VanSelecionada
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> IniciarViagem(string destino)
        {
            var motoristaId = _userManager.GetUserId(User);
            var motorista = await _context.Usuarios.FindAsync(motoristaId);

            var viagem = new Viagem
            {
                MotoristaId = motoristaId,
                Destino = destino,
                HoraInicio = DateTime.Now,
                Ativa = true,
                VanSelecionada = motorista.VanSelecionada // Adicione a van selecionada à viagem
            };

            _context.Add(viagem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> EncerrarViagem(int id)
        {
            var viagem = await _context.Viagens.FindAsync(id);
            if (viagem == null)
            {
                return NotFound();
            }

            viagem.HoraFim = DateTime.Now;
            viagem.Ativa = false;
            _context.Update(viagem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
