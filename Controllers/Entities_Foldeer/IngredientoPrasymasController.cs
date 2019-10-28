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
    public class IngredientoPrasymasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public IngredientoPrasymasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
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

      ViewData["IngredientaiPrasymui"] = new SelectList(_context.Ingredientas.Join(_context.IngredientoReceptas.Where(x => x.KlientoUzsakymoId == id),
  k => k.IngredientoId,
  e => e.IngredientoId,
  (k, e) => k), "IngredientoId", "IngredientoPavadinimas");
      var alaus_daryklos_gamybos_valdymas_DBContext = _context.IngredientoPrasymas.Include(i => i.Ingrediento).Include(i => i.KlientoUzsakymo).
        Where(i => i.KlientoUzsakymoId == id);
      ViewData["KlientoUzsakymoId"] = id;
      return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
        }

    public async Task<IActionResult> ChangeState(decimal kui, string NB)
    {
      var alaus_daryklos_gamybos_valdymas_DBContext5 = _context.IngredientoPrasymas.Include(i => i.Ingrediento).Include(i => i.KlientoUzsakymo).
         Where(i => i.KlientoUzsakymoId == kui).ToList();
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext5)
      {
        item.Busena = NB;
        item.KlientoUzsakymoId = kui;
        item.IngredientoId = item.IngredientoId;
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
            if (!IngredientoPrasymasExists(item.KlientoUzsakymoId))
            {
              return NotFound();
            }
            else if (!IngredientoPrasymasExists(item.IngredientoId))
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
    public async Task<IActionResult> ChangeStateFORM(decimal kuiINGRPR, string NB)
    {
      decimal IngrPrasAlert = 0;
      var alaus_daryklos_gamybos_valdymas_DBContext4 = _context.IngredientoPrasymas.Include(i => i.Ingrediento).Include(i => i.KlientoUzsakymo).
         Where(i => i.KlientoUzsakymoId == kuiINGRPR).ToList();
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext4)
      {
        item.Busena = NB;
        item.KlientoUzsakymoId = kuiINGRPR;
        item.IngredientoId = item.IngredientoId;
        if (!ModelState.IsValid)
        {
          return RedirectToAction(nameof(Redirect), new { id = kuiINGRPR });

        }
        if (ModelState.IsValid)
        {
          try
          {
            _context.Update(item);
            await _context.SaveChangesAsync();
            IngrPrasAlert = 1;
          }
          catch (DbUpdateConcurrencyException)
          {
            if (!IngredientoPrasymasExists(item.KlientoUzsakymoId))
            {
              return NotFound();
            }
            else if (!IngredientoPrasymasExists(item.IngredientoId))
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
      return RedirectToAction(nameof(Redirect), new { id = kuiINGRPR, IngrPrasAlert = IngrPrasAlert });
    }


    // GET: IngredientoPrasymas/Details/5
    public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientoPrasymas = await _context.IngredientoPrasymas
                .Include(i => i.Ingrediento)
                .Include(i => i.KlientoUzsakymo)
                .SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (ingredientoPrasymas == null)
            {
                return NotFound();
            }

            return View(ingredientoPrasymas);
        }

        // GET: IngredientoPrasymas/Create
       /* public IActionResult Create(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var IngredientoPrasymas = _context.IngredientoPrasymas.Include(m => m.KlientoUzsakymoId == id);

         
            int i;
            
            /*var Ingredientas = new Ingredientas { IngredientoId = 0, IngredientoPavadinimas = "Naujai įvedamas ingredientas" };
            _context.Ingredientas.Add(Ingredientas);
            _context.SaveChanges();*/
            /*ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas");
            ViewData["KlientoUzsakymoId"] = id;
            
            return View();
        }
  */
        // POST: IngredientoPrasymas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal kuiInPR, decimal kiInPR, decimal PRIID, string PRIPB, float PRIK,
          [Bind("KlientoUzsakymoId,IngredientoId,IngredientoKiekis,Busena")] IngredientoPrasymas ingredientoPrasymas)
        {
      decimal alertshowPrasymas = 0;
      ingredientoPrasymas.KlientoUzsakymoId = kuiInPR;
      ingredientoPrasymas.IngredientoId = PRIID;
      ingredientoPrasymas.IngredientoKiekis = PRIK;
      ingredientoPrasymas.Busena = PRIPB;
      var alaus_daryklos_gamybos_valdymas_DBContext3 = _context.IngredientoPrasymas.Include(i => i.Ingrediento).Include(i => i.KlientoUzsakymo).
         Where(i => i.KlientoUzsakymoId == kuiInPR);
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext3)
      {
        if (ingredientoPrasymas.IngredientoId == item.IngredientoId)
        {
          alertshowPrasymas = 2;
          return RedirectToAction(nameof(Redirect), new { id = kuiInPR, alertshowPrasymas = alertshowPrasymas });
        }
      }
      if (ModelState.IsValid)
            {
                _context.Add(ingredientoPrasymas);
                await _context.SaveChangesAsync();
        var KlientoUzsakymoAplankas = await _context.KlientoUzsakymoAplankas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kuiInPR && m.KlientoId == kiInPR);
        if (KlientoUzsakymoAplankas.PrasymasZaliavomsReikalingas == null)
        {
          KlientoUzsakymoAplankas.PrasymasZaliavomsReikalingas = "T";
          if (ModelState.IsValid)
          {
            _context.Update(KlientoUzsakymoAplankas);
            await _context.SaveChangesAsync();
          }
        }
          alertshowPrasymas++;
          return RedirectToAction(nameof(Redirect), new { id = kuiInPR, alertshowPrasymas = alertshowPrasymas });
            }
            ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas", ingredientoPrasymas.IngredientoId);
            ViewData["KlientoUzsakymoId"] = kuiInPR;
      return RedirectToAction(nameof(Redirect), new { id = kuiInPR });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFROMINDEX(decimal kuiInPR, decimal PRIID, string PRIPB, float PRIK,
          [Bind("KlientoUzsakymoId,IngredientoId,IngredientoKiekis,Busena")] IngredientoPrasymas ingredientoPrasymas)
    {
      ingredientoPrasymas.KlientoUzsakymoId = kuiInPR;
      ingredientoPrasymas.IngredientoId = PRIID;
      ingredientoPrasymas.IngredientoKiekis = PRIK;
      ingredientoPrasymas.Busena = PRIPB;
      if (ModelState.IsValid)
      {
        _context.Add(ingredientoPrasymas);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kuiInPR});
      }
      ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas", ingredientoPrasymas.IngredientoId);
      ViewData["KlientoUzsakymoId"] = kuiInPR;
      return RedirectToAction(nameof(Index), new { id = kuiInPR });
    }
    public IActionResult Redirect(decimal? id, decimal? alertshowPrasymas, decimal? IngrPrasAlert)
        {
            string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new { id = id, alertshowPrasymas = alertshowPrasymas, IngrPrasAlert = IngrPrasAlert });
            return Redirect(url);
        }

     /*   // GET: IngredientoPrasymas/Edit/5
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
      var ingredientoPrasymas = _context.IngredientoPrasymas.SingleOrDefault(x => x.KlientoUzsakymoId == id && x.IngredientoId == id2);
            if (ingredientoPrasymas == null)
            {
                return NotFound();
            }
            ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas", ingredientoPrasymas.IngredientoId);
      ViewData["KlientoUzsakymoId"] = id;
            return View(ingredientoPrasymas);
        }*/

        // POST: IngredientoPrasymas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(float ingrk, decimal iid, string ingrp, decimal kui)
        {
      var ingredientoPrasymas = await _context.IngredientoPrasymas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui
            && m.IngredientoId == iid);
      ingredientoPrasymas.IngredientoKiekis = ingrk;

      if (ModelState.IsValid)
      {
        _context.Update(ingredientoPrasymas);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kui});
      }
      ViewData["KlientoUzsakymoIdEdit"] = kui;

      return RedirectToAction(nameof(Index), new { id = kui});
    }

        // GET: IngredientoPrasymas/Delete/5
     /*   public async Task<IActionResult> Delete(decimal? id, decimal? id2)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientoPrasymas = await _context.IngredientoPrasymas
                .Include(i => i.Ingrediento)
                .Include(i => i.KlientoUzsakymo)
                .SingleOrDefaultAsync(i => i.KlientoUzsakymoId == id && i.IngredientoId == id2);
            if (ingredientoPrasymas == null)
            {
                return NotFound();
            }

            return View(ingredientoPrasymas);
        }
        */
        // POST: IngredientoPrasymas/Delete/5

        public async Task<IActionResult> DeleteConfirmed(decimal kui2, decimal iid2, float ingrk2)
        {
      var ingredientoPrasymas = await _context.IngredientoPrasymas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui2 &&
      m.IngredientoId == iid2 && m.IngredientoKiekis == ingrk2);
      _context.IngredientoPrasymas.Remove(ingredientoPrasymas);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index), new { id = kui2 });
    }

        private bool IngredientoPrasymasExists(decimal id)
        {
            return _context.IngredientoPrasymas.Any(e => e.KlientoUzsakymoId == id);
        }
    }
}
