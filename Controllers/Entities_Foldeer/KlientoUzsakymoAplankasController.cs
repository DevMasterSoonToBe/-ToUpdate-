using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;
namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
  public class KlientoUzsakymoAplankasController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;



    public KlientoUzsakymoAplankasController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: KlientoUzsakymoAplankas
        
        public IActionResult Index(int? puslapis)
        {
      if (User.Identity.IsAuthenticated)
      {
        var alaus_daryklos_gamybos_valdymas_DBContextPage = _context.KlientoUzsakymoAplankas.Include(k => k.Kliento).Include(k => k.GamybosInstrukcija)
          .Include(k => k.Kliento).Include(k => k.IngredientoPrasymas).Include(k => k.TechnikosPrietaisoPrasymas).
          OrderBy(b => b.Terminas).ToList().ToPagedList(puslapis ?? 1, 14);
        ViewData["KuiVisi"] = _context.KlientoUzsakymoAplankas.Count();
        ViewData["KuiVykdomi"] = _context.KlientoUzsakymoAplankas.Where(k => k.Busena != "A").Count();
        ViewData["KuiArchyvuoti"] = _context.KlientoUzsakymoAplankas.Where(k => k.Busena == "A").Count();
          ViewData["kuiatsaukti"] = _context.KlientoUzsakymoAplankas.Where(k => k.Busena == "A").
            Join(_context.GamybosInstrukcija.Where(x => x.Busena != "B"),
         k => k.KlientoUzsakymoId,
         e => e.KlientoUzsakymoId,
         (k, e) => k).Count();
       
        ViewData["kuipaveluoti"] = _context.KlientoUzsakymoAplankas.Where(k => k.Sutartis.SutartiesTerminas < k.ArchyvavimoLaikas).Count();






        List<string> Ingredientai = new List<string>();
        var ingrTop3 = _context.Ingredientas.Join(_context.IngredientoReceptas,
         k => k.IngredientoId,
         e => e.IngredientoId,
         (k, e) => k).GroupBy(k => k.IngredientoPavadinimas).OrderByDescending(k => k.Count()).Take(3).ToList();


        foreach (var item in ingrTop3)
        {
          foreach (var item2 in item)
          {
            if (!Ingredientai.Contains(item2.IngredientoPavadinimas))
            {
              Ingredientai.Add(item2.IngredientoPavadinimas);
            }
          }
        }
          var ingrtop3 = Ingredientai.ToArray();

          ViewData["ingredientaitop3"] = ingrtop3;




        List<string> Tech = new List<string>();
        var TechTop3 = _context.TechnikosPrietaisas.Join(_context.TechnikosPrietaisoPatarimas,
         k => k.TechnikosPrietaisoId,
         e => e.TechnikosPrietaisoId,
         (k, e) => k).GroupBy(k => k.PrietaisoPavadinimas).OrderByDescending(k => k.Count()).Take(3).ToList();


        foreach (var item in TechTop3)
        {
          foreach (var item2 in item)
          {
            if (!Tech.Contains(item2.PrietaisoPavadinimas))
            {
              Tech.Add(item2.PrietaisoPavadinimas);
            }
          }
        }
        var Techtop3 = Tech.ToArray();

        ViewData["techtop3"] = Techtop3;

        



        return View(alaus_daryklos_gamybos_valdymas_DBContextPage); 
      }
       return Redirect(@Url.Page("/Account/Login", new { area = "Identity" }));
    }

        public IActionResult Redirect()
        {
            string url = @Url.Action("ArchyvuotiUzsakymai", "KlientoUzsakymoAplankas", null);
                return Redirect(url);
        }
    public async Task<IActionResult> GamybosInstrukcijaUzpildyta(decimal id, decimal id2, char a1, DateTime a2, string a3, string  a4, string a5)
    {


      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technologas"))
        {
          var gamybosinstrukcija = await _context.GamybosInstrukcija.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id && m.InstrukcijosId == id2
          && m.GamybosTerminas == a2 && m.BrandinimoLaikas == a3);
          gamybosinstrukcija.RecepturaParuosta = "T";
          if (ModelState.IsValid)
          {

            _context.Update(gamybosinstrukcija);
            await _context.SaveChangesAsync();
          }
        }
      }
      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technikos vadovas"))
        {
          var gamybosinstrukcija = await _context.GamybosInstrukcija.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id
          && m.InstrukcijosId == id2 && m.GamybosTerminas == a2 && m.BrandinimoLaikas == a3);
          gamybosinstrukcija.TechnikosPatarimaiParuosti = "T";
          if (ModelState.IsValid)
          {

            _context.Update(gamybosinstrukcija);
            await _context.SaveChangesAsync();
          }
        }
      }
      var gamybosinstrukcijaa = await _context.GamybosInstrukcija.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id
      && m.InstrukcijosId == id2 && m.BrandinimoLaikas == a3);
      if (gamybosinstrukcijaa.RecepturaParuosta == "T")
      {
        if (gamybosinstrukcijaa.TechnikosPatarimaiParuosti == "T")
        {
          gamybosinstrukcijaa.Busena = "U";
          if (ModelState.IsValid)
          {

            _context.Update(gamybosinstrukcijaa);
            await _context.SaveChangesAsync();
          }

        }
      }
          return RedirectToAction(nameof(Details), new { id = id });
        }
    // Edit FormosId
    
    // GET: KlientoUzsakymoAplankas/Details/5
    public async Task<IActionResult> Details(decimal? id, decimal? alertshowPrasymas, decimal? alertshowPrasymas2, decimal? ingrreservealert,
      decimal? techreservealert, decimal? techformaddalert, decimal? ingrformaddalert,  decimal? archyvUAAlert,
      decimal? SutartisIkeltaAlert, decimal? brandlaik, decimal? IngrPrasAlert, decimal? TechPrasAlert, decimal? ChangeStateGIalert, decimal? GIalert,
      decimal? lasiviresursaialert)
        {
   
      if (User.Identity.IsAuthenticated)
          {
        if (User.IsInRole("Technologas") || User.IsInRole("Gamybos vadovas") || User.IsInRole("Gamybos meistras"))
        {
          ViewData["IngredientoId"] = new SelectList(_context.Ingredientas, "IngredientoId", "IngredientoPavadinimas");
        }
      }
      if (User.Identity.IsAuthenticated)
      {
        if (User.IsInRole("Technikos vadovas") || User.IsInRole("Gamybos vadovas") || User.IsInRole("Gamybos meistras"))
        {
          ViewData["TechnikosPrietaisoId"] = new SelectList(_context.TechnikosPrietaisas, "TechnikosPrietaisoId", "PrietaisoPavadinimas");
        }
      }
      ViewData["FormosId"] = new SelectList(_context.LaisviResursai, "FormosId", "ResursoPavadinimas");
      ViewData["IsFormaZ"] = _context.LaisviResursaiUzsakymui.Where(k => k.LaisvuResursuTipas == "Ž").Count();
      ViewData["IsFormaT"] = _context.LaisviResursaiUzsakymui.Where(k => k.LaisvuResursuTipas == "T").Count();

      ViewData["AlertIngrPrasymas"] = alertshowPrasymas;
      ViewData["AlertTechPrasymas"] = alertshowPrasymas2;

      ViewData["AlertIngrPrasymasBusena"] = IngrPrasAlert;
      ViewData["AlertTechPrasymasBusena"] = TechPrasAlert;

      ViewData["IngrFormaAdd"] = ingrformaddalert;
      ViewData["TechFormaAdd"] = techformaddalert;

      ViewData["AlertIngrReserve"] = ingrreservealert;
      ViewData["AlertTechReserve"] = techreservealert;

      ViewData["ArchyvuotasAplankas"] = archyvUAAlert;

      ViewData["brandlaikalert"] = brandlaik;

      ViewData["GIBusena"] = ChangeStateGIalert;

      ViewData["GIAlertas"] = GIalert;

      ViewData["SutartisAlert"] = SutartisIkeltaAlert;

      ViewData["IngredientaiRezervacijoms"] = new SelectList(_context.Ingredientas.Where(k => k.LaisvasKiekis != null).Join(_context.IngredientoReceptas.Where(x => x.KlientoUzsakymoId == id),
        k => k.IngredientoId,
        e => e.IngredientoId,
        (k, e) => k), "IngredientoId", "IngredientoPavadinimas");
      ViewData["PrietaisaiRezervacijoms"] = new SelectList(_context.TechnikosPrietaisas.Where(k => k.LaisvasKiekis != null).Join(_context.TechnikosPrietaisoPatarimas.Where(x => x.KlientoUzsakymoId == id),
        k => k.TechnikosPrietaisoId,
        e => e.TechnikosPrietaisoId,
        (k, e) => k), "TechnikosPrietaisoId", "PrietaisoPavadinimas");

      ViewData["IsIngrPrasymai"] = _context.IngredientoPrasymas.Where(k => k.KlientoUzsakymoId == id).Count();

      ViewData["IsTechPrasymai"] = _context.TechnikosPrietaisoPrasymas.Where(k => k.KlientoUzsakymoId == id).Count();

      ViewData["ArAtliktiTechPrasymai"] = _context.TechnikosPrietaisoPrasymas.Where(k => k.KlientoUzsakymoId == id).Where(k => k.Busena == "A").Count();
      ViewData["ArAtliktiIngrPrasymai"] = _context.IngredientoPrasymas.Where(k => k.KlientoUzsakymoId == id).Where(k => k.Busena == "A").Count();

      ViewData["Laisivresursaialert"] = lasiviresursaialert;
      ViewData["IngredientaiPrasymui"] = new SelectList(_context.Ingredientas.Join(_context.IngredientoReceptas.Where(x => x.KlientoUzsakymoId == id),
        k => k.IngredientoId,
        e => e.IngredientoId,
        (k,e) => k), "IngredientoId", "IngredientoPavadinimas");
      ViewData["TechnikaPrasymui"] = new SelectList(_context.TechnikosPrietaisas.Join(_context.TechnikosPrietaisoPatarimas.Where(x => x.KlientoUzsakymoId == id),
        k => k.TechnikosPrietaisoId,
        e => e.TechnikosPrietaisoId,
        (k, e) => k), "TechnikosPrietaisoId", "PrietaisoPavadinimas");


      if (id == null)
      {
        return NotFound();
      }
      var klientoUzsakymoAplankas = await _context.KlientoUzsakymoAplankas
                .Include(k => k.Kliento)
                .Include(k => k.GamybosInstrukcija).ThenInclude(k => k.TechnikosPrietaisoPatarimas).ThenInclude(k => k.TechnikosPrietaiso)
                .Include(k => k.GamybosInstrukcija).ThenInclude(k => k.IngredientoReceptas).ThenInclude(k => k.Ingrediento)
                .Include(k => k.IngredientoPrasymas).ThenInclude(k => k.Ingrediento)
                .Include(k => k.IngredientoRezervacija).ThenInclude(k => k.Ingrediento)
                .Include(k => k.Sutartis)
                .Include(k => k.LaisviResursaiUzsakymui).ThenInclude(k => k.ResursaiLaisvi)
                .Include(k => k.TechnikosPrietaisoPrasymas).ThenInclude(k =>k.TechnikosPrietaiso)
                .Include(k => k.TechnikosPrietaisoRezervacija).ThenInclude(k => k.TechnikosPrietaiso)
                .SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);

      
      if (klientoUzsakymoAplankas == null)
            {
                return NotFound();
            }

            return View(klientoUzsakymoAplankas);
    }

        // GET: KlientoUzsakymoAplankas/Create
        public IActionResult Create(decimal? alertkuiadd, decimal? kiaddalert)
            
        {
      ViewData["kiadd"] = kiaddalert;
      ViewData["kuiadd"] = alertkuiadd;
      ViewData["KlientoID"] = new SelectList(_context.Klientas, "KlientoId", "KlientoPavadinimas");
            //Console.WriteLine(ViewData["KlientoID"]);
            return View();
        }

        // POST: KlientoUzsakymoAplankas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlientoUzsakymoId,KlientoId,Terminas,Busena")] KlientoUzsakymoAplankas klientoUzsakymoAplankas)
        {

      decimal alertkuiadd = 0;
            if (ModelState.IsValid)
            {
                _context.Add(klientoUzsakymoAplankas);
                await _context.SaveChangesAsync();
        alertkuiadd++;
                return RedirectToAction(nameof(Create), new { alertkuiadd = alertkuiadd});
            }
            ViewData["KlientoId"] = new SelectList(_context.Klientas, "KlientoId", "KlientoPavadinimas", klientoUzsakymoAplankas.KlientoId);
           // Console.WriteLine(ViewData["KlientoID"]);
       
            return View(klientoUzsakymoAplankas);
        }

        // GET: KlientoUzsakymoAplankas/Edit/5
       /* public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klientoUzsakymoAplankas = await _context.KlientoUzsakymoAplankas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (klientoUzsakymoAplankas == null)
            {
                return NotFound();
            }
        
            ViewData["KlientoId"] = new SelectList(_context.Klientas, "KlientoId", "KlientoPavadinimas", klientoUzsakymoAplankas.KlientoId);
            return View(klientoUzsakymoAplankas);
        }
        */
        // POST: KlientoUzsakymoAplankas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal kuiESUA, decimal kiESUA,string UAB, string UAK)
        {
      decimal archyvUAAlert = 0;
      var kUAEdit = await _context.KlientoUzsakymoAplankas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kuiESUA && m.KlientoId == kiESUA);
      kUAEdit.Busena = UAB;
      kUAEdit.Archyvavimo_Komentaras = UAK;
      kUAEdit.ArchyvavimoLaikas = DateTime.UtcNow;
      if (ModelState.IsValid)
      {
        _context.Update(kUAEdit);
        await _context.SaveChangesAsync();
        archyvUAAlert++;
        return RedirectToAction(nameof(Details), new { id = kuiESUA, archyvUAAlert = archyvUAAlert });
      }
      return RedirectToAction(nameof(Details), new { id = kuiESUA, archyvUAAlert = archyvUAAlert });
    }


    public async Task<IActionResult> PrasymasZaliavomsNereikalingas(decimal kui, decimal ki, string ZaliavuPrasymasNereikalingas)
    {

      var kUAEdit = await _context.KlientoUzsakymoAplankas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui && m.KlientoId == ki);
      kUAEdit.PrasymasZaliavomsReikalingas = ZaliavuPrasymasNereikalingas;
      if (ModelState.IsValid)
      {
        _context.Update(kUAEdit);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id = kui });
      }
      return RedirectToAction(nameof(Details), new { id = kui});
    }

    public async Task<IActionResult> PrasymasTechnikaiNereikalingas(decimal kui, decimal ki, string TechnikosPrasymasNereikalingas)
    {

      var kUAEdit = await _context.KlientoUzsakymoAplankas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kui && m.KlientoId == ki);
      kUAEdit.PrasymasZaliavomsReikalingas = TechnikosPrasymasNereikalingas;
      if (ModelState.IsValid)
      {
        _context.Update(kUAEdit);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id = kui });
      }
      return RedirectToAction(nameof(Details), new { id = kui });
    }


    // GET: KlientoUzsakymoAplankas/Delete/5
    public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klientoUzsakymoAplankas = await _context.KlientoUzsakymoAplankas
                .Include(k => k.Kliento)
                .SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            if (klientoUzsakymoAplankas == null)
            {
                return NotFound();
            }

            return View(klientoUzsakymoAplankas);
        }

        // POST: KlientoUzsakymoAplankas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var klientoUzsakymoAplankas = await _context.KlientoUzsakymoAplankas.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == id);
            _context.KlientoUzsakymoAplankas.Remove(klientoUzsakymoAplankas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlientoUzsakymoAplankasExists(decimal id)
        {
            return _context.KlientoUzsakymoAplankas.Any(e => e.KlientoUzsakymoId == id);
        }
    }
}
