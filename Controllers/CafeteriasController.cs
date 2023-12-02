using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cafeteria_joy.Models;
using Microsoft.AspNetCore.Authorization;

namespace cafeteria_joy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CafeteriasController : Controller
    {
        private readonly JoyContext _context;

        public CafeteriasController(JoyContext context)
        {
            _context = context;
        }

        // GET: Cafeterias
        public async Task<IActionResult> Index()
        {
            var joyContext = _context.Cafeteria.Include(c => c.CampusNavigation).Include(c => c.EncargadoNavigation);
            return View(await joyContext.ToListAsync());
        }

        // GET: Cafeterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cafeteria == null)
            {
                return NotFound();
            }

            var cafeteria = await _context.Cafeteria
                .Include(c => c.CampusNavigation)
                .Include(c => c.EncargadoNavigation)
                .FirstOrDefaultAsync(m => m.CafeteriaId == id);
            if (cafeteria == null)
            {
                return NotFound();
            }

            return View(cafeteria);
        }

        // GET: Cafeterias/Create
        public IActionResult Create()
        {
            ViewData["Campus"] = new SelectList(_context.Campus, "CampusId", "Descripcion");
            ViewData["Encargado"] = new SelectList(_context.Empleados, "EmpleadosId", "Nombre");
            return View();
        }

        // POST: Cafeterias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CafeteriaId,Descripcion,Campus,Encargado,Estado")] Cafeteria cafeteria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cafeteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Campus"] = new SelectList(_context.Campus, "CampusId", "Descripcion", cafeteria.Campus);
            ViewData["Encargado"] = new SelectList(_context.Empleados, "EmpleadosId", "Nombre", cafeteria.Encargado);
            return View(cafeteria);
        }

        // GET: Cafeterias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cafeteria == null)
            {
                return NotFound();
            }

            var cafeteria = await _context.Cafeteria.FindAsync(id);
            if (cafeteria == null)
            {
                return NotFound();
            }
            ViewData["Campus"] = new SelectList(_context.Campus, "CampusId", "Descripcion", cafeteria.Campus);
            ViewData["Encargado"] = new SelectList(_context.Empleados, "EmpleadosId", "Nombre", cafeteria.Encargado);
            return View(cafeteria);
        }

        // POST: Cafeterias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CafeteriaId,Descripcion,Campus,Encargado,Estado")] Cafeteria cafeteria)
        {
            if (id != cafeteria.CafeteriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cafeteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CafeteriaExists(cafeteria.CafeteriaId))
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
            ViewData["Campus"] = new SelectList(_context.Campus, "CampusId", "Descripcion", cafeteria.Campus);
            ViewData["Encargado"] = new SelectList(_context.Empleados, "EmpleadosId", "Nombre", cafeteria.Encargado);
            return View(cafeteria);
        }

        // GET: Cafeterias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cafeteria == null)
            {
                return NotFound();
            }

            var cafeteria = await _context.Cafeteria
                .Include(c => c.CampusNavigation)
                .Include(c => c.EncargadoNavigation)
                .FirstOrDefaultAsync(m => m.CafeteriaId == id);
            if (cafeteria == null)
            {
                return NotFound();
            }

            return View(cafeteria);
        }

        // POST: Cafeterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cafeteria == null)
            {
                return Problem("Entity set 'JoyContext.Cafeteria'  is null.");
            }
            var cafeteria = await _context.Cafeteria.FindAsync(id);
            if (cafeteria != null)
            {
                _context.Cafeteria.Remove(cafeteria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CafeteriaExists(int id)
        {
          return (_context.Cafeteria?.Any(e => e.CafeteriaId == id)).GetValueOrDefault();
        }
    }
}
