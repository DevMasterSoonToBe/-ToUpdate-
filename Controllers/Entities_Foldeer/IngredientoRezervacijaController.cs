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
    public class IngredientoRezervacijaController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public IngredientoRezervacijaController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: IngredientoRezervacija
        public async Task<IActionResult> Index(decimal? id, decimal? ingrreservealert)
        {
      if (id == null)
      {
        return NotFound();
      }

      ViewData["IngredientoReserveAlert"] = ingrreservealert;
      ViewData["IngredientaiRezervacijoms"] = new SelectList(_context.Ingredientas.Where(k => k.LaisvasKiekis != null).Join(_context.IngredientoReceptas.Where(x => x.KlientoUzsakymoId == id),
          k => k.IngredientoId,
          e => e.IngredientoId,
          (k, e) => k), "IngredientoId", "IngredientoPavadinimas");
      var alaus_daryklos_gamybos_valdymas_DBContextIndex = _context.IngredientoRezervacija.Include(i => i.Ingrediento).Include(i => i.KlientoUzsakymo).
        Where(i => i.KlientoUzsakymoId == id);
      ViewData["KlientoUzsakymoId"] = id;
      return View(await alaus_daryklos_gamybos_valdymas_DBContextIndex.ToListAsync());
    }

        // GET: IngredientoRezervacija/Details/5
        public async Task<IActionResult> Details(decimal? id, decimal? ingrreservealert)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientoRezervacija = await _context.IngredientoRezervacija
                .Include(i => i.Ingrediento)
                .Include(i => i.KlientoUzsakymo)
                .FirstOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (ingredientoRezervacija == null)
            {
                return NotFound();
            }
      ViewData["IngrReservedAlert"] = ingrreservealert;
            return View(ingredientoRezervacija);
        }


    
    public async Task<IActionResult> Create([Bind("KlientoUzsakymoId,IngredientoId,IngredientoKiekis")] IngredientoRezervacija ingredientoRezervacija, decimal id,
          decimal? ingrreservealert,
      decimal reservedIngr, float reservedingrkiekis)
        {

      ingredientoRezervacija.KlientoUzsakymoId = id;
      ingredientoRezervacija.IngredientoId = reservedIngr;
      ingredientoRezervacija.IngredientoKiekis = reservedingrkiekis;

      var alaus_daryklos_gamybos_valdymas_DBContext = _context.IngredientoRezervacija.Include(i => i.Ingrediento).Include(i => i.KlientoUzsakymo).
        Where(i => i.KlientoUzsakymoId == id).ToList();
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext)
      {
        if (ingrreservealert == 3)
        {
          if (ingredientoRezervacija.IngredientoId == item.IngredientoId)
          {
            item.IngredientoKiekis = ingredientoRezervacija.IngredientoKiekis + item.IngredientoKiekis;
            if (ModelState.IsValid)
            {
              _context.Update(item);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Redirect), new { id = id, ingrreservealert = ingrreservealert });
            }
          }
        }
      }

      if (ingrreservealert == 3)
      {
        if (ModelState.IsValid)
        {
          _context.Add(ingredientoRezervacija);
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Redirect), new { id = id, ingrreservealert = ingrreservealert });
        }
      }
      return RedirectToAction(nameof(Redirect), new { id = id, ingrreservealert = ingrreservealert});
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFROMINDEX(decimal iidR, float lkR, decimal kuiInR,
      [Bind("KlientoUzsakymoId,IngredientoId,IngredientoKiekis")] IngredientoRezervacija ingredientoRezervacija)
    {
      decimal ingrreservealert = 0;
      var ingr = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == iidR);
      if (ingr.LaisvasKiekis == null)
      {
        ingrreservealert++;
        return RedirectToAction(nameof(Index), new { id = kuiInR, ingrreservealert = ingrreservealert });
      }
      else
      {
        ingr.LaisvasKiekis = ingr.LaisvasKiekis - lkR;
        if (ingr.LaisvasKiekis < 0)
        {
          ingrreservealert = 2;
          return RedirectToAction(nameof(Index), new { id = kuiInR, ingrreservealert = ingrreservealert });
        }
        if (ingr.LaisvasKiekis == 0)
        {
          ingr.LaisvasKiekis = null;
        }
      }
      if (ModelState.IsValid)
      {
        _context.Update(ingr);
        await _context.SaveChangesAsync();
        ingrreservealert = 3;
      }
      ingredientoRezervacija.KlientoUzsakymoId = kuiInR;
      ingredientoRezervacija.IngredientoId = iidR;
      ingredientoRezervacija.IngredientoKiekis = lkR;
      var alaus_daryklos_gamybos_valdymas_DBContextIndex = _context.IngredientoRezervacija.Include(i => i.Ingrediento).Include(i => i.KlientoUzsakymo).
        Where(i => i.KlientoUzsakymoId == kuiInR).ToList();
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContextIndex)
      {
        if (ingredientoRezervacija.IngredientoId == item.IngredientoId)
        {
          item.IngredientoKiekis = ingredientoRezervacija.IngredientoKiekis + item.IngredientoKiekis;
          if (ModelState.IsValid)
          {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Redirect), new { id = kuiInR, ingrreservealert = ingrreservealert });
          }

        }
      }
      if (ModelState.IsValid)
      {
        _context.Add(ingredientoRezervacija);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kuiInR, ingrreservealert = ingrreservealert });
      }
      return RedirectToAction(nameof(Index), new { id = kuiInR, ingrreservealert = ingrreservealert });
    }


    // Redirectas i kliento uzsakymo aplanka
    public IActionResult Redirect(decimal? id, decimal? ingrreservealert)
    {
      string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new
      {
        id = id,
        ingrreservealert = ingrreservealert
      });
      return Redirect(url);
    }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(float ingrk, decimal iid, string ingrp, decimal kui)
        {

      decimal ingrreservealert = 0;
      var ingredientoRezervacija = await _context.IngredientoRezervacija.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui
      && m.IngredientoId == iid);
      var ingr = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == iid);
      
      if (ingr.LaisvasKiekis == null && ingredientoRezervacija.IngredientoKiekis > ingrk)
      {
        ingr.LaisvasKiekis = ingredientoRezervacija.IngredientoKiekis - ingrk;
      }
      else if (ingr.LaisvasKiekis == null)
      {
        ingrreservealert++;
        return RedirectToAction(nameof(Index), new { id = kui, ingrreservealert = ingrreservealert });
      }
      else
      {
        ingr.LaisvasKiekis = ingr.LaisvasKiekis + (ingredientoRezervacija.IngredientoKiekis - ingrk);
        if (ingr.LaisvasKiekis < 0)
        {
          ingrreservealert = 2;
          return RedirectToAction(nameof(Index), new
          {
            id = kui,
            ingrreservealert = ingrreservealert
          });
        }
        if (ingr.LaisvasKiekis == 0)
        {
          ingr.LaisvasKiekis = null;
        }
      }
      ingredientoRezervacija.IngredientoKiekis = ingrk;
      if (ModelState.IsValid)
      {
        _context.Update(ingr);
        await _context.SaveChangesAsync();
      }
      if (ModelState.IsValid)
      {
        _context.Update(ingredientoRezervacija);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kui,
          ingrreservealert = ingrreservealert
        });
      }
      ViewData["KlientoUzsakymoIdEdit"] = kui;

      return RedirectToAction(nameof(Index), new { id = kui,
        ingrreservealert = ingrreservealert
      });
    }

     
        public async Task<IActionResult> DeleteConfirmed(decimal kui2, decimal iid2, float ingrk2)
        {
      var ingredientoRezervacija = await _context.IngredientoRezervacija.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui2 &&
m.IngredientoId == iid2 && m.IngredientoKiekis == ingrk2);
      _context.Remove(ingredientoRezervacija);
      await _context.SaveChangesAsync();
      var ingr = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == iid2);
      if (ingr.LaisvasKiekis == null)
      {
        ingr.LaisvasKiekis = ingredientoRezervacija.IngredientoKiekis;
      }
      else
      {
        ingr.LaisvasKiekis = ingr.LaisvasKiekis + ingrk2;
      }
      if (ModelState.IsValid)
      {
        _context.Update(ingr);
        await _context.SaveChangesAsync();
      }
      return RedirectToAction(nameof(Index), new { id = kui2 });
    }

        private bool IngredientoRezervacijaExists(decimal id)
        {
            return _context.IngredientoRezervacija.Any(e => e.KlientoUzsakymoId == id);
        }
    }
}
