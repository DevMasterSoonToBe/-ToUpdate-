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
    public class TechnikosPrietaisoPatarimasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public TechnikosPrietaisoPatarimasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

    // GET: TechnikosPrietaisoPatarimas
    public async Task<IActionResult> Index(decimal? id, decimal? id2, string a11, DateTime a22, string brandl, string a44, string a55)
    {
      if (id == null || id2 == null)
      {
        return NotFound();
      }
      var alaus_daryklos_gamybos_valdymas_DBContext = _context.TechnikosPrietaisoPatarimas.Include(i => i.GamybosInstrukcija).
        ThenInclude(i => i.KlientoUzsakymo).
      Where(i => i.InstrukcijosId == id2).
      Where(i => i.KlientoUzsakymoId == id).
     Include(i => i.TechnikosPrietaiso);
      ViewData["KlientoUzsakymoId"] = id;
      ViewData["InstrukcijosId"] = id2;
      ViewData["InstrukcijosBusena"] = a11;
      ViewData["InstrukcijosTerminas"] = a22;
      ViewData["InstrukcijosBrandinimoLaikas"] = brandl;
      ViewData["RecepturaParuosta"] = a44;
      ViewData["TechnikaParuosta"] = a55;
      return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
    }

    // GET: TechnikosPrietaisoPatarimas/Details/5
    public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technikosPrietaisoPatarimas = await _context.TechnikosPrietaisoPatarimas
                .Include(t => t.GamybosInstrukcija)
                .Include(t => t.TechnikosPrietaiso)
                .SingleOrDefaultAsync(m => m.TechnikosPrietaisoId == id);
            if (technikosPrietaisoPatarimas == null)
            {
                return NotFound();
            }

            return View(technikosPrietaisoPatarimas);
        }

    // GET: TechnikosPrietaisoPatarimas/Create
    public IActionResult Create(decimal? id, decimal? id2, string a1, DateTime a2, string a3, string a4, string a5, decimal? alertshow, decimal? Techaddalert)
    {

      if (id == null || id2 == null)
      {
        return NotFound();
      }
      var technikosprietaisopatarimas = _context.TechnikosPrietaisoPatarimas.Include(m => m.KlientoUzsakymoId == id).Include(m => m.InstrukcijosId == id2).Include(m => m.TechnikosPrietaiso);
      ViewData["KlientoUzsakymoId"] = id;
      ViewData["InstrukcijosId"] = id2;
      ViewData["InstrukcijosBusena"] = a1;
      ViewData["InstrukcijosTerminas"] = a2;
      ViewData["InstrukcijosBrandinimoLaikas"] = a3;
      ViewData["RecepturaParuosta"] = a4;
      ViewData["TechnikaParuosta"] = a5;
      ViewData["AlertMsgdecimal"] = alertshow;
      ViewData["Techaddalert"] = Techaddalert;
      ViewData["TechnikosPrietaisoId"] = new SelectList(_context.TechnikosPrietaisas, "TechnikosPrietaisoId", "PrietaisoPavadinimas");
      return View();
    }

    // POST: TechnikosPrietaisoPatarimas/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal kui, decimal Ini, string a3,
      [Bind("TechnikosPrietaisoId,KlientoUzsakymoId,InstrukcijosId,PrietaisoKomentaras,PrietaisoKiekis")] TechnikosPrietaisoPatarimas technikosPrietaisoPatarimas,
      string IB, string RP, string TP, DateTime GT)
        {
      decimal alertshow = 0;
      technikosPrietaisoPatarimas.KlientoUzsakymoId = kui;
      technikosPrietaisoPatarimas.InstrukcijosId = Ini;
      var alaus_daryklos_gamybos_valdymas_DBContext2 = _context.TechnikosPrietaisoPatarimas.Include(i => i.GamybosInstrukcija).
      ThenInclude(i => i.KlientoUzsakymo).
    Where(i => i.InstrukcijosId == Ini).
    Where(i => i.KlientoUzsakymoId == kui).
   Include(i => i.TechnikosPrietaiso);
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext2)
      {
        if (technikosPrietaisoPatarimas.TechnikosPrietaisoId == item.TechnikosPrietaisoId)
        {
          alertshow = 2;
          return RedirectToAction(nameof(Create), new { id = kui, id2 = Ini, alertshow = alertshow, a1 = IB, a2 = GT, a3 = a3, a4 = RP, a5 = TP });
        }
      }
      if (ModelState.IsValid)
      {
        _context.Add(technikosPrietaisoPatarimas);
        await _context.SaveChangesAsync();
        alertshow++;
        return RedirectToAction(nameof(Create), new { id = kui, id2 = Ini, alertshow = alertshow, a1 = IB, a2 = GT, a3 = a3, a4 = RP, a5 = TP });
      }
      ViewData["KlientoUzsakymoId"] = kui;
      ViewData["InstrukcijosId"] = Ini;
      ViewData["TechnikosPrietaisoId"] = new SelectList(_context.TechnikosPrietaisas, "TechnikosPrietaisoId", "PrietaisoPavadinimas");
      return View(technikosPrietaisoPatarimas);
    }
    public IActionResult TechnikaDone(decimal? id, decimal id2, string a1, DateTime a2, string a3, string a4, string a5)
    {
      string url = @Url.Action("GamybosInstrukcijaUzpildyta", "KlientoUzsakymoAplankas", new { id = id, id2 = id2, a1 = a1, a2 = a2, a3 = a3, a4 = a4, a5 = a5});
      return Redirect(url);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTechPriet([Bind("TechnikosPrietaisoId,FormosId,PrietaisoPavadinimas,LaisvasKiekis")] TechnikosPrietaisas technikosPrietaisas, decimal kui2, decimal Ini2,string techp,string a33,
      string IB3, string RP3, string TP3, DateTime GT3,
       [Bind("TechnikosPrietaisoId,KlientoUzsakymoId,InstrukcijosId,PrietaisoKomentaras,PrietaisoKiekis")] TechnikosPrietaisoPatarimas technikosPrietaisoPatarimas)
    {
      technikosPrietaisoPatarimas.InstrukcijosId = Ini2;
      technikosPrietaisas.PrietaisoPavadinimas = techp;
      technikosPrietaisoPatarimas.KlientoUzsakymoId = kui2;
      decimal Techaddalert = 0;
      if (ModelState.IsValid)
      {
        _context.Add(technikosPrietaisas);
        await _context.SaveChangesAsync();
        Techaddalert++;
        return RedirectToAction(nameof(Create), new { id = kui2, id2 = Ini2, Techaddalert = Techaddalert, a2 = GT3, a3 = a33, a4 = RP3, a5 = TP3 });
      }
      ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "LaisvuResursuTipas", technikosPrietaisas.FormosId);
      return RedirectToAction(nameof(Create), new { id = kui2, id2 = Ini2, a1 = IB3, a2 = GT3, a3 = a33, a4 = RP3, a5 = TP3 });
    }

    // GET: TechnikosPrietaisoPatarimas/Edit/5
    public async Task<IActionResult> Edit(decimal? id, decimal? id2, decimal? id3)
    {
      if (id == null || id2 == null || id3 == null)
      {
        return NotFound();
      }

      var technikosPrietaisoPatarimas = await _context.TechnikosPrietaisoPatarimas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id && m.InstrukcijosId == id2
      && m.TechnikosPrietaisoId == id3
      );
      if (technikosPrietaisoPatarimas == null)
      {
        return NotFound();
      }
      ViewData["KlientoUzsakymoId"] = id;
      ViewData["TechnikosPrietaisoId"] = new SelectList(_context.TechnikosPrietaisas, "TechnikosPrietaisoId", "PrietaisoPavadinimas", technikosPrietaisoPatarimas.TechnikosPrietaisoId);
      ViewData["InstrukcijosId"] = id2;
      return View(technikosPrietaisoPatarimas);
    }
    // POST: TechnikosPrietaisoPatarimas/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal kui, decimal ti1, decimal ini, int tk, string tko)
        {
      var technikospatarimas = await _context.TechnikosPrietaisoPatarimas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui && m.InstrukcijosId == ini
           && m.TechnikosPrietaisoId == ti1);
      technikospatarimas.PrietaisoKiekis = tk;
      technikospatarimas.PrietaisoKomentaras = tko;

      if (ModelState.IsValid)
      {
        _context.Update(technikospatarimas);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kui, id2 = ini });
      }
      ViewData["KlientoUzsakymoIdEdit"] = kui;
      ViewData["IngredientoIdEdit"] = ti1;
      ViewData["PrietaisoIdEdit"] = ini;
      ViewData["PrietaisoKiekisEdit"] = tk;
      ViewData["PrietaisoKomentarasEdit"] = tko;
      return RedirectToAction(nameof(Index), new { id = kui, id2 = ini });
    }
    public async Task<IActionResult> DeleteConfirmed(decimal id, decimal id3, decimal id2, int a1, string a2)
    {
      var technikosPrietaisoPatarimas = await _context.TechnikosPrietaisoPatarimas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id && m.InstrukcijosId == id2
      && m.TechnikosPrietaisoId == id3 && m.PrietaisoKiekis == a1 && m.PrietaisoKomentaras == a2);
      _context.TechnikosPrietaisoPatarimas.Remove(technikosPrietaisoPatarimas);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index), new { id = id, id2 = id2 });
    }
    private bool TechnikosPrietaisoPatarimasExists(decimal id)
        {
            return _context.TechnikosPrietaisoPatarimas.Any(e => e.TechnikosPrietaisoId == id);
        }
    }
}
