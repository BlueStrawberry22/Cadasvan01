using Cadasvan01.Data;
using Cadasvan01.Enums;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Authorize]
    public class MotoristaListaAlunosController : Controller
    {
        public readonly UserManager<Usuario> _userManager;
        public readonly ApplicationDbContext _context;
        public MotoristaListaAlunosController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListaAlunos()
        {
            var alunos = _context.Usuarios
                .Where(u => u.Tipo == UsuarioEnum.Aluno)
                .Include(u => u.Cidade)
                .ToList();

            return View(alunos);
        }

    }

}


