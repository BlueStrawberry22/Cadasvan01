using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Cadasvan01.Services;
using Cadasvan01.Data;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Aluno")]
    [Authorize(Roles = "Aluno")]
    public class AlunoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
