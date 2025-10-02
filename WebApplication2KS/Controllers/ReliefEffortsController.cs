using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2KS.Data;
using WebApplication2KS.Models;

namespace WebApplication2KS.Controllers
{
    public class ReliefEffortsController : Controller
    {
        private readonly WebAppDbContext _context;

        public ReliefEffortsController(WebAppDbContext context)
        {
            _context = context;
        }

        // GET: ReliefEfforts
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReliefEfforts.ToListAsync());
        }

        // GET: ReliefEfforts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefEffort = await _context.ReliefEfforts
                .FirstOrDefaultAsync(m => m.EffortId == id);
            if (reliefEffort == null)
            {
                return NotFound();
            }

            return View(reliefEffort);
        }

        // GET: ReliefEfforts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReliefEfforts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EffortId,Title,Description,Date")] ReliefEffort reliefEffort)
        {
            
            _context.Add(reliefEffort);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: ReliefEfforts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefEffort = await _context.ReliefEfforts.FindAsync(id);
            if (reliefEffort == null)
            {
                return NotFound();
            }
            return View(reliefEffort);
        }

        // POST: ReliefEfforts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EffortId,Title,Description,Date")] ReliefEffort reliefEffort)
        {
            if (id != reliefEffort.EffortId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reliefEffort);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReliefEffortExists(reliefEffort.EffortId))
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
            return View(reliefEffort);
        }

        // GET: ReliefEfforts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefEffort = await _context.ReliefEfforts
                .FirstOrDefaultAsync(m => m.EffortId == id);
            if (reliefEffort == null)
            {
                return NotFound();
            }

            return View(reliefEffort);
        }

        // POST: ReliefEfforts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reliefEffort = await _context.ReliefEfforts.FindAsync(id);
            if (reliefEffort != null)
            {
                _context.ReliefEfforts.Remove(reliefEffort);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReliefEffortExists(int id)
        {
            return _context.ReliefEfforts.Any(e => e.EffortId == id);
        }
    }
}
