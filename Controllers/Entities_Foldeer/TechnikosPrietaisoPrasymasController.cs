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
    public class TechnikosPrietaisoPrasymasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public TechnikosPrietaisoPrasymasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }
    // GET: IngredientoPrasymas
    public async Task<IActionResult> Index(decimal? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      ViewData["TechnikaPrasymui"] = new SelectList(_context.TechnikosPrietaisas.Join(_context.TechnikosPrietaisoPatarimas.Where(x => x.KlientoUzsakymoId == id),
       k => k.TechnikosPrietaisoId,
       e => e.TechnikosPrietaisoId,
       (k, e) => k), "TechnikosPrietaisoId", "PrietaisoPavadinimas");
      var alaus_daryklos_gamybos_valdymas_DBContext = _context.TechnikosPrietaisoPrasymas.Include(i => i.TechnikosPrietaiso).Include(i => i.KlientoUzsakymo).
        Where(i => i.KlientoUzsakymoId == id);
      ViewData["KlientoUzsakymoId"] = id;
      return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
    }

    public async Task<IActionResult> ChangeState(decimal kui, string NB)
    {
      var alaus_daryklos_gamybos_valdymas_DBContext5 = _context.TechnikosPrietaisoPrasymas.Include(i => i.TechnikosPrietaiso).Include(i => i.KlientoUzsakymo).
         Where(i => i.KlientoUzsakymoId == kui).ToList();
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext5)
      {
        item.Busena = NB;
        item.KlientoUzsakymoId = kui;
        item.TechnikosPrietaisoId = item.TechnikosPrietaisoId;
        if (!ModelState.IsValid)
        {
          return RedirectToAction(nameof(Redirect), new { id = kui });

        }
        if (ModelState.IsValid)
        {
          try
          {
            _context.Update(item);
            await _context.SaveChangesAsync();
          }
          catch (DbUpdateConcurrencyException)
          {
            if (!TechnikosPrietaisoPrasymasExists(item.KlientoUzsakymoId))
            {
              return NotFound();
            }
            else if (!TechnikosPrietaisoPrasymasExists(item.TechnikosPrietaisoId))
            {
              return NotFound();
            }
            else
            {
              throw;
            }
          }
        }
      }
      return RedirectToAction(nameof(Redirect), new { id = kui });
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStateFORM(decimal kuiTECHPR, string NB)
    {
      decimal TechPrasAlert = 0;
      var alaus_daryklos_gamybos_valdymas_DBContext4 = _context.TechnikosPrietaisoPrasymas.Include(i => i.TechnikosPrietaiso).Include(i => i.KlientoUzsakymo).
         Where(i => i.KlientoUzsakymoId == kuiTECHPR).ToList();
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext4)
      {
        item.Busena = NB;
        item.KlientoUzsakymoId = kuiTECHPR;
        item.TechnikosPrietaisoId = item.TechnikosPrietaisoId;
        if (!ModelState.IsValid)
        {
          return RedirectToAction(nameof(Redirect), new { id = kuiTECHPR });

        }
        if (ModelState.IsValid)
        {
          try
          {
            _context.Update(item);
            await _context.SaveChangesAsync();
            TechPrasAlert = 1;
          }
          catch (DbUpdateConcurrencyException)
          {
            if (!TechnikosPrietaisoPrasymasExists(item.KlientoUzsakymoId))
            {
              return NotFound();
            }
            else if (!TechnikosPrietaisoPrasymasExists(item.TechnikosPrietaisoId))
            {
              return NotFound();
            }
            else
            {
              throw;
            }
          }
        }
      }
      return RedirectToAction(nameof(Redirect), new { id = kuiTECHPR, TechPrasAlert = TechPrasAlert });
    }
    public IActionResult Redirect(decimal? id, decimal? alertshowPrasymas2, decimal? TechPrasAlert)
    {
      string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new { id = id, alertshowPrasymas2 = alertshowPrasymas2, TechPrasAlert = TechPrasAlert });
      return Redirect(url);
    }
    // GET: TechnikosPrietaisoPrasymas/Details/5
    public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technikosPrietaisoPrasymas = await _context.TechnikosPrietaisoPrasymas
                .Include(t => t.KlientoUzsakymo)
                .Include(t => t.TechnikosPrietaiso)
                .SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (technikosPrietaisoPrasymas == null)
            {
                return NotFound();
            }

            return View(technikosPrietaisoPrasymas);
        }
    public IActionResult Create(decimal? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var TechnikosPrietaisoPrasymas = _context.TechnikosPrietaisoPrasymas.Include(m => m.KlientoUzsakymoId == id);


      int i;

      /*var Ingredientas = new Ingredientas { IngredientoId = 0, IngredientoPavadinimas = "Naujai įvedamas ingredientas" };
      _context.Ingredientas.Add(Ingredientas);
      _context.SaveChanges();*/
      ViewData["TechnikosPrietaisoId"] = new SelectList(_context.TechnikosPrietaisas, "TechnikosPrietaisoId", "PrietaisoPavadinimas");
      ViewData["KlientoUzsakymoId"] = id;

      return View();
    }

    // POST: IngredientoPrasymas/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(decimal kuiTPPR, decimal kiTPPR,
      decimal PRTID, string PRTPB, int PRTK, [Bind("KlientoUzsakymoId,TechnikosPrietaisoId,Prietaisokiekis,Busena")] TechnikosPrietaisoPrasymas technikosPrietaisoPrasymas)
    {
      decimal alertshowPrasymas2 = 0;
      technikosPrietaisoPrasymas.KlientoUzsakymoId = kuiTPPR;
      technikosPrietaisoPrasymas.TechnikosPrietaisoId = PRTID;
      technikosPrietaisoPrasymas.PrietaisoKiekis = PRTK;
      technikosPrietaisoPrasymas.Busena = PRTPB;
      var alaus_daryklos_gamybos_valdymas_DBContext3 = _context.TechnikosPrietaisoPrasymas.Include(i => i.TechnikosPrietaiso).Include(i => i.KlientoUzsakymo).
         Where(i => i.KlientoUzsakymoId == kuiTPPR);
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext3)
      {
        if (technikosPrietaisoPrasymas.TechnikosPrietaisoId == item.TechnikosPrietaisoId)
        {
          alertshowPrasymas2 = 2;
          return RedirectToAction(nameof(Redirect), new { id = kuiTPPR, alertshowPrasymas2 = alertshowPrasymas2 });
        }
      }
      if (ModelState.IsValid)
      {
        _context.Add(technikosPrietaisoPrasymas);
        await _context.SaveChangesAsync();
        var KlientoUzsakymoAplankas = await _context.KlientoUzsakymoAplankas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kuiTPPR && m.KlientoId == kiTPPR);
        if (KlientoUzsakymoAplankas.PrasymasTechnikaiReikalingas == null)
        {
          KlientoUzsakymoAplankas.PrasymasTechnikaiReikalingas = "T";
          if (ModelState.IsValid)
          {
            _context.Update(KlientoUzsakymoAplankas);
            await _context.SaveChangesAsync();
          }
        }
        alertshowPrasymas2++;
        return RedirectToAction(nameof(Redirect), new { id = kuiTPPR, alertshowPrasymas2 = alertshowPrasymas2 });
      }
      ViewData["TechnikosPrietaisoId"] = new SelectList(_context.TechnikosPrietaisas, "TechnikosPrietaisoId", "PrietaisoPavadinimas", technikosPrietaisoPrasymas.TechnikosPrietaisoId);
      ViewData["KlientoUzsakymoId"] = kuiTPPR;
      return RedirectToAction(nameof(Redirect), new { id = kuiTPPR, alertshowPrasymas2 = alertshowPrasymas2 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFROMINDEX(decimal kuiTPPR,
      decimal PRTID, string PRTPB, int PRTK, [Bind("KlientoUzsakymoId,TechnikosPrietaisoId,Prietaisokiekis,Busena")] TechnikosPrietaisoPrasymas technikosPrietaisoPrasymas)
    {
      technikosPrietaisoPrasymas.KlientoUzsakymoId = kuiTPPR;
      technikosPrietaisoPrasymas.TechnikosPrietaisoId = PRTID;
      technikosPrietaisoPrasymas.PrietaisoKiekis = PRTK;
      technikosPrietaisoPrasymas.Busena = PRTPB;
      if (ModelState.IsValid)
      {
        _context.Add(technikosPrietaisoPrasymas);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kuiTPPR});
      }
      ViewData["TechnikosPrietaisoId"] = new SelectList(_context.TechnikosPrietaisas, "TechnikosPrietaisoId", "PrietaisoPavadinimas", technikosPrietaisoPrasymas.TechnikosPrietaisoId);
      ViewData["KlientoUzsakymoId"] = kuiTPPR;
      return RedirectToAction(nameof(Index), new { id = kuiTPPR});
    }

    /*
    // GET: IngredientoPrasymas/Edit/5
    public async Task<IActionResult> Edit(decimal? id, decimal? id2)
    {
      if (id == null)
      {
        return NotFound();
      }
      if (id2 == null)
      {
        return NotFound();
      }
      var technikosPrietaisoPrasymas = _context.TechnikosPrietaisoPrasymas.SingleOrDefault(x => x.KlientoUzsakymoId == id && x.TechnikosPrietaisoId == id2);
      if (technikosPrietaisoPrasymas == null)
      {
        return NotFound();
      }
      ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas", technikosPrietaisoPrasymas.TechnikosPrietaisoId);
      ViewData["KlientoUzsakymoId"] = id;
      return View(technikosPrietaisoPrasymas);
    }
    */
    // POST: IngredientoPrasymas/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int techk, decimal tid, string techp, decimal kui)
    {
      var TechPrietPrasymas = await _context.TechnikosPrietaisoPrasymas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui
                  && m.TechnikosPrietaisoId == tid);
      TechPrietPrasymas.PrietaisoKiekis = techk;

      if (ModelState.IsValid)
      {
        _context.Update(TechPrietPrasymas);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kui });
      }
      ViewData["KlientoUzsakymoIdEdit"] = kui;

      return RedirectToAction(nameof(Index), new { id = kui });
    }

   /* // GET: IngredientoPrasymas/Delete/5
    public async Task<IActionResult> Delete(decimal? id, decimal? id2)
    {
      if (id == null)
      {
        return NotFound();
      }

      var technikosprietaisoprasymas = await _context.TechnikosPrietaisoPrasymas
          .Include(i => i.TechnikosPrietaiso)
          .Include(i => i.KlientoUzsakymo)
          .SingleOrDefaultAsync(i => i.KlientoUzsakymoId == id && i.TechnikosPrietaisoId == id2);
      if (technikosprietaisoprasymas == null)
      {
        return NotFound();
      }

      return View(technikosprietaisoprasymas);
    }
    */
    // POST: IngredientoPrasymas/Delete/5
    public async Task<IActionResult> DeleteConfirmed(decimal kui2, decimal tid2, int techk2)
    {
      var TechPrietPrasymas = await _context.TechnikosPrietaisoPrasymas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui2 &&
     m.TechnikosPrietaisoId == tid2 && m.PrietaisoKiekis == techk2);
      _context.TechnikosPrietaisoPrasymas.Remove(TechPrietPrasymas);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index), new { id = kui2 });
    }

    private bool TechnikosPrietaisoPrasymasExists(decimal id)
        {
            return _context.TechnikosPrietaisoPrasymas.Any(e => e.KlientoUzsakymoId == id);
        }
    }
}
