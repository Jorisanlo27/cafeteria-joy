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
    public class TiposusuariosController : Controller
    {
        private readonly JoyContext _context;

        public TiposusuariosController(JoyContext context)
        {
            _context = context;
        }

        // GET: Tiposusuarios
        public async Task<IActionResult> Index()
        {
              return _context.Tiposusuarios != null ? 
                          View(await _context.Tiposusuarios.ToListAsync()) :
                          Problem("Entity set 'JoyContext.Tiposusuarios'  is null.");
        }

        // GET: Tiposusuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tiposusuarios == null)
            {
                return NotFound();
            }

            var tiposusuario = await _context.Tiposusuarios
                .FirstOrDefaultAsync(m => m.TipoUsuarioId == id);
            if (tiposusuario == null)
            {
                return NotFound();
            }

            return View(tiposusuario);
        }

        // GET: Tiposusuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tiposusuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoUsuarioId,Descripcion,Estado")] Tiposusuario tiposusuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposusuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposusuario);
        }

        // GET: Tiposusuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tiposusuarios == null)
            {
                return NotFound();
            }

            var tiposusuario = await _context.Tiposusuarios.FindAsync(id);
            if (tiposusuario == null)
            {
                return NotFound();
            }
            return View(tiposusuario);
        }

        // POST: Tiposusuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoUsuarioId,Descripcion,Estado")] Tiposusuario tiposusuario)
        {
            if (id != tiposusuario.TipoUsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposusuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposusuarioExists(tiposusuario.TipoUsuarioId))
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
            return View(tiposusuario);
        }

        // GET: Tiposusuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tiposusuarios == null)
            {
                return NotFound();
            }

            var tiposusuario = await _context.Tiposusuarios
                .FirstOrDefaultAsync(m => m.TipoUsuarioId == id);
            if (tiposusuario == null)
            {
                return NotFound();
            }

            return View(tiposusuario);
        }

        // POST: Tiposusuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tiposusuarios == null)
            {
                return Problem("Entity set 'JoyContext.Tiposusuarios'  is null.");
            }
            var tiposusuario = await _context.Tiposusuarios.FindAsync(id);
            if (tiposusuario != null)
            {
                _context.Tiposusuarios.Remove(tiposusuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposusuarioExists(int id)
        {
          return (_context.Tiposusuarios?.Any(e => e.TipoUsuarioId == id)).GetValueOrDefault();
        }
    }
}
