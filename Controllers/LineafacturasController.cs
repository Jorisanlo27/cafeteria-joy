using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cafeteria_joy.Models;

namespace cafeteria_joy.Controllers
{
    public class LineafacturasController : Controller
    {
        private readonly JoyContext _context;

        public LineafacturasController(JoyContext context)
        {
            _context = context;
        }

        // GET: Lineafacturas
        public async Task<IActionResult> Index()
        {
            var joyContext = _context.Lineafacturas.Include(l => l.Articulo).Include(l => l.FacturacionArticulos);
            return View(await joyContext.ToListAsync());
        }

        // GET: Lineafacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lineafacturas == null)
            {
                return NotFound();
            }

            var lineafactura = await _context.Lineafacturas
                .Include(l => l.Articulo)
                .Include(l => l.FacturacionArticulos)
                .FirstOrDefaultAsync(m => m.LineaFacturaId == id);
            if (lineafactura == null)
            {
                return NotFound();
            }

            return View(lineafactura);
        }

        // GET: Lineafacturas/Create
        public IActionResult Create(int? id)
        {
            ViewData["ArticuloId"] = new SelectList(_context.Articulos.Where(a => a.Estado == true), "ArticuloId", "Descripcion");
            ViewData["FacturacionArticulosId"] = id;
            return View();
        }

        // POST: Lineafacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineaFacturaId,FacturacionArticulosId,ArticuloId,UnidadesVendidas,Total")] Lineafactura lineafactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lineafactura);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { controller = "facturacionarticulos", action = "Details", id = lineafactura.FacturacionArticulosId });
                //return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "ArticuloId", "Descripcion", lineafactura.ArticuloId);
            ViewData["FacturacionArticulosId"] = new SelectList(_context.Facturacionarticulos, "FacturacionArticulosId", "NoFactura", lineafactura.FacturacionArticulosId);
            return View(lineafactura);
        }

        // GET: Lineafacturas/Edit/5
        public async Task<IActionResult> Edit(int? id, int? idFactura)
        {
            if (id == null || _context.Lineafacturas == null)
            {
                return NotFound();
            }

            var lineafactura = await _context.Lineafacturas.FindAsync(id);
            if (lineafactura == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos.Where(a => a.Estado == true), "ArticuloId", "Descripcion", lineafactura.ArticuloId);
            ViewData["FacturacionArticulosId"] = idFactura;
            return View(lineafactura);
        }

        // POST: Lineafacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LineaFacturaId,FacturacionArticulosId,ArticuloId,UnidadesVendidas,Total")] Lineafactura lineafactura)
        {
            if (id != lineafactura.LineaFacturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineafactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineafacturaExists(lineafactura.LineaFacturaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToRoute(new { controller = "facturacionarticulos", action = "Details", id = lineafactura.FacturacionArticulosId });
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos.Where(a => a.Estado == true), "ArticuloId", "Descripcion", lineafactura.ArticuloId);
            ViewData["FacturacionArticulosId"] = lineafactura.FacturacionArticulosId;
            return View(lineafactura);
        }

        // GET: Lineafacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lineafacturas == null)
            {
                return NotFound();
            }

            var lineafactura = await _context.Lineafacturas
                .Include(l => l.Articulo)
                .Include(l => l.FacturacionArticulos)
                .FirstOrDefaultAsync(m => m.LineaFacturaId == id);
            if (lineafactura == null)
            {
                return NotFound();
            }

            return View(lineafactura);
        }

        // POST: Lineafacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lineafacturas == null)
            {
                return Problem("Entity set 'JoyContext.Lineafacturas'  is null.");
            }
            var lineafactura = await _context.Lineafacturas.FindAsync(id);
            if (lineafactura != null)
            {
                _context.Lineafacturas.Remove(lineafactura);
            }

            await _context.SaveChangesAsync();
            return RedirectToRoute(new { controller = "facturacionarticulos", action = "Details", id = lineafactura.FacturacionArticulosId });
        }

        private bool LineafacturaExists(int id)
        {
            return (_context.Lineafacturas?.Any(e => e.LineaFacturaId == id)).GetValueOrDefault();
        }
    }
}
