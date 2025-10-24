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
    public class DonationsController : Controller
    {
        private readonly WebAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DonationsController(WebAppDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var webAppDbContext = _context.Donations.Include(d => d.User);
            return View(await webAppDbContext.ToListAsync());
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationId,Amount,Type,Date")] Donation donation)
        {
            WebApplication2KS.Models.User user = new WebApplication2KS.Models.User();
            var loggedInUser = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(loggedInUser);
            var logged  = _context.Users.FirstOrDefault(u => u.EmailAddress == email) ?? user;



            
            user.EmailAddress = logged.EmailAddress;
            user.Password = logged.Password;
            user.Name = logged.Name;

            donation.UserId = user.UserId;
            donation.User = user;

            _context.Add(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", donation.UserId);
            return View(donation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationId,UserId,Amount,Type,Date")] Donation donation)
        {
            if (id != donation.DonationId)
            {
                return NotFound();
            }
            WebApplication2KS.Models.User user = new WebApplication2KS.Models.User();
            var loggedInUser = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(loggedInUser);
            var logged = _context.Users.FirstOrDefault(u => u.EmailAddress == email) ?? user;




            user.EmailAddress = logged.EmailAddress;
            user.Password = logged.Password;
            user.Name = logged.Name;

            donation.UserId = user.UserId;
            donation.User = user;

            try
            {
                _context.Update(donation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationExists(donation.DonationId))
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

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donations.Any(e => e.DonationId == id);
        }
    }
}
