using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2KS.Data;
using WebApplication2KS.Models;

namespace WebApplication2KS.Controllers
{
    public class DisasterAlertsController : Controller
    {
        private readonly WebAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DisasterAlertsController(WebAppDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DisasterAlerts
        public async Task<IActionResult> Index()
        {
            var webAppDbContext = _context.DisasterAlerts.Include(d => d.User);
            return View(await webAppDbContext.ToListAsync());
        }

        // GET: DisasterAlerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disasterAlert = await _context.DisasterAlerts
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (disasterAlert == null)
            {
                return NotFound();
            }

            return View(disasterAlert);
        }

        // GET: DisasterAlerts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress");
            return View();
        }

        // POST: DisasterAlerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlertId,UserId,Title,Type,Date,Location")] DisasterAlert disasterAlert)
        {
           
            WebApplication2KS.Models.User user = new WebApplication2KS.Models.User();
            var loggedInUser = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(loggedInUser);
            var logged = _context.Users.FirstOrDefault(u => u.EmailAddress == email) ?? user;

            //assigning the logged in user to the alert
            
            user.EmailAddress = logged.EmailAddress;
            user.Password = logged.Password;
            user.Name = logged.Name;

            //creating a dummy relief effort to link with the alert
            ReliefEffort relief = new ReliefEffort();
            relief.EffortId = 0;
            relief.Title = "";
            relief.Date = DateTime.Now;
            relief.Description = "";
           
            disasterAlert.ReliefEffort = relief;
            disasterAlert.User = user;

            _context.Add(disasterAlert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: DisasterAlerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disasterAlert = await _context.DisasterAlerts.FindAsync(id);
            if (disasterAlert == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", disasterAlert.UserId);
            return View(disasterAlert);
        }

        // POST: DisasterAlerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlertId,UserId,Title,Type,Date,Location")] DisasterAlert disasterAlert)
        {
            if (id != disasterAlert.AlertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disasterAlert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterAlertExists(disasterAlert.AlertId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", disasterAlert.UserId);
            return View(disasterAlert);
        }

        // GET: DisasterAlerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disasterAlert = await _context.DisasterAlerts
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (disasterAlert == null)
            {
                return NotFound();
            }

            return View(disasterAlert);
        }

        // POST: DisasterAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disasterAlert = await _context.DisasterAlerts.FindAsync(id);
            if (disasterAlert != null)
            {
                _context.DisasterAlerts.Remove(disasterAlert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterAlertExists(int id)
        {
            return _context.DisasterAlerts.Any(e => e.AlertId == id);
        }
    }
}
