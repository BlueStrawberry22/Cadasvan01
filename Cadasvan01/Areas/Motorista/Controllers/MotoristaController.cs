using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cadasvan01.Models;
using Cadasvan01.Services;
using Cadasvan01.Data;
using Microsoft.EntityFrameworkCore;
using Cadasvan01.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Cadasvan01.Areas.Motorista.Controllers
{
    [Area("Motorista")]
   
    [Authorize(Roles = "Motorista")]
    [Authorize(Roles = "Admin")]
    public class MotoristaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
