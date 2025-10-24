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
    public class VolunteersController : Controller
    {
        private readonly WebAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VolunteersController(WebAppDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index()
        {
            var webAppDbContext = _context.Volunteers.Include(v => v.User);
            return View(await webAppDbContext.ToListAsync());
        }

        // GET: Volunteers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VolunteerId == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // GET: Volunteers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress");
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VolunteerId,UserId,Name,EmailAddress")] Volunteer volunteer)
        {
            WebApplication2KS.Models.User user = new WebApplication2KS.Models.User();
            var loggedInUser = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(loggedInUser);
            var logged = _context.Users.FirstOrDefault(u => u.EmailAddress == email) ?? user;


            user.EmailAddress = logged.EmailAddress;
            user.Password = logged.Password;
            user.Name = logged.Name;

            volunteer.User = user;

            _context.Add(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", volunteer.UserId);
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VolunteerId,UserId,Name,EmailAddress")] Volunteer volunteer)
        {
            if (id != volunteer.VolunteerId)
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

            volunteer.User = user;
            try
            {
                _context.Update(volunteer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(volunteer.VolunteerId))
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

        // GET: Volunteers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VolunteerId == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(e => e.VolunteerId == id);
        }
    }
}
