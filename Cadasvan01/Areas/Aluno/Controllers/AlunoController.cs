using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Cadasvan01.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Roles = "Aluno")]
    public class AlunoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AlunoController(ApplicationDbContext context,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var alunoId = _userManager.GetUserId(User);
            var aluno = await _context.Usuarios
                .Include(u => u.Cidade)
                .FirstOrDefaultAsync(u => u.Id == alunoId);

            if (aluno == null)
            {
                return NotFound();
            }

            Usuario motorista = null;
            List<Aviso> avisos = new List<Aviso>();
            Presenca presencaHoje = null;
            Viagem viagemAtiva = null;

            if (!string.IsNullOrEmpty(aluno.MotoristaId))
            {
                motorista = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == aluno.MotoristaId);
                avisos = await _context.Avisos
                    .Where(a => a.MotoristaId == aluno.MotoristaId)
                    .OrderByDescending(a => a.DataPublicacao)
                    .ToListAsync();

                viagemAtiva = await _context.Viagens
                      .FirstOrDefaultAsync(v => v.MotoristaId == aluno.MotoristaId && v.Ativa);

                var hoje = DateTime.Now.Date;
                presencaHoje = await _context.Presencas
                    .FirstOrDefaultAsync(p => p.UsuarioId == alunoId && p.DataViagem == hoje);
                
            }

            var model = new AlunoIndexViewModel
            {
                Aluno = aluno,
                Motorista = motorista,
                Avisos = avisos,
                PresencaHoje = presencaHoje,
                ViagemAtiva = viagemAtiva
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditarEndereco(EditarEnderecoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var aluno = await _userManager.GetUserAsync(User);
                if (aluno != null)
                {
                    aluno.CEP = model.CEP;
                    aluno.Endereco = model.Endereco;
                    aluno.Complemento = model.Complemento;
                    aluno.Bairro = model.Bairro;

                    var result = await _userManager.UpdateAsync(aluno);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AvaliarMotorista(AvaliacaoViewModel avaliacaoViewModel)
        {
            var alunoId = _userManager.GetUserId(User);
            var aluno = await _context.Usuarios.FindAsync(alunoId);

            if (aluno == null)
            {
                return NotFound();
            }

            var avaliacaoExistente = await _context.Avaliacoes
                .FirstOrDefaultAsync(a => a.MotoristaId == avaliacaoViewModel.MotoristaId && a.AlunoId == alunoId);

            if (avaliacaoExistente != null)
            {
                avaliacaoExistente.AvaliacaoEstrelas = avaliacaoViewModel.AvaliacaoEstrelas;
                avaliacaoExistente.Opiniao = avaliacaoViewModel.Opiniao;
                _context.Update(avaliacaoExistente);
            }
            else
            {
                var avaliacao = new Avaliacao
                {
                    AvaliacaoEstrelas = avaliacaoViewModel.AvaliacaoEstrelas,
                    Opiniao = avaliacaoViewModel.Opiniao,
                    AlunoId = alunoId,
                    MotoristaId = avaliacaoViewModel.MotoristaId
                };

                _context.Add(avaliacao);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(InfosMotorista), new { id = avaliacaoViewModel.MotoristaId });
        }

        public async Task<IActionResult> InfosMotorista(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var motorista = await _context.Users
                .Include(m => m.AvaliacoesRecebidas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorista == null)
            {
                return NotFound();
            }

            var alunoId = _userManager.GetUserId(User);
            var avaliacao = await _context.Avaliacoes
                .Where(a => a.MotoristaId == id && a.AlunoId == alunoId)
                .FirstOrDefaultAsync();

            AvaliacaoViewModel avaliacaoViewModel = null;

            if (avaliacao != null)
            {
                avaliacaoViewModel = new AvaliacaoViewModel
                {
                    AvaliacaoEstrelas = avaliacao.AvaliacaoEstrelas,
                    Opiniao = avaliacao.Opiniao
                };
            }

            double mediaAvaliacoes = 0;
            if (motorista.AvaliacoesRecebidas.Any())
            {
                mediaAvaliacoes = motorista.AvaliacoesRecebidas.Average(a => a.AvaliacaoEstrelas);
            }

            var model = new InfosMotoristaViewModel
            {
                Motorista = motorista,
                Avaliacao = avaliacaoViewModel ?? new AvaliacaoViewModel(),
                MediaAvaliacoes = mediaAvaliacoes
            };

            return View(model);
        }



    }
    public class AlunoIndexViewModel
    {
        public Usuario Aluno { get; set; }
        public Usuario Motorista { get; set; }
        public List<Aviso> Avisos { get; set; }
        public Presenca PresencaHoje { get; set; }
        public Viagem ViagemAtiva { get; set; }
    }

    public class EditarEnderecoViewModel
    {
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
    }

    public class InfosMotoristaViewModel
    {
        public Usuario Motorista { get; set; }
        public AvaliacaoViewModel Avaliacao { get; set; }
        public double MediaAvaliacoes { get; set; }
    }

    public class AvaliacaoViewModel
    {
        public int AvaliacaoEstrelas { get; set; }
        public string Opiniao { get; set; }
        public string MotoristaId { get; set; }
    }
}
