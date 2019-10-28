using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;

namespace AlausDaryklosGamybosValdymoSistema.Controllers.Entities_Foldeer
{
    public class TechnikosPrietaisoRezervacijaController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public TechnikosPrietaisoRezervacijaController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: TechnikosPrietaisoRezervacija
        public async Task<IActionResult> Index(decimal? id, decimal? techreservealert)
        {
      if (id == null)
      {
        return NotFound();
      }

      ViewData["PrietaisoReserveAlert"] = techreservealert;
      ViewData["PrietaisaiRezervacijoms"] = new SelectList(_context.TechnikosPrietaisas.Where(k => k.LaisvasKiekis != null).Join(_context.TechnikosPrietaisoPatarimas.Where(x => x.KlientoUzsakymoId == id),
        k => k.TechnikosPrietaisoId,
        e => e.TechnikosPrietaisoId,
        (k, e) => k), "TechnikosPrietaisoId", "PrietaisoPavadinimas");
      var alaus_daryklos_gamybos_valdymas_DBContextIndex = _context.TechnikosPrietaisoRezervacija.Include(i => i.TechnikosPrietaiso).Include(i => i.KlientoUzsakymo).
        Where(i => i.KlientoUzsakymoId == id);
      ViewData["KlientoUzsakymoId"] = id;
      return View(await alaus_daryklos_gamybos_valdymas_DBContextIndex.ToListAsync());
    }

        // GET: TechnikosPrietaisoRezervacija/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technikosPrietaisoRezervacija = await _context.TechnikosPrietaisoRezervacija
                .Include(t => t.KlientoUzsakymo)
                .Include(t => t.TechnikosPrietaiso)
                .FirstOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (technikosPrietaisoRezervacija == null)
            {
                return NotFound();
            }

            return View(technikosPrietaisoRezervacija);
        }

    
    public async Task<IActionResult> Create([Bind("KlientoUzsakymoId,TechnikosPrietaisoId,PrietaisoKiekis")] TechnikosPrietaisoRezervacija technikosPrietaisoRezervacija,
          decimal id, decimal? techreservealert, decimal? techformaddalert, decimal reservedTech,
      int reservedtechkiekis)
        {
      technikosPrietaisoRezervacija.KlientoUzsakymoId = id;
      technikosPrietaisoRezervacija.TechnikosPrietaisoId = reservedTech;
      technikosPrietaisoRezervacija.PrietaisoKiekis = reservedtechkiekis;

      var alaus_daryklos_gamybos_valdymas_DBContext = _context.TechnikosPrietaisoRezervacija.Include(i => i.TechnikosPrietaiso).Include(i => i.KlientoUzsakymo).
        Where(i => i.KlientoUzsakymoId == id).ToList();
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext)
      {
        if (techreservealert == 3)
        {
          if (technikosPrietaisoRezervacija.TechnikosPrietaisoId == item.TechnikosPrietaisoId)
          {
            item.PrietaisoKiekis = technikosPrietaisoRezervacija.PrietaisoKiekis + item.PrietaisoKiekis;
            if (ModelState.IsValid)
            {
              _context.Update(item);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Redirect), new { id = id, techreservealert = techreservealert });
            }
          }
        }
      }

      if (techreservealert == 3)
      {
        if (ModelState.IsValid)
        {
          _context.Add(technikosPrietaisoRezervacija);
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Redirect), new { id = id, techreservealert = techreservealert });
        }
      }
      return RedirectToAction(nameof(Redirect), new { id = id, techreservealert = techreservealert});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFROMINDEX(decimal kuiTPPR,
     decimal PRTID, int PRTK, [Bind("KlientoUzsakymoId,TechnikosPrietaisoId,PrietaisoKiekis")] TechnikosPrietaisoRezervacija technikosPrietaisoRezervacija)
    {

      decimal techreservealert = 0;
      var tech = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == PRTID);
      if (tech.LaisvasKiekis == null)
      {
        techreservealert++;
        return RedirectToAction(nameof(Index), new { id = kuiTPPR, techreservealert = techreservealert });
      }
      else
      {
        tech.LaisvasKiekis = tech.LaisvasKiekis - PRTK;
        if (tech.LaisvasKiekis < 0)
        {
          techreservealert = 2;
          return RedirectToAction(nameof(Index), new { id = kuiTPPR, techreservealert = techreservealert });
        }
        if (tech.LaisvasKiekis == 0)
        {
          tech.LaisvasKiekis = null;
        }
      }
      if (ModelState.IsValid)
      {
        _context.Update(tech);
        await _context.SaveChangesAsync();
        techreservealert = 3;
      }
      technikosPrietaisoRezervacija.KlientoUzsakymoId = kuiTPPR;
      technikosPrietaisoRezervacija.TechnikosPrietaisoId = PRTID;
      technikosPrietaisoRezervacija.PrietaisoKiekis = PRTK;

      var alaus_daryklos_gamybos_valdymas_DBContextIndex = _context.TechnikosPrietaisoRezervacija.Include(i => i.TechnikosPrietaiso).Include(i => i.KlientoUzsakymo).
        Where(i => i.KlientoUzsakymoId == kuiTPPR).ToList();
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContextIndex)
      {
        if (technikosPrietaisoRezervacija.TechnikosPrietaisoId == item.TechnikosPrietaisoId)
        {
          item.PrietaisoKiekis = technikosPrietaisoRezervacija.PrietaisoKiekis + item.PrietaisoKiekis;
          if (ModelState.IsValid)
          {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Redirect), new { id = kuiTPPR, techreservealert = techreservealert });
          }

        }
      }
          if (ModelState.IsValid)
      {
        _context.Add(technikosPrietaisoRezervacija);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kuiTPPR, techreservealert = techreservealert });
      }
      return RedirectToAction(nameof(Index), new { id = kuiTPPR, techreservealert = techreservealert });
    }

    public IActionResult Redirect(decimal? id, decimal? techreservealert)
    {
      string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new
      {
        id = id,
        techreservealert = techreservealert,
      });
      return Redirect(url);
    }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int techk, decimal tid, string techp, decimal kui)
        {

      decimal techreservealert = 0;
      var TechPrietRezervacija = await _context.TechnikosPrietaisoRezervacija.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui
               && m.TechnikosPrietaisoId == tid);
      var tech = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == tid);
      if (tech.LaisvasKiekis == null && TechPrietRezervacija.PrietaisoKiekis > techk)
      {
        tech.LaisvasKiekis = TechPrietRezervacija.PrietaisoKiekis - techk;
      }
      else if (tech.LaisvasKiekis == null)
      {
        techreservealert++;
        return RedirectToAction(nameof(Index), new
        {
          id = kui,
          techreservealert = techreservealert
        });
      }
      else
      {
        tech.LaisvasKiekis = tech.LaisvasKiekis + (TechPrietRezervacija.PrietaisoKiekis - techk);
        if (tech.LaisvasKiekis < 0)
        {
          techreservealert = 2;
          return RedirectToAction(nameof(Index), new
          {
            id = kui,
            techreservealert = techreservealert
          });
        }
        if (tech.LaisvasKiekis == 0)
        {
          tech.LaisvasKiekis = null;
        }
      }
      TechPrietRezervacija.PrietaisoKiekis = techk;
      if (ModelState.IsValid)
      {
        _context.Update(tech);
        await _context.SaveChangesAsync();
      }
      if (ModelState.IsValid)
      {
        _context.Update(TechPrietRezervacija);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new {
          id = kui,
          techreservealert = techreservealert
        });
      }
      ViewData["KlientoUzsakymoIdEdit"] = kui;

      return RedirectToAction(nameof(Index), new {
        id = kui,
        techreservealert = techreservealert
      });
    }

       
        public async Task<IActionResult> DeleteConfirmed(decimal kui2, decimal tid2, int techk2)
        {
      var TechPrietRezervacija = await _context.TechnikosPrietaisoRezervacija.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui2 &&
m.TechnikosPrietaisoId == tid2 && m.PrietaisoKiekis == techk2);
      _context.Remove(TechPrietRezervacija);
      await _context.SaveChangesAsync();
      var tech = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == tid2);
      if (tech.LaisvasKiekis == null)
      {
        tech.LaisvasKiekis = TechPrietRezervacija.PrietaisoKiekis;
      }
      else {
        tech.LaisvasKiekis = tech.LaisvasKiekis + techk2;
      }
      if (ModelState.IsValid)
      {
        _context.Update(tech);
        await _context.SaveChangesAsync();
      }
      return RedirectToAction(nameof(Index), new { id = kui2 });
    }

        private bool TechnikosPrietaisoRezervacijaExists(decimal id)
        {
            return _context.TechnikosPrietaisoRezervacija.Any(e => e.KlientoUzsakymoId == id);
        }
    }
}
