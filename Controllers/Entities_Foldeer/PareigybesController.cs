/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;

namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
    public class PareigybesController : Controller
    {
        private readonly Alaus_daryklos_gamybos_valdymas_DBContext _context;

        public PareigybesController(Alaus_daryklos_gamybos_valdymas_DBContext context)
        {
            _context = context;
        }

        // GET: Pareigybes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pareigybe.ToListAsync());
        }

        // GET: Pareigybes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pareigybe = await _context.Pareigybe
                .SingleOrDefaultAsync(m => m.PareigybesId == id);
            if (pareigybe == null)
            {
                return NotFound();
            }

            return View(pareigybe);
        }

        // GET: Pareigybes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pareigybes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PareigybesId,Paeigybe")] Pareigybe pareigybe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pareigybe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pareigybe);
        }

        // GET: Pareigybes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pareigybe = await _context.Pareigybe.SingleOrDefaultAsync(m => m.PareigybesId == id);
            if (pareigybe == null)
            {
                return NotFound();
            }
            return View(pareigybe);
        }

        // POST: Pareigybes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("PareigybesId,Paeigybe")] Pareigybe pareigybe)
        {
            if (id != pareigybe.PareigybesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pareigybe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PareigybeExists(pareigybe.PareigybesId))
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
            return View(pareigybe);
        }

        // GET: Pareigybes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pareigybe = await _context.Pareigybe
                .SingleOrDefaultAsync(m => m.PareigybesId == id);
            if (pareigybe == null)
            {
                return NotFound();
            }

            return View(pareigybe);
        }

        // POST: Pareigybes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var pareigybe = await _context.Pareigybe.SingleOrDefaultAsync(m => m.PareigybesId == id);
            _context.Pareigybe.Remove(pareigybe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PareigybeExists(decimal id)
        {
            return _context.Pareigybe.Any(e => e.PareigybesId == id);
        }
    }
}
*/