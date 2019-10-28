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
  public class ResursaiUzsakymuiController : Controller
  {
    private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;



    public ResursaiUzsakymuiController(Alaus_daryklos_gamybos_valdymas_DBContext context)
    {
      _context = context;
    }


    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(decimal kuiLR2, decimal fid, [Bind("KlientoUzsakymoId,FormosId,LaisvuResursuTipas")]
    ResursaiUzsakymui resursoUzsakymai)
    {
      decimal lasiviresursaialert = 0;
      resursoUzsakymai.KlientoUzsakymoId = kuiLR2;
      resursoUzsakymai.FormosId = fid;

      var alaus_daryklos_gamybos_valdymas_DBContext = _context.LaisviResursaiUzsakymui.Include(i => i.ResursaiLaisvi).Include(i => i.UzsakymasKliento).
   Where(i => i.KlientoUzsakymoId == kuiLR2);
      foreach (var item in alaus_daryklos_gamybos_valdymas_DBContext)
      {
        if (resursoUzsakymai.FormosId == item.FormosId)
        {
          lasiviresursaialert = 2;
          return RedirectToAction(nameof(Redirect), new { id = kuiLR2, lasiviresursaialert = lasiviresursaialert });
        }
      }
      if (fid == 2)
      {
        resursoUzsakymai.LaisvuResursuTipas = "T";
      }
      else
      {
        resursoUzsakymai.LaisvuResursuTipas = "Ž";
      }
      if (ModelState.IsValid)
      {
        _context.Add(resursoUzsakymai);
        await _context.SaveChangesAsync();
        lasiviresursaialert++;
        return RedirectToAction(nameof(Redirect), new { id = kuiLR2, lasiviresursaialert = lasiviresursaialert });
      }
      return RedirectToAction(nameof(Redirect), new { id = kuiLR2, lasiviresursaialert = lasiviresursaialert });
    }
    public IActionResult Redirect(decimal? id, decimal? lasiviresursaialert)
    {
      string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new { id = id, lasiviresursaialert = lasiviresursaialert });
      return Redirect(url);
    }
  }
}
