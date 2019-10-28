/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;

namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
    public class VartotojasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public VartotojasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: Vartotojas
        public async Task<IActionResult> Index()
        {
            var alaus_daryklos_gamybos_valdymas_DBContext = _context.Vartotojas;
            return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
        }

        // GET: Vartotojas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vartotojas = await _context.Vartotojas    
                .SingleOrDefaultAsync(m => m.VartotojoId == id);
            if (vartotojas == null)
            {
                return NotFound();
            }

            return View(vartotojas);
        }

        // GET: Vartotojas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vartotojas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VartotojoId,Vardas,Pavarde,PrisijungimoVardas")] Vartotojas vartotojas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vartotojas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vartotojas);
        }

        // GET: Vartotojas/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vartotojas = await _context.Vartotojas.SingleOrDefaultAsync(m => m.VartotojoId == id);
            if (vartotojas == null)
            {
                return NotFound();
            }
            return View(vartotojas);
        }

        // POST: Vartotojas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("VartotojoId,Vardas,Pavarde,PrisijungimoVardas")] Vartotojas vartotojas)
        {
            if (id != vartotojas.VartotojoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vartotojas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VartotojasExists(vartotojas.VartotojoId))
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
            return View(vartotojas);
        }

        // GET: Vartotojas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vartotojas = await _context.Vartotojas
                .SingleOrDefaultAsync(m => m.VartotojoId == id);
            if (vartotojas == null)
            {
                return NotFound();
            }

            return View(vartotojas);
        }

        // POST: Vartotojas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vartotojas = await _context.Vartotojas.SingleOrDefaultAsync(m => m.VartotojoId == id);
            _context.Vartotojas.Remove(vartotojas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VartotojasExists(decimal id)
        {
            return _context.Vartotojas.Any(e => e.VartotojoId == id);
        }
    }
}
*/