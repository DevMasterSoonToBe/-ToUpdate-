using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;
using Microsoft.AspNetCore.Authorization;

namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
    public class TiekejasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public TiekejasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: Tiekejas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tiekejas.ToListAsync());
        }

        // GET: Tiekejas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiekejas = await _context.Tiekejas
                .SingleOrDefaultAsync(m => m.TiekejoId == id);
            if (tiekejas == null)
            {
                return NotFound();
            }

            return View(tiekejas);
        }

        // GET: Tiekejas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tiekejas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TiekejoId,TiekejoPavadinimas,TiekejoTtipas")] Tiekejas tiekejas, decimal kui, string tpav,
          string tikod, string tinr, string tiadr, string ttip)
        {
      decimal tiekejasdbalert = 0;
      tiekejas.TiekejoPavadinimas = tpav;
      tiekejas.ImonesKodas = tikod;
      tiekejas.KontaktinisNR = tinr;
      tiekejas.Adresas = tiadr;
      tiekejas.TiekejoTtipas = ttip;
      if (ModelState.IsValid)
            {
                _context.Add(tiekejas);
                await _context.SaveChangesAsync();
        tiekejasdbalert++;
                return RedirectToAction(nameof(Redirect), new { id = kui, tiekejasdbalert = tiekejasdbalert });
            }
            return View(tiekejas);
        }

    public IActionResult Redirect(decimal? id, decimal? tiekejasdbalert)
    {
      string url = @Url.Action("Create", "Sutartis", new { id = id, tiekejasdbalert = tiekejasdbalert });
      return Redirect(url);
    }

    // GET: Tiekejas/Edit/5
    public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiekejas = await _context.Tiekejas.SingleOrDefaultAsync(m => m.TiekejoId == id);
            if (tiekejas == null)
            {
                return NotFound();
            }
            return View(tiekejas);
        }

        // POST: Tiekejas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("TiekejoId,TiekejoPavadinimas,TiekejoTtipas")] Tiekejas tiekejas)
        {
            if (id != tiekejas.TiekejoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiekejas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiekejasExists(tiekejas.TiekejoId))
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
            return View(tiekejas);
        }

        // GET: Tiekejas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiekejas = await _context.Tiekejas
                .SingleOrDefaultAsync(m => m.TiekejoId == id);
            if (tiekejas == null)
            {
                return NotFound();
            }

            return View(tiekejas);
        }

        // POST: Tiekejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var tiekejas = await _context.Tiekejas.SingleOrDefaultAsync(m => m.TiekejoId == id);
            _context.Tiekejas.Remove(tiekejas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiekejasExists(decimal id)
        {
            return _context.Tiekejas.Any(e => e.TiekejoId == id);
        }
    }
}
