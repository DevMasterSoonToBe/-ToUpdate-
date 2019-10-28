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
    public class TechnikosPrietaisasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public TechnikosPrietaisasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: TechnikosPrietaisas
        public async Task<IActionResult> Index()
        {
            var alaus_daryklos_gamybos_valdymas_DBContext = _context.TechnikosPrietaisas.Include(t => t.Formos);
            return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
        }

        // GET: TechnikosPrietaisas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technikosPrietaisas = await _context.TechnikosPrietaisas
                .Include(t => t.Formos)
                .SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == id);
            if (technikosPrietaisas == null)
            {
                return NotFound();
            }

            return View(technikosPrietaisas);
        }

        // GET: TechnikosPrietaisas/Create
        public IActionResult Create()
        {
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas");
            return View();
        }

        // POST: TechnikosPrietaisas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TechnikosPrietaisoId,FormosId,PrietaisoPavadinimas,LaisvasKiekis")] TechnikosPrietaisas technikosPrietaisas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technikosPrietaisas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", technikosPrietaisas.FormosId);
            return View(technikosPrietaisas);
        }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddtoLaisvaTechnika(decimal tpd, int lk, decimal fid2, decimal kuiTN)
    {
      decimal techformaddalert = 0;
      var tech = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == tpd);
      if (tech.LaisvasKiekis == null)
      {
        tech.LaisvasKiekis = lk;
      }
      else
      {
        tech.LaisvasKiekis = tech.LaisvasKiekis + lk;
      }
      tech.FormosId = fid2;
      if (ModelState.IsValid)
      {
        _context.Update(tech);
        await _context.SaveChangesAsync();
        techformaddalert++;
        return RedirectToAction(nameof(Redirect), new { id = kuiTN, techformaddalert = techformaddalert });
      }
      ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", tech.FormosId);
      return RedirectToAction(nameof(Redirect), new { id = kuiTN, techformaddalert = techformaddalert });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReserveLaisvaTechnika(decimal tpdR, int lkR, decimal fid2R, decimal kuiTNR)
    {
      decimal techreservealert = 0;
      var tech = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == tpdR);
      decimal reservedTech = tpdR;
      var reservedtechkiekis = lkR;
      if (tech.LaisvasKiekis == null)
      {
        techreservealert++;
        return RedirectToAction(nameof(RedirectReserve), new { id = kuiTNR, techreservealert = techreservealert, reservedTech = reservedTech,
          reservedtechkiekis = reservedtechkiekis
        });
      }
      else
      {
        tech.LaisvasKiekis = tech.LaisvasKiekis - lkR;
        if (tech.LaisvasKiekis < 0)
        {
          techreservealert = 2;
          return RedirectToAction(nameof(RedirectReserve), new { id = kuiTNR, techreservealert = techreservealert,
            reservedTech = reservedTech,
            reservedtechkiekis = reservedtechkiekis
          });
        }
        if (tech.LaisvasKiekis == 0)
        {
          tech.LaisvasKiekis = null;
        }
      }
      tech.FormosId = fid2R;
      if (ModelState.IsValid)
      {
        _context.Update(tech);
        await _context.SaveChangesAsync();
        techreservealert = 3;
        return RedirectToAction(nameof(RedirectReserve), new { id = kuiTNR, techreservealert = techreservealert,
          reservedTech = reservedTech,
          reservedtechkiekis = reservedtechkiekis
        });
      }
      return RedirectToAction(nameof(RedirectReserve), new
      {
        id = kuiTNR,
        techreservealert = techreservealert,
        reservedTech = reservedTech,
        reservedtechkiekis = reservedtechkiekis
      });
    }
    public IActionResult RedirectReserve(decimal? id, decimal? techreservealert,
         decimal? reservedTech, float reservedtechkiekis)
    {
      string url = @Url.Action("Create", "TechnikosPrietaisoRezervacija", new
      {
        id = id,
        techreservealert = techreservealert,
        reservedTech = reservedTech,
        reservedtechkiekis = reservedtechkiekis
      });
      return Redirect(url);
    }
    public IActionResult Redirect(decimal? id, decimal? techformaddalert)
    {
      string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new
      {
        id = id,
        techformaddalert = techformaddalert
      });
      return Redirect(url);
    }
    // GET: TechnikosPrietaisas/Edit/5
    public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technikosPrietaisas = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == id);
            if (technikosPrietaisas == null)
            {
                return NotFound();
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", technikosPrietaisas.FormosId);
            return View(technikosPrietaisas);
        }

        // POST: TechnikosPrietaisas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("TechnikosPrietaisoId,FormosId,PrietaisoPavadinimas,LaisvasKiekis")] TechnikosPrietaisas technikosPrietaisas)
        {
            if (id != technikosPrietaisas.TechnikosPrietaisoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technikosPrietaisas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnikosPrietaisasExists(technikosPrietaisas.TechnikosPrietaisoId))
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
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", technikosPrietaisas.FormosId);
            return View(technikosPrietaisas);
        }

        // GET: TechnikosPrietaisas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technikosPrietaisas = await _context.TechnikosPrietaisas
                .Include(t => t.Formos)
                .SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == id);
            if (technikosPrietaisas == null)
            {
                return NotFound();
            }

            return View(technikosPrietaisas);
        }

        // POST: TechnikosPrietaisas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var technikosPrietaisas = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == id);
            _context.TechnikosPrietaisas.Remove(technikosPrietaisas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnikosPrietaisasExists(decimal id)
        {
            return _context.TechnikosPrietaisas.Any(e => e.TechnikosPrietaisoId == id);
        }
    }
}
