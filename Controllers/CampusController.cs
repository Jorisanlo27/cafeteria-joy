using cafeteria_joy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cafeteria_joy.Controllers
{
    public class CampusController : Controller
    {
        private readonly JoyContext _context;

        public CampusController(JoyContext context)
        {
            _context = context;
        }

        // GET: Campus
        public async Task<IActionResult> Index()
        {
              return _context.Campus != null ? 
                          View(await _context.Campus.ToListAsync()) :
                          Problem("Entity set 'JoyContext.Campus'  is null.");
        }

        // GET: Campus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Campus == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus
                .FirstOrDefaultAsync(m => m.CampusId == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // GET: Campus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CampusId,Descripcion,Estado")] Campus campus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campus);
        }

        // GET: Campus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Campus == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }
            return View(campus);
        }

        // POST: Campus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CampusId,Descripcion,Estado")] Campus campus)
        {
            if (id != campus.CampusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampusExists(campus.CampusId))
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
            return View(campus);
        }

        // GET: Campus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Campus == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus
                .FirstOrDefaultAsync(m => m.CampusId == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // POST: Campus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Campus == null)
            {
                return Problem("Entity set 'JoyContext.Campus'  is null.");
            }
            var campus = await _context.Campus.FindAsync(id);
            if (campus != null)
            {
                _context.Campus.Remove(campus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampusExists(int id)
        {
          return (_context.Campus?.Any(e => e.CampusId == id)).GetValueOrDefault();
        }
    }
}
