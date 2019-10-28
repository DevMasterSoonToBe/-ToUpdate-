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
    public class IngredientasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public IngredientasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: Ingredientas
        public async Task<IActionResult> Index()
        {
            var alaus_daryklos_gamybos_valdymas_DBContext = _context.Ingredientas.Include(i => i.Formos);
            return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
        }

        // GET: Ingredientas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientas = await _context.Ingredientas
                .Include(i => i.Formos)
                .SingleOrDefaultAsync(m => m.IngredientoId == id);
            if (ingredientas == null)
            {
                return NotFound();
            }

            return View(ingredientas);
        }

        // GET: Ingredientas/Create
        /*public IActionResult Create()
        {
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas");
            return View();
        }

        // POST: Ingredientas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598. */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngredientoId,FormosId,IngredientoPavadinimas,LaisvasKiekis")] Ingredientas ingredientas, decimal kui2, decimal Ini2, string ingrr, string a33,
          string IB3, string RP3, string TP3, DateTime GT3)
        {
      decimal ingraddalert = 0;
      ingredientas.IngredientoPavadinimas = ingrr;
            if (ModelState.IsValid)
            {
                _context.Add(ingredientas);
                await _context.SaveChangesAsync();
        ingraddalert++;
        return RedirectToAction(nameof(Redirect2), new { id = kui2, id2 = Ini2, a3=a33, IB3 = IB3, GT3 = GT3, RP3 = RP3, TP3 = TP3, ingraddalert = ingraddalert });
      }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", ingredientas.FormosId);
      return RedirectToAction(nameof(Redirect2), new { id = kui2, id2 = Ini2, a3=a33, IB3 = IB3, GT3 = GT3, RP3 = RP3, TP3 = TP3, ingraddalert = ingraddalert });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddtoLaisvosZaliavos(decimal iid, float lk, decimal fid2, decimal kuiIn)
    {
      decimal ingrformaddalert = 0;
      var ingr = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == iid);
      ingr.FormosId = fid2;
      if (ingr.LaisvasKiekis == null)
      {
        ingr.LaisvasKiekis = lk;
      }
      else
      {
        ingr.LaisvasKiekis = ingr.LaisvasKiekis + lk;
      }
      if (ModelState.IsValid)
      {
        _context.Update(ingr);
        await _context.SaveChangesAsync();
        ingrformaddalert++;
        return RedirectToAction(nameof(Redirect), new { id = kuiIn, ingrformaddalert = ingrformaddalert });
      }
      ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", ingr.FormosId);
      return RedirectToAction(nameof(Redirect), new { id = kuiIn, ingrformaddalert = ingrformaddalert });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReserveLaisvosZaliavos(decimal iidR, float lkR, decimal fid2R, decimal kuiInR)
    {
      decimal ingrreservealert = 0;
      var ingr = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == iidR);
      decimal reservedIngr = iidR;
      var reservedingrkiekis = lkR;
      ingr.FormosId = fid2R;
      if (ingr.LaisvasKiekis == null)
      {
        ingrreservealert++;
        return RedirectToAction(nameof(RedirectReserve), new { id = kuiInR, ingrreservealert = ingrreservealert, reservedIngr = reservedIngr,
          reservedingrkiekis = reservedingrkiekis});
      }
      else
      {
        ingr.LaisvasKiekis = ingr.LaisvasKiekis - lkR;
        if (ingr.LaisvasKiekis < 0) {
          ingrreservealert = 2;
          return RedirectToAction(nameof(RedirectReserve), new { id = kuiInR, ingrreservealert = ingrreservealert,
            reservedIngr = reservedIngr,
            reservedingrkiekis = reservedingrkiekis
          });
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
        
        return RedirectToAction(nameof(RedirectReserve), new { id = kuiInR, ingrreservealert = ingrreservealert,
          reservedIngr = reservedIngr,
          reservedingrkiekis = reservedingrkiekis
        });
      }
      return RedirectToAction(nameof(RedirectReserve), new { id = kuiInR, ingrreservealert = ingrreservealert,
        reservedIngr = reservedIngr,
        reservedingrkiekis = reservedingrkiekis
      });
    }
    public IActionResult RedirectReserve(decimal? id, decimal? ingrreservealert,
      decimal? reservedIngr, float reservedingrkiekis)
    {
      string url = @Url.Action("Create", "IngredientoRezervacija", new { id = id, ingrreservealert =  ingrreservealert,
        reservedIngr = reservedIngr,
        reservedingrkiekis = reservedingrkiekis
      });
      return Redirect(url);
    }

    public IActionResult Redirect(decimal? id, decimal? ingrformaddalert)
    {
      string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new {
        id = id,
        ingrformaddalert = ingrformaddalert
      });
      return Redirect(url);
    }
    public IActionResult Redirect2(decimal? id, decimal? id2, string a3, decimal? ingraddalert,
      string IB3, string RP3, string TP3, DateTime GT3)
    {
      string url = @Url.Action("Create", "IngredientoReceptas", new { id = id, id2 = id2, a3 = a3, ingraddalert = ingraddalert, a1 = IB3, a2 = GT3, a4 = RP3, a5 = TP3 });
      return Redirect(url);
    }
    // GET: Ingredientas/Edit/5
    public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientas = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == id);
            if (ingredientas == null)
            {
                return NotFound();
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", ingredientas.FormosId);
            return View(ingredientas);
        }

        // POST: Ingredientas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IngredientoId,FormosId,IngredientoPavadinimas,LaisvasKiekis")] Ingredientas ingredientas)
        {
            if (id != ingredientas.IngredientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredientas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientasExists(ingredientas.IngredientoId))
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
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", ingredientas.FormosId);
            return View(ingredientas);
        }

        // GET: Ingredientas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientas = await _context.Ingredientas
                .Include(i => i.Formos)
                .SingleOrDefaultAsync(m => m.IngredientoId == id);
            if (ingredientas == null)
            {
                return NotFound();
            }

            return View(ingredientas);
        }

        // POST: Ingredientas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var ingredientas = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == id);
            _context.Ingredientas.Remove(ingredientas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientasExists(decimal id)
        {
            return _context.Ingredientas.Any(e => e.IngredientoId == id);
        }
    }
}
