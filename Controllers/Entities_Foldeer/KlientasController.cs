using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;

namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
    public class KlientasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public KlientasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: Klientas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klientas.ToListAsync());
        }

        // GET: Klientas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klientas = await _context.Klientas
                .SingleOrDefaultAsync(m => m.KlientoId == id);
            if (klientas == null)
            {
                return NotFound();
            }

            return View(klientas);
        }



        // POST: Klientas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlientoId,KlientoPavadinimas")] Klientas klientas, string kipav, string kikod, string kinr, string kiadr)
        {
      decimal kiaddalert = 0;
      klientas.KlientoPavadinimas = kipav;
      klientas.ImonesKodas = kikod;
      klientas.KontaktinisNR = kinr;
      klientas.Adresas = kiadr;
      if (ModelState.IsValid)
            {
                _context.Add(klientas);
                await _context.SaveChangesAsync();
        kiaddalert++;
                return RedirectToAction(nameof(Redirect), new { kiaddalert = kiaddalert });
            }
      return RedirectToAction(nameof(Redirect), new { kiaddalert = kiaddalert });
    }
        public IActionResult Redirect(decimal? kiaddalert)
        {
            var klientas = _context.Klientas;
            string url = @Url.Action("Create", "KlientoUzsakymoAplankas", new { kiaddalert = kiaddalert });
            if (klientas != null)
            return Redirect(url);
            else return NotFound();
        }
        // GET: Klientas/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klientas = await _context.Klientas.SingleOrDefaultAsync(m => m.KlientoId == id);
            if (klientas == null)
            {
                return NotFound();
            }
            return View(klientas);
        }

        // POST: Klientas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("KlientoId,KlientoPavadinimas")] Klientas klientas)
        {
            if (id != klientas.KlientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klientas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlientasExists(klientas.KlientoId))
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
            return View(klientas);
        }

        // GET: Klientas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klientas = await _context.Klientas
                .SingleOrDefaultAsync(m => m.KlientoId == id);
            if (klientas == null)
            {
                return NotFound();
            }

            return View(klientas);
        }

        // POST: Klientas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var klientas = await _context.Klientas.SingleOrDefaultAsync(m => m.KlientoId == id);
            _context.Klientas.Remove(klientas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlientasExists(decimal id)
        {
            return _context.Klientas.Any(e => e.KlientoId == id);
        }
    }
}
