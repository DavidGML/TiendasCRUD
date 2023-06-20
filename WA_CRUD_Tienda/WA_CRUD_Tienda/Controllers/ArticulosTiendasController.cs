using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WA_CRUD_Tienda.Models;

namespace WA_CRUD_Tienda.Controllers
{
    public class ArticulosTiendasController : Controller
    {
        private readonly TiendasContext _context;

        public ArticulosTiendasController(TiendasContext context)
        {
            _context = context;
        }

        // GET: ArticulosTiendas
        public async Task<IActionResult> Index()
        {
            var tiendasContext = _context.ArticuloTienda.Include(a => a.CodigoArticuloAtNavigation).Include(a => a.CodigoSucursalAtNavigation);
            return View(await tiendasContext.ToListAsync());
        }

        // GET: ArticulosTiendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ArticuloTienda == null)
            {
                return NotFound();
            }

            var articuloTiendum = await _context.ArticuloTienda
                .Include(a => a.CodigoArticuloAtNavigation)
                .Include(a => a.CodigoSucursalAtNavigation)
                .FirstOrDefaultAsync(m => m.CodigoArticuloAt == id);
            if (articuloTiendum == null)
            {
                return NotFound();
            }

            return View(articuloTiendum);
        }

        // GET: ArticulosTiendas/Create
        public IActionResult Create()
        {
            ViewData["CodigoArticuloAt"] = new SelectList(_context.Articulos, "CodigoArticulo", "CodigoArticulo");
            ViewData["CodigoSucursalAt"] = new SelectList(_context.Sucursales, "CodigoSucursal", "CodigoSucursal");
            return View();
        }

        // POST: ArticulosTiendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoArticuloAt,CodigoSucursalAt,FechaAs")] ArticuloTiendum articuloTiendum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articuloTiendum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoArticuloAt"] = new SelectList(_context.Articulos, "CodigoArticulo", "CodigoArticulo", articuloTiendum.CodigoArticuloAt);
            ViewData["CodigoSucursalAt"] = new SelectList(_context.Sucursales, "CodigoSucursal", "CodigoSucursal", articuloTiendum.CodigoSucursalAt);
            return View(articuloTiendum);
        }

        // GET: ArticulosTiendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ArticuloTienda == null)
            {
                return NotFound();
            }

            var articuloTiendum = await _context.ArticuloTienda.FindAsync(id);
            if (articuloTiendum == null)
            {
                return NotFound();
            }
            ViewData["CodigoArticuloAt"] = new SelectList(_context.Articulos, "CodigoArticulo", "CodigoArticulo", articuloTiendum.CodigoArticuloAt);
            ViewData["CodigoSucursalAt"] = new SelectList(_context.Sucursales, "CodigoSucursal", "CodigoSucursal", articuloTiendum.CodigoSucursalAt);
            return View(articuloTiendum);
        }

        // POST: ArticulosTiendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoArticuloAt,CodigoSucursalAt,FechaAs")] ArticuloTiendum articuloTiendum)
        {
            if (id != articuloTiendum.CodigoArticuloAt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articuloTiendum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloTiendumExists(articuloTiendum.CodigoArticuloAt))
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
            ViewData["CodigoArticuloAt"] = new SelectList(_context.Articulos, "CodigoArticulo", "CodigoArticulo", articuloTiendum.CodigoArticuloAt);
            ViewData["CodigoSucursalAt"] = new SelectList(_context.Sucursales, "CodigoSucursal", "CodigoSucursal", articuloTiendum.CodigoSucursalAt);
            return View(articuloTiendum);
        }

        // GET: ArticulosTiendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ArticuloTienda == null)
            {
                return NotFound();
            }

            var articuloTiendum = await _context.ArticuloTienda
                .Include(a => a.CodigoArticuloAtNavigation)
                .Include(a => a.CodigoSucursalAtNavigation)
                .FirstOrDefaultAsync(m => m.CodigoArticuloAt == id);
            if (articuloTiendum == null)
            {
                return NotFound();
            }

            return View(articuloTiendum);
        }

        // POST: ArticulosTiendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ArticuloTienda == null)
            {
                return Problem("Entity set 'TiendasContext.ArticuloTienda'  is null.");
            }
            var articuloTiendum = await _context.ArticuloTienda.FindAsync(id);
            if (articuloTiendum != null)
            {
                _context.ArticuloTienda.Remove(articuloTiendum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloTiendumExists(int id)
        {
          return (_context.ArticuloTienda?.Any(e => e.CodigoArticuloAt == id)).GetValueOrDefault();
        }
    }
}
