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
    public class LaisviResursaisController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public LaisviResursaisController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: LaisviResursais
        public async Task<IActionResult> Index()
        {
            return View(await _context.LaisviResursai.ToListAsync());
        }

        // GET: Laisva technika details
        public async Task<IActionResult> DetailsLaisvaTechnika(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }
      ViewData["TechnikosPrietaisoId"] = new SelectList(_context.TechnikosPrietaisas, "TechnikosPrietaisoId", "PrietaisoPavadinimas");
      var laisviResursaiT = await _context.LaisviResursai.Include(m => m.TechnikosPrietaisas)
                .SingleOrDefaultAsync(m => m.FormosId == id);
            if (laisviResursaiT == null)
            {
                return NotFound();
            }

            return View(laisviResursaiT);
        }
    // GET: Laisvos zaliavos details
    public async Task<IActionResult> DetailsLaisvosZaliavos(decimal? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas");

      var laisviResursaiZ = await _context.LaisviResursai.Include(m => m.Ingredientas)
          .SingleOrDefaultAsync(m => m.FormosId == id);
      if (laisviResursaiZ == null)
      {
        return NotFound();
      }
      return View(laisviResursaiZ);
    }
   /* [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddRes(decimal tpd2, float lk2, decimal fid3)
    {
      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technikos vadovas") || User.IsInRole("Gamybos vadovas") || User.IsInRole("Gamybos meistras"))
        {
          if (fid3 == 2)
          {
            var tech = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == tpd2);
            tech.FormosId = fid3;
            tech.LaisvasKiekis = lk2;
            if (ModelState.IsValid)
            {
              _context.Update(tech);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(DetailsLaisvaTechnika), new { id = fid3 });
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuKodas", tech.FormosId);
            return RedirectToAction(nameof(DetailsLaisvaTechnika), new { id = fid3 });
          }
        }
      }
      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technologas") || User.IsInRole("Gamybos vadovas") || User.IsInRole("Gamybos meistras"))
        {
          if (fid3 == 1)
          {
            var ingr = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == tpd2);
            ingr.FormosId = fid3;
            ingr.LaisvasKiekis = lk2;
            if (ModelState.IsValid)
            {
              _context.Update(ingr);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(DetailsLaisvosZaliavos), new { id = fid3 });
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuKodas", ingr.FormosId);
            return RedirectToAction(nameof(DetailsLaisvosZaliavos), new { id = fid3 });
          }
        }
      }
      return NotFound();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRes(decimal tpd,decimal tpd3, float lk, decimal fid2)
    {
      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technikos vadovas") || User.IsInRole("Gamybos vadovas") || User.IsInRole("Gamybos meistras"))
        {
          if (fid2 == 2)
          {
            var tech = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == tpd && m.FormosId == fid2);
            tech.LaisvasKiekis = lk;
            if (ModelState.IsValid)
            {
              _context.Update(tech);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(DetailsLaisvaTechnika), new { id = fid2 });
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuKodas", tech.FormosId);
            return RedirectToAction(nameof(DetailsLaisvaTechnika), new { id = fid2 });
          }
        }
      }

      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technologas") || User.IsInRole("Gamybos vadovas") || User.IsInRole("Gamybos meistras"))
        {
          if (fid2 == 1)
          {
            var ingr = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == tpd && m.FormosId == fid2);
            ingr.LaisvasKiekis = lk;
            if (ModelState.IsValid)
            {
              _context.Update(ingr);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(DetailsLaisvosZaliavos), new { id = fid2 });
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuKodas", ingr.FormosId);
            return RedirectToAction(nameof(DetailsLaisvosZaliavos), new { id = fid2 });
          }
        }
      }
      return NotFound();
    }

    public async Task<IActionResult> DeletefromForm(decimal tpd3, float lk3, decimal fid4)
    {
      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technikos vadovas") || User.IsInRole("Gamybos vadovas") || User.IsInRole("Gamybos meistras"))
        {
          if (fid4 == 2)
          {
            var tech = await _context.TechnikosPrietaisas.SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == tpd3 && m.FormosId == fid4 && m.LaisvasKiekis == lk3);
            tech.FormosId = null;
            tech.LaisvasKiekis = null;
            if (ModelState.IsValid)
            {
              _context.Update(tech);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(DetailsLaisvaTechnika), new { id = fid4 });
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuKodas", tech.FormosId);
            return RedirectToAction(nameof(DetailsLaisvaTechnika), new { id = fid4 });
          }
        }
      }
      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technologas") || User.IsInRole("Gamybos vadovas") || User.IsInRole("Gamybos meistras"))
        {
          if (fid4 == 1)
          {
            var ingr = await _context.Ingredientas.SingleOrDefaultAsync(m => m.IngredientoId == tpd3 && m.FormosId == fid4 && m.LaisvasKiekis == lk3);
            ingr.FormosId = null;
            ingr.LaisvasKiekis= null;
            if (ModelState.IsValid)
            {
              _context.Update(ingr);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(DetailsLaisvosZaliavos), new { id = fid4 });
            }
            ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuKodas", ingr.FormosId);
            return RedirectToAction(nameof(DetailsLaisvosZaliavos), new { id = fid4 });
          }
        }
      }
      return NotFound();
    }
    // GET: LaisviResursais/Create
    /* public IActionResult Create()
     {
         return View();
     }
     */
    // POST: LaisviResursais/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    public IActionResult Redirect(decimal? id)
    {
      string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new { id = id });
      return Redirect(url);
    }
       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormosId,LaisvuResursuKodas")] LaisviResursai laisviResursai, decimal kiLR, decimal kuiLR, string kubLR)
        {


      if (ModelState.IsValid)
      {
        _context.Add(laisviResursai);
        await _context.SaveChangesAsync();
      }
        var kua = await _context.KlientoUzsakymoAplankas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kuiLR
        && m.KlientoId == kiLR && m.Busena == kubLR);
        kua.FormosId = laisviResursai.FormosId;
        if (ModelState.IsValid)
        {
          _context.Update(kua);
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Redirect), new { id = kuiLR });
        }
      
      return RedirectToAction(nameof(Redirect), new { id = kuiLR });
    }
    */
        // GET: LaisviResursais/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laisviResursai = await _context.LaisviResursai.SingleOrDefaultAsync(m => m.FormosId == id);
            if (laisviResursai == null)
            {
                return NotFound();
            }
            return View(laisviResursai);
        }

        // POST: LaisviResursais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("FormosId,LaisvuResursuKodas")] LaisviResursai laisviResursai)
        {
            if (id != laisviResursai.FormosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laisviResursai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaisviResursaiExists(laisviResursai.FormosId))
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
            return View(laisviResursai);
        }

        // GET: LaisviResursais/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laisviResursai = await _context.LaisviResursai
                .SingleOrDefaultAsync(m => m.FormosId == id);
            if (laisviResursai == null)
            {
                return NotFound();
            }

            return View(laisviResursai);
        }

        // POST: LaisviResursais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var laisviResursai = await _context.LaisviResursai.SingleOrDefaultAsync(m => m.FormosId == id);
            _context.LaisviResursai.Remove(laisviResursai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaisviResursaiExists(decimal id)
        {
            return _context.LaisviResursai.Any(e => e.FormosId == id);
        }
    }
}
