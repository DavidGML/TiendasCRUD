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
    public class ClienteArticulosController : Controller
    {
        private readonly TiendasContext _context;

        public ClienteArticulosController(TiendasContext context)
        {
            _context = context;
        }

        // GET: ClienteArticulos
        public async Task<IActionResult> Index()
        {
            var tiendasContext = _context.ClienteArticulos.Include(c => c.CodigoArticuloCaNavigation).Include(c => c.IdClienteCaNavigation);
            return View(await tiendasContext.ToListAsync());
        }

        // GET: ClienteArticulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteArticulos == null)
            {
                return NotFound();
            }

            var clienteArticulo = await _context.ClienteArticulos
                .Include(c => c.CodigoArticuloCaNavigation)
                .Include(c => c.IdClienteCaNavigation)
                .FirstOrDefaultAsync(m => m.IdClienteCa == id);
            if (clienteArticulo == null)
            {
                return NotFound();
            }

            return View(clienteArticulo);
        }

        // GET: ClienteArticulos/Create
        public IActionResult Create()
        {
            ViewData["CodigoArticuloCa"] = new SelectList(_context.Articulos, "CodigoArticulo", "CodigoArticulo");
            ViewData["IdClienteCa"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            return View();
        }

        // POST: ClienteArticulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClienteCa,CodigoArticuloCa,FechaAc")] ClienteArticulo clienteArticulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteArticulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoArticuloCa"] = new SelectList(_context.Articulos, "CodigoArticulo", "CodigoArticulo", clienteArticulo.CodigoArticuloCa);
            ViewData["IdClienteCa"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteArticulo.IdClienteCa);
            return View(clienteArticulo);
        }

        // GET: ClienteArticulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteArticulos == null)
            {
                return NotFound();
            }

            var clienteArticulo = await _context.ClienteArticulos.FindAsync(id);
            if (clienteArticulo == null)
            {
                return NotFound();
            }
            ViewData["CodigoArticuloCa"] = new SelectList(_context.Articulos, "CodigoArticulo", "CodigoArticulo", clienteArticulo.CodigoArticuloCa);
            ViewData["IdClienteCa"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteArticulo.IdClienteCa);
            return View(clienteArticulo);
        }

        // POST: ClienteArticulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClienteCa,CodigoArticuloCa,FechaAc")] ClienteArticulo clienteArticulo)
        {
            if (id != clienteArticulo.IdClienteCa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteArticulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteArticuloExists(clienteArticulo.IdClienteCa))
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
            ViewData["CodigoArticuloCa"] = new SelectList(_context.Articulos, "CodigoArticulo", "CodigoArticulo", clienteArticulo.CodigoArticuloCa);
            ViewData["IdClienteCa"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteArticulo.IdClienteCa);
            return View(clienteArticulo);
        }

        // GET: ClienteArticulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteArticulos == null)
            {
                return NotFound();
            }

            var clienteArticulo = await _context.ClienteArticulos
                .Include(c => c.CodigoArticuloCaNavigation)
                .Include(c => c.IdClienteCaNavigation)
                .FirstOrDefaultAsync(m => m.IdClienteCa == id);
            if (clienteArticulo == null)
            {
                return NotFound();
            }

            return View(clienteArticulo);
        }

        // POST: ClienteArticulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteArticulos == null)
            {
                return Problem("Entity set 'TiendasContext.ClienteArticulos'  is null.");
            }
            var clienteArticulo = await _context.ClienteArticulos.FindAsync(id);
            if (clienteArticulo != null)
            {
                _context.ClienteArticulos.Remove(clienteArticulo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteArticuloExists(int id)
        {
          return (_context.ClienteArticulos?.Any(e => e.IdClienteCa == id)).GetValueOrDefault();
        }
    }
}
