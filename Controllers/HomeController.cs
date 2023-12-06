using cafeteria_joy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cafeteria_joy.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly JoyContext _context;

        //private readonly ILogger<HomeController> _logger;

        public HomeController(JoyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Marcas = _context.Marcas.Where(m => m.Estado == true).ToList().Count();
            ViewBag.Articulos = _context.Articulos.Where(m => m.Estado == true).ToList().Count();
            ViewBag.Empleados = _context.Empleados.Where(m => m.Estado == true).ToList().Count();
            ViewBag.Ordenes = _context.Facturacionarticulos.Where(m => m.Estado == true).ToList().Count();
            return View(await _context.Facturacionarticulos.Include(f => f.Lineafacturas).ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}