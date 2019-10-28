using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System;

namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
    public class SutartisController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        IHostingEnvironment _hostingEnvironment;


        public SutartisController(Alaus_daryklos_gamybos_valdymas_DBContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Sutartis
        public async Task<IActionResult> Index()
        {
            var alaus_daryklos_gamybos_valdymas_DBContext = _context.Sutartis.Include(s => s.KlientoUzsakymo).Include(s => s.Tiekejo);
            return View(await alaus_daryklos_gamybos_valdymas_DBContext.ToListAsync());
        }

        // GET: Sutartis/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sutartis = await _context.Sutartis
                .Include(s => s.Kliento)
                .Include(s => s.KlientoUzsakymo)
                .Include(s => s.Tiekejo)
                .SingleOrDefaultAsync(m => m.SutartiesId == id);
            if (sutartis == null)
            {
                return NotFound();
            }

            return View(sutartis);
        }

        // GET: Sutartis/Create
        public IActionResult Create(decimal? id, decimal? id2, decimal? SutartisIkeltaAlert, decimal? tiekejasdbalert)
        {
           
            var sutartis = _context.Sutartis.Include(m => m.KlientoUzsakymoId == id).Include(m => m.TiekejoId == id2);
            ViewData["KlientoUzsakymoId"] = id;
      ViewData["TiekejoAlert"] = SutartisIkeltaAlert;
      ViewData["TiekejoDBAlert"] = tiekejasdbalert;
      ViewData["TiekejoId"] = new SelectList(_context.Tiekejas, "TiekejoId", "TiekejoPavadinimas");
            return View();
        }

        // POST: Sutartis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal? kui, DateTime term, IFormFile files,
          [Bind("SutartiesId,KlientoUzsakymoId,TiekejoId,KlientoId,SutartiesPasirasymoData,SutartiesTerminas,SutartisFailas")] Sutartis Sutartis)
        {
      /*foreach (var file in files) {
          var fileName = ContentDispositionHeaderValue.
              Parse(file.ContentDisposition).FileName.
              Trim('"');

          if (fileName.EndsWith(".docx"))// Important for security if saving in webroot
          {

              var filePath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\" + fileName;
              byte[] byteArray = Encoding.UTF8.GetBytes(filePath);
              MemoryStream stream = new MemoryStream();
              stream.Write(byteArray, 0, byteArray.Length);
              await file.CopyToAsync(stream);
          }
          if (fileName.EndsWith(".pdf"))// Important for security if saving in webroot
          {

              var filePath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\" + fileName;
              byte[] byteArray = Encoding.UTF8.GetBytes(filePath);
              MemoryStream stream = new MemoryStream();
              stream.Write(byteArray, 0, byteArray.Length);
              await file.CopyToAsync(stream);
          }

      }
      */
      if (files == null || files.Length == 0)
        return Content("file not selected");

      var path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot",
                  files.FileName);

      using (var stream = new FileStream(path, FileMode.Create))
      {
        await files.CopyToAsync(stream);
      }
      decimal SutartisIkeltaAlert = 0;
      Sutartis.FailoPavadinimas = files.FileName;
      Sutartis.KlientoUzsakymoId = kui;
      if (kui != null)
      {
        Sutartis.SutartiesTerminas = term;
      }
      string FileExtention = Path.GetExtension(files.FileName);
      if (FileExtention != ".pdf")
      {
        if (FileExtention != ".docx")

        {
          SutartisIkeltaAlert = 2;
          return RedirectToAction(nameof(Redirect), new { id = kui, SutartisIkeltaAlert = SutartisIkeltaAlert });

        }
      }
      if (ModelState.IsValid)
            {
               /* if (sutartis.Sutartis1 != null && sutartis.Sutartis1.ContentLength > 0)
                {
                    var uploadDir = "~/Files";
                    var sutartisPath = Server.MapPath(uploadDir);
                    sutartis.Sutartis1.SaveAs(sutartisPath);
                   
                }*/

                _context.Add(Sutartis);
                await _context.SaveChangesAsync();
        SutartisIkeltaAlert++;
        if (Sutartis.KlientoId != null)
        {
          return RedirectToAction(nameof(Redirect), new { id = kui, SutartisIkeltaAlert = SutartisIkeltaAlert });
        }
        else
        {
          return RedirectToAction(nameof(Create), new { id = kui, SutartisIkeltaAlert = SutartisIkeltaAlert });
        }
        }
            ViewData["KlientoId"] = Sutartis.KlientoId;
            ViewData["KlientoUzsakymoId"] = kui;
            ViewData["TiekejoId"] = new SelectList(_context.Tiekejas, "TiekejoId", "TiekejoPavadinimas", Sutartis.TiekejoId);
      if (Sutartis.KlientoId != null)
      {
        return RedirectToAction(nameof(Redirect), new { id = kui, SutartisIkeltaAlert = SutartisIkeltaAlert });
      }
      else
      {
        return RedirectToAction(nameof(Create), new { id = kui, SutartisIkeltaAlert = SutartisIkeltaAlert });
      }
    }

    public async Task<IActionResult> Siusti(string filename)
    {
      if (filename == null)
        return Content("filename not present");

      var path = Path.Combine(
                     Directory.GetCurrentDirectory(),
                     "wwwroot", filename);

      var memory = new MemoryStream();
      using (var stream = new FileStream(path, FileMode.Open))
      {
        await stream.CopyToAsync(memory);
      }
      memory.Position = 0;
      return File(memory, GetContentType(path), Path.GetFileName(path));
    }

    private string GetContentType(string path)
    {
      var types = GetMimeTypes();
      var ext = Path.GetExtension(path).ToLowerInvariant();
      return types[ext];
    }

    private Dictionary<string, string> GetMimeTypes()
    {
      return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".docx", "application/vnd.ms-word"}
            };
    }

      public IActionResult Redirect(decimal? id, decimal? SutartisIkeltaAlert)
        {
            string url = @Url.Action("Details", "KlientoUzsakymoAplankas", new { id = id, SutartisIkeltaAlert = SutartisIkeltaAlert });
            return Redirect(url);
        }
    // GET: Sutartis/Edit/5
    /* public async Task<IActionResult> Edit(decimal? id)
     {

         if (id == null)
         {
             return NotFound();
         }

         var sutartis = await _context.Sutartis.SingleOrDefaultAsync(m => m.SutartiesId == id);
         if (sutartis == null)
         {
             return NotFound();
         }
         ViewData["KlientoId"] = new SelectList(_context.Klientas, "KlientoId", "KlientoPavadinimas", sutartis.KlientoId);
         ViewData["KlientoUzsakymoId"] = new SelectList(_context.KlientoUzsakymoAplankas, "KlientoUzsakymoId", "Busena", sutartis.KlientoUzsakymoId);
         ViewData["TiekejoId"] = new SelectList(_context.Tiekejas, "TiekejoId", "TiekejoPavadinimas", sutartis.TiekejoId);
         return View(sutartis);
     }
     */
    // POST: Sutartis/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal kiES, decimal kuiES, DateTime spdES, DateTime stES, IFormFile files)
        {

      var sutartisEdit = await _context.Sutartis.SingleOrDefaultAsync(m => m.KlientoUzsakymoId == kuiES && m.KlientoId == kiES);
      sutartisEdit.SutartiesPasirasymoData = spdES;
      sutartisEdit.SutartiesTerminas = stES;
      if (files == null || files.Length == 0)
        return Content("file not selected");

      var path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot",
                  files.FileName);

      using (var stream = new FileStream(path, FileMode.Create))
      {
        await files.CopyToAsync(stream);
      }
      decimal SutartisIkeltaAlert = 0;
      sutartisEdit.FailoPavadinimas = files.FileName;
      string FileExtention = Path.GetExtension(files.FileName);
      if (FileExtention != ".pdf")
      {
        if (FileExtention != ".docx")

        {
          SutartisIkeltaAlert = 2;
          return RedirectToAction(nameof(Redirect), new { id = kuiES, SutartisIkeltaAlert = SutartisIkeltaAlert });
        }
      }

      if (ModelState.IsValid)
            {
                    _context.Update(sutartisEdit);
                    await _context.SaveChangesAsync();
        SutartisIkeltaAlert = 3;
        return RedirectToAction(nameof(Redirect), new { id = kuiES, SutartisIkeltaAlert = SutartisIkeltaAlert });
      }
            ViewData["KlientoId"] = new SelectList(_context.Klientas, "KlientoId", "KlientoPavadinimas", sutartisEdit.KlientoId);
            ViewData["KlientoUzsakymoId"] = new SelectList(_context.KlientoUzsakymoAplankas, "KlientoUzsakymoId", "Busena", sutartisEdit.KlientoUzsakymoId);
            ViewData["TiekejoId"] = new SelectList(_context.Tiekejas, "TiekejoId", "TiekejoPavadinimas", sutartisEdit.TiekejoId);
      return RedirectToAction(nameof(Redirect), new { id = kuiES, SutartisIkeltaAlert = SutartisIkeltaAlert });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTiekejasSutartis(decimal ti, DateTime sutartispas, DateTime sutartister, IFormFile files)
    {

      var sutartisEdit = await _context.Sutartis.SingleOrDefaultAsync( m=>m.TiekejoId == ti);
      sutartisEdit.SutartiesPasirasymoData = sutartispas;
      sutartisEdit.SutartiesTerminas = sutartister;
      if (files == null || files.Length == 0)
        return Content("file not selected");

      var path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot",
                  files.FileName);

      using (var stream = new FileStream(path, FileMode.Create))
      {
        await files.CopyToAsync(stream);
      }

      sutartisEdit.FailoPavadinimas = files.FileName;
      string FileExtention = Path.GetExtension(files.FileName);
      if (FileExtention != ".pdf")
      {
        if (FileExtention != ".docx")

        {

          return RedirectToAction(nameof(Index));

        }
      }

      if (ModelState.IsValid)
      {
        _context.Update(sutartisEdit);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["KlientoId"] = new SelectList(_context.Klientas, "KlientoId", "KlientoPavadinimas", sutartisEdit.KlientoId);
      ViewData["KlientoUzsakymoId"] = new SelectList(_context.KlientoUzsakymoAplankas, "KlientoUzsakymoId", "Busena", sutartisEdit.KlientoUzsakymoId);
      ViewData["TiekejoId"] = new SelectList(_context.Tiekejas, "TiekejoId", "TiekejoPavadinimas", sutartisEdit.TiekejoId);
      return RedirectToAction(nameof(Index));
    }

    // GET: Sutartis/Delete/5
    public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sutartis = await _context.Sutartis
                .Include(s => s.Kliento)
                .Include(s => s.KlientoUzsakymo)
                .Include(s => s.Tiekejo)
                .SingleOrDefaultAsync(m => m.SutartiesId == id);
            if (sutartis == null)
            {
                return NotFound();
            }

            return View(sutartis);
        }


        public async Task<IActionResult> DeleteConfirmed(decimal sid)
        {
            var sutartis = await _context.Sutartis.SingleOrDefaultAsync(m => m.SutartiesId == sid);
            _context.Sutartis.Remove(sutartis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SutartisExists(decimal id)
        {
            return _context.Sutartis.Any(e => e.SutartiesId == id);
        }
    }
}
