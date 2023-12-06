using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cafeteria_joy.Models;
using Rotativa.AspNetCore;

namespace cafeteria_joy.Controllers
{
    public class FacturacionarticulosController : Controller
    {
        private readonly JoyContext _context;

        public FacturacionarticulosController(JoyContext context)
        {
            _context = context;
        }
        public IActionResult ImprimirVenta()
        {
            var modelo = _context.Facturacionarticulos
               .Include(f => f.EmpleadoNavigation)
               .Where(f => f.Estado == true)
               .ToList();

            return new ViewAsPdf("ImprimirVenta", modelo)
            {
                FileName = $"Venta.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
        // GET: Facturacionarticulos
        public async Task<IActionResult> Index()
        {
            var joyContext = _context.Facturacionarticulos.Include(f => f.EmpleadoNavigation);
            return View(await joyContext.ToListAsync());
        }

        // GET: Facturacionarticulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facturacionarticulos == null)
            {
                return NotFound();
            }

            var facturacionarticulo = await _context.Facturacionarticulos
                .Include(f => f.EmpleadoNavigation)
                .Include(f => f.Lineafacturas)
                .ThenInclude(f => f.Articulo)
                .FirstOrDefaultAsync(m => m.FacturacionArticulosId == id);
            if (facturacionarticulo == null)
            {
                return NotFound();
            }

            return View(facturacionarticulo);
        }

        // GET: Facturacionarticulos/Create
        public IActionResult Create()
        {
            ViewData["Empleado"] = new SelectList(_context.Empleados.Where(e => e.Estado == true), "EmpleadosId", "Nombre");
            return View();
        }

        // POST: Facturacionarticulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturacionArticulosId,NoFactura,Empleado,Cliente,FechaVenta,Total,Comentario,Estado")] Facturacionarticulo facturacionarticulo)
        {
            var date = DateTime.Now;

            facturacionarticulo.NoFactura = date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + new Random().Next(100, 999).ToString();

            if (ModelState.IsValid)
            {
                _context.Add(facturacionarticulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Empleado"] = new SelectList(_context.Empleados, "EmpleadosId", "Nombre", facturacionarticulo.Empleado);
            return View(facturacionarticulo);
        }

        // GET: Facturacionarticulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facturacionarticulos == null)
            {
                return NotFound();
            }

            var facturacionarticulo = await _context.Facturacionarticulos.FindAsync(id);
            if (facturacionarticulo == null)
            {
                return NotFound();
            }
            ViewData["Empleado"] = new SelectList(_context.Empleados.Where(e => e.Estado == true), "EmpleadosId", "Nombre", facturacionarticulo.Empleado);
            return View(facturacionarticulo);
        }

        // POST: Facturacionarticulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturacionArticulosId,NoFactura,Empleado,Cliente,FechaVenta,Total,Comentario,Estado")] Facturacionarticulo facturacionarticulo)
        {
            if (id != facturacionarticulo.FacturacionArticulosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturacionarticulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturacionarticuloExists(facturacionarticulo.FacturacionArticulosId))
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
            ViewData["Empleado"] = new SelectList(_context.Empleados.Where(e => e.Estado == true), "EmpleadosId", "Nombre", facturacionarticulo.Empleado);
            return View(facturacionarticulo);
        }

        // GET: Facturacionarticulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facturacionarticulos == null)
            {
                return NotFound();
            }

            var facturacionarticulo = await _context.Facturacionarticulos
                .Include(f => f.EmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.FacturacionArticulosId == id);
            if (facturacionarticulo == null)
            {
                return NotFound();
            }

            return View(facturacionarticulo);
        }

        // POST: Facturacionarticulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facturacionarticulos == null)
            {
                return Problem("Entity set 'JoyContext.Facturacionarticulos'  is null.");
            }
            var facturacionarticulo = await _context.Facturacionarticulos.FindAsync(id);
            if (facturacionarticulo != null)
            {
                _context.Facturacionarticulos.Remove(facturacionarticulo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturacionarticuloExists(int id)
        {
            return (_context.Facturacionarticulos?.Any(e => e.FacturacionArticulosId == id)).GetValueOrDefault();
        }
    }
}
