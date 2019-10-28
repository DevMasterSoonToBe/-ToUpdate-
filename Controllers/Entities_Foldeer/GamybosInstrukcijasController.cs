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
    public class GamybosInstrukcijasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public GamybosInstrukcijasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: GamybosInstrukcijas
        public async Task<IActionResult> Index()
        {
            var alaus_daryklos_gamybos_valdymas_DBContext = _context.GamybosInstrukcija.Include(g => g.KlientoUzsakymo);
            return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
        }

        // GET: GamybosInstrukcijas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamybosInstrukcija = await _context.GamybosInstrukcija
                .Include(g => g.KlientoUzsakymo)
           
                .SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (gamybosInstrukcija == null)
            {
                return NotFound();
            }

            return View(gamybosInstrukcija);
        }

    public IActionResult Redirect(decimal? id, decimal? brandlaik, decimal? ChangeStateGIalert, decimal? GIalert)
    {
      string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new { id = id, brandlaik = brandlaik, ChangeStateGIalert = ChangeStateGIalert, GIalert = GIalert });
      return Redirect(url);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(decimal kuiGI,[Bind("KlientoUzsakymoId,InstrukcijosId,Busena,RecepturaParuosta,TechnikosPatarimaiParuosti,GamybosTerminas,BrandinimoLaikas")]
    GamybosInstrukcija gamybosinstrukcija)
    {

      decimal GIalert = 0;
      if (ModelState.IsValid)
      {
        _context.Add(gamybosinstrukcija);
        await _context.SaveChangesAsync();
        GIalert++;
        return RedirectToAction(nameof(Redirect), new { id = kuiGI, GIalert = GIalert });
      }
      ViewData["KlientoUzsakymoId"] = kuiGI;
      return RedirectToAction(nameof(Redirect), new { id = kuiGI });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBrandinimoLaikas(decimal kui3, decimal Ini3, string IB, string RP, string TP, DateTime GT, string BL)
    {

      decimal brandlaik = 0;
      var gamybosinstrukcijaaddBrandinimoLaikas = await _context.GamybosInstrukcija.SingleOrDefaultAsync(m => m.InstrukcijosId == Ini3 && m.KlientoUzsakymoId == kui3 && m.Busena == IB
     && m.RecepturaParuosta == RP && m.TechnikosPatarimaiParuosti == TP && m.GamybosTerminas == GT);
      gamybosinstrukcijaaddBrandinimoLaikas.BrandinimoLaikas = BL;
      if (ModelState.IsValid)
      {
        _context.Update(gamybosinstrukcijaaddBrandinimoLaikas);
        await _context.SaveChangesAsync();
        brandlaik++;
        return RedirectToAction(nameof(Redirect), new { id = kui3, brandlaik = brandlaik });
      }
      ViewData["KlientoUzsakymoId"] = kui3;
      return RedirectToAction(nameof(Redirect), new { id = kui3, brandlaik = brandlaik });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditBrandinimoLaikas(decimal kui4, decimal Ini4, string IB2, string RP2, string TP2, DateTime GT2, string BL2)
    {
      decimal brandlaik = 0;
      var gamybosinstrukcijaaddBrandinimoLaikas = await _context.GamybosInstrukcija.SingleOrDefaultAsync(m => m.InstrukcijosId == Ini4 && m.KlientoUzsakymoId == kui4 && m.Busena == IB2
     && m.RecepturaParuosta == RP2 && m.TechnikosPatarimaiParuosti == TP2 && m.GamybosTerminas == GT2);
      gamybosinstrukcijaaddBrandinimoLaikas.BrandinimoLaikas = BL2;
      if (ModelState.IsValid)
      {
        _context.Update(gamybosinstrukcijaaddBrandinimoLaikas);
        await _context.SaveChangesAsync();
        brandlaik = 2;
        return RedirectToAction(nameof(Redirect), new { id = kui4, brandlaik = brandlaik });
      }
      ViewData["KlientoUzsakymoId"] = kui4;
      return RedirectToAction(nameof(Redirect), new { id = kui4, brandlaik = brandlaik });
    }
    
    // POST: GamybosInstrukcijas/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGamybosTerminas(decimal kuiGT, decimal INI, string IB2, string RP2, string TP2, DateTime GT)
        {

      decimal GIalert = 0;
      var gamybosinstrukcijaEditGT = _context.GamybosInstrukcija.SingleOrDefault(m => m.KlientoUzsakymoId == kuiGT && m.InstrukcijosId == INI && m.Busena == IB2
     && m.RecepturaParuosta == RP2 && m.TechnikosPatarimaiParuosti == TP2);
      gamybosinstrukcijaEditGT.GamybosTerminas = GT;
      if (ModelState.IsValid)
      {
        _context.Update(gamybosinstrukcijaEditGT);
        await _context.SaveChangesAsync();
        GIalert = 2;
        return RedirectToAction(nameof(Redirect), new { id = kuiGT, GIalert = GIalert });
      }

      ViewData["KlientoUzsakymoId"] = new SelectList(_context.KlientoUzsakymoAplankas, "KlientoUzsakymoId", "Busena", gamybosinstrukcijaEditGT.KlientoUzsakymoId);
      return RedirectToAction(nameof(Redirect), new { id = kuiGT });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeState(decimal kuiGT2, decimal INI2, string IB3, string RP3, string TP3, DateTime GT2)
    {

      decimal ChangeStateGIalert = 0;
      var gamybosinstrukcijaEditGT = _context.GamybosInstrukcija.SingleOrDefault(m => m.KlientoUzsakymoId == kuiGT2 && m.InstrukcijosId == INI2 && m.GamybosTerminas == GT2
     && m.RecepturaParuosta == RP3 && m.TechnikosPatarimaiParuosti == TP3);
      gamybosinstrukcijaEditGT.Busena = IB3;
      if (ModelState.IsValid)
      {
        _context.Update(gamybosinstrukcijaEditGT);
        await _context.SaveChangesAsync();
        ChangeStateGIalert++;
        return RedirectToAction(nameof(Redirect), new { id = kuiGT2, ChangeStateGIalert = ChangeStateGIalert });
      }

      ViewData["KlientoUzsakymoId"] = new SelectList(_context.KlientoUzsakymoAplankas, "KlientoUzsakymoId", "Busena", gamybosinstrukcijaEditGT.KlientoUzsakymoId);
      return RedirectToAction(nameof(Redirect), new { id = kuiGT2 });
    }


    // GET: GamybosInstrukcijas/Delete/5
    public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamybosInstrukcija = await _context.GamybosInstrukcija
                .Include(g => g.KlientoUzsakymo)
                .SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (gamybosInstrukcija == null)
            {
                return NotFound();
            }

            return View(gamybosInstrukcija);
        }

        // POST: GamybosInstrukcijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var gamybosInstrukcija = await _context.GamybosInstrukcija.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            _context.GamybosInstrukcija.Remove(gamybosInstrukcija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamybosInstrukcijaExists(decimal id)
        {
            return _context.GamybosInstrukcija.Any(e => e.KlientoUzsakymoId == id);
        }
    }
}
