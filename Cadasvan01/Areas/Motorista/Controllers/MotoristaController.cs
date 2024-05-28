using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Cadasvan01.Data;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Identity;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
    [Authorize(Roles = "Motorista")]
    public class MotoristaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotoristaController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles ="Aluno")]

        [HttpGet]
        public async Task<IActionResult> InfosMotorista(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var motorista = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (motorista == null)
            {
                return NotFound();
            }

            return View(motorista);
        }
    }
}
