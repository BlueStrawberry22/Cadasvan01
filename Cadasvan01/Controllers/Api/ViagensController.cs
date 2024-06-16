using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Cadasvan01.Services;

namespace Cadasvan01.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Motorista")]
    public class ViagensController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHubContext<ViagemHub> _hubContext;

        public ViagensController(ApplicationDbContext context, UserManager<Usuario> userManager, IHubContext<ViagemHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        [HttpPost("Iniciar")]
        public async Task<IActionResult> IniciarViagem([FromBody] string destino)
        {
            var motoristaId = _userManager.GetUserId(User);
            if (motoristaId == null)
            {
                return Unauthorized();
            }

            var viagem = new Viagem
            {
                MotoristaId = motoristaId,
                Destino = destino,
                HoraInicio = DateTime.Now,
                Ativa = true
            };

            _context.Viagens.Add(viagem);
            await _context.SaveChangesAsync();

            // Notificar os alunos via SignalR
            await _hubContext.Clients.All.SendAsync("ReceberNotificacao", "Seu motorista iniciou uma viagem!");

            return Ok(viagem);
        }

        [HttpPost("Encerrar/{id}")]
        public async Task<IActionResult> EncerrarViagem(int id)
        {
            var viagem = await _context.Viagens.FindAsync(id);
            if (viagem == null)
            {
                return NotFound();
            }

            viagem.HoraFim = DateTime.Now;
            viagem.Ativa = false;

            _context.Viagens.Update(viagem);
            await _context.SaveChangesAsync();

            // Notificar os alunos via SignalR
            await _hubContext.Clients.All.SendAsync("ReceberNotificacao", "Seu motorista encerrou a viagem!");

            return Ok(viagem);
        }

        [HttpGet("Localizacao")]
        public async Task<IActionResult> ObterLocalizacaoMotorista(string motoristaId)
        {
            var motorista = await _context.Usuarios.FindAsync(motoristaId);
            if (motorista == null)
            {
                return NotFound();
            }

            return Ok(new { latitude = motorista.Latitude, longitude = motorista.Longitude });
        }

        [HttpPost("AtualizarLocalizacao")]
        public async Task<IActionResult> AtualizarLocalizacao([FromBody] Coordenadas coordenadas)
        {
            var motoristaId = _userManager.GetUserId(User);
            if (motoristaId == null)
            {
                return Unauthorized();
            }

            var motorista = await _context.Usuarios.FindAsync(motoristaId);
            if (motorista == null)
            {
                return NotFound();
            }

            motorista.Latitude = coordenadas.Latitude;
            motorista.Longitude = coordenadas.Longitude;

            _context.Usuarios.Update(motorista);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

    public class Coordenadas
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
