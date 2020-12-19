using System;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;

namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
    public class IngredientoReceptasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public IngredientoReceptasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: IngredientoReceptas
        public async Task<IActionResult> Index(decimal? id, decimal? id2, string a11, DateTime a22, string brandl, string a44, string a55)
        {
              if (id == null || id2 == null)
              {
                return NotFound();
              }
      var alaus_daryklos_gamybos_valdymas_DBContext = _context.IngredientoReceptas.Include(i => i.GamybosInstrukcija).
        ThenInclude(i => i.KlientoUzsakymo).
      Where(i => i.InstrukcijosId == id2).
      Where(i => i.KlientoUzsakymoId == id).
     Include(i => i.Ingrediento);
      ViewData["KlientoUzsakymoId"] = id;
      ViewData["InstrukcijosId"] = id2;
      ViewData["InstrukcijosBusena"] = a11;
      ViewData["InstrukcijosTerminas"] = a22;
      ViewData["InstrukcijosBrandinimoLaikas"] = brandl;
      ViewData["RecepturaParuosta"] = a44;
      ViewData["TechnikaParuosta"] = a55;
      ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas");
      return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
        }
        // komentass
    // GET: IngredientoReceptas/Details/5
    public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientoReceptas = await _context.IngredientoReceptas
                .Include(i => i.GamybosInstrukcija)
                .Include(i => i.Ingrediento)
                .SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (ingredientoReceptas == null)
            {
                return NotFound();
            }

            return View(ingredientoReceptas);
        }

    // GET: IngredientoReceptas/Create
    public IActionResult Create(decimal? id, decimal? id2, decimal? ingraddalert, string a1, DateTime a2, string a3, string a4, string a5, decimal? alertshow)
        {

      if (id == null || id2 == null)
      {
        return NotFound();
      }
      var ingredientoreceptas = _context.IngredientoReceptas.Include(m => m.Ingrediento).Include(m => m.KlientoUzsakymoId == id).Include(m => m.InstrukcijosId == id2);
        ViewData["KlientoUzsakymoId"] = id;
      ViewData["InstrukcijosId"] = id2;
      ViewData["AlertMsgdecimal"] = alertshow;
      ViewData["InstrukcijosBusena"] = a1;
      ViewData["InstrukcijosTerminas"] = a2;
      ViewData["InstrukcijosBrandinimoLaikas"] = a3;
      ViewData["RecepturaParuosta"] = a4;
      ViewData["TechnikaParuosta"] = a5;
      ViewData["Ingraddalertas"] = ingraddalert;
      ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas");
            return View();
        }

        // POST: IngredientoReceptas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal kui, decimal Ini, string a3, [Bind("KlientoUzsakymoId,InstrukcijosId,IngredientoId,IngredientoKiekis,VirimoLaikas,FermentacijosLaikas")] IngredientoReceptas ingredientoReceptas,
          string IB2, string RP2, string TP2, DateTime GT2)
        {

      decimal alertshow = 0;
     ingredientoReceptas.KlientoUzsakymoId = kui;
      ingredientoReceptas.InstrukcijosId = Ini;
      var alaus_daryklos_gamybos_valdymas_DBContext2 = _context.IngredientoReceptas.Include(i => i.GamybosInstrukcija).
       ThenInclude(i => i.KlientoUzsakymo).
     Where(i => i.InstrukcijosId == Ini).
     Where(i => i.KlientoUzsakymoId == kui).
    Include(i => i.Ingrediento);
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext2) {
        if (ingredientoReceptas.IngredientoId == item.IngredientoId) {
          alertshow = 2;
          return RedirectToAction(nameof(Create), new { id = kui, id2 = Ini, alertshow = alertshow, a1 = IB2, a2 = GT2, a3=a3, a4 = RP2, a5=TP2});
        }
      }
      if (ModelState.IsValid)
            {
                _context.Add(ingredientoReceptas);
                await _context.SaveChangesAsync();
        alertshow ++;
        return RedirectToAction(nameof(Create), new { id = kui, id2 = Ini, alertshow = alertshow, a1 = IB2, a2 = GT2, a3 = a3, a4 = RP2, a5 = TP2});
            }
      ViewData["KlientoUzsakymoId"] = kui;
      ViewData["InstrukcijosId"] = Ini;
      ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas");
      return View(ingredientoReceptas);
        }
    public IActionResult RecepturaDone(decimal id, decimal id2, string a1, DateTime a2, string a3, string a4, string a5)
    {
      string url = @Url.Action("GamybosInstrukcijaUzpildyta", "KlientoUzsakymoAplankas", new { id = id, id2 = id2, a1 = a1, a2 = a2, a3 = a3, a4 = a4, a5 = a5});
      return Redirect(url);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateIngr([Bind("IngredientoId,FormosId,IngredientoPavadinimas,LaisvasKiekis")] Ingredientas ingredientas, string Iid, decimal kui2,decimal Ini2)
    {
      ingredientas.IngredientoPavadinimas = Iid;
      if (ModelState.IsValid)
      {
        _context.Add(ingredientas);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Create), new { id = kui2, id2 = Ini2 });
      }
      return RedirectToAction(nameof(Create), new { id = kui2, id2 = Ini2 });
    }

    // GET: IngredientoReceptas/Edit/5
    /*public async Task<IActionResult> Edit(decimal? id, decimal? id2, decimal? id3, float ik, string vl, string fl)
        {
            if (id == null || id2 == null || id3 == null)
            {
                return NotFound();
            }

            var ingredientoReceptas = await _context.IngredientoReceptas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id && m.InstrukcijosId == id2
            && m.IngredientoId == id3 && m.IngredientoKiekis == ik
            );
            if (ingredientoReceptas == null)
            {
                return NotFound();
            }
      ViewData["KlientoUzsakymoIdEdit"] = id;
            ViewData["IngredientoIdEdit"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas", ingredientoReceptas.IngredientoId);
      ViewData["InstrukcijosIdEdit"] = id2;
      ViewData["IngredientoKiekisEdit"] = ik;
      ViewData["IngredientoVirimoLaikasEdit"] = vl;
      ViewData["IngredientoFermentacijosLaikasEdit"] =fl;

      return View("index");
        }

        // POST: IngredientoReceptas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        */[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal kui, decimal ii1, decimal ini, float ik, string vl, string fl)
        {
      var ingredientoReceptas = await _context.IngredientoReceptas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui && m.InstrukcijosId == ini
            && m.IngredientoId == ii1);
      ingredientoReceptas.IngredientoKiekis = ik;
      ingredientoReceptas.VirimoLaikas = vl;
      ingredientoReceptas.FermentacijosLaikas = fl;

      if (ModelState.IsValid)
      {
        _context.Update(ingredientoReceptas);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = kui, id2 = ini });
      }
      ViewData["KlientoUzsakymoIdEdit"] = kui;
            ViewData["IngredientoIdEdit"] = ii1;
            ViewData["InstrukcijosIdEdit"] = ini;
            ViewData["IngredientoKiekisEdit"] = ik;
            ViewData["IngredientoVirimoLaikasEdit"] = vl;
            ViewData["IngredientoFermentacijosLaikasEdit"] = fl;

      return RedirectToAction(nameof(Index), new { id = kui, id2 = ini});
    }
        public async Task<IActionResult> DeleteConfirmed(decimal id, decimal id2, decimal id3, float a1, string a2, string a3)
        {
            var ingredientoReceptas = await _context.IngredientoReceptas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id && m.InstrukcijosId == id2
            && m.IngredientoId == id3 && m.IngredientoKiekis == a1 && m.VirimoLaikas == a2 && m.FermentacijosLaikas == a3);
      _context.IngredientoReceptas.Remove(ingredientoReceptas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = id, id2 = id2});
        }

        private bool IngredientoReceptasExists(decimal id)
        {
            return _context.IngredientoReceptas.Any(e => e.KlientoUzsakymoId == id);
        }
    }
}
