using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Web_Messaging_MVC.Data;
using Web_Messaging_MVC.Models;

namespace Web_Messaging_MVC.Controllers
{
    public class SendersController : Controller
    {
        private readonly Web_Messaging_DbContext _context;

        public SendersController(Web_Messaging_DbContext context)
        {
            _context = context;
        }

        // GET: Senders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sender.ToListAsync());
        }

        // GET: Senders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sender = await _context.Sender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sender == null)
            {
                return NotFound();
            }

            return View(sender);
        }
        [Authorize]
        // GET: Senders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Senders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SenderName,SenderEmail")] Sender sender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sender);
        }
        [Authorize]
        // GET: Senders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sender = await _context.Sender.FindAsync(id);
            if (sender == null)
            {
                return NotFound();
            }
            return View(sender);
        }

        // POST: Senders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SenderName,SenderEmail")] Sender sender)
        {
            if (id != sender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SenderExists(sender.Id))
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
            return View(sender);
        }
        [Authorize]
        // GET: Senders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sender = await _context.Sender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sender == null)
            {
                return NotFound();
            }

            return View(sender);
        }

        // POST: Senders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sender = await _context.Sender.FindAsync(id);
            _context.Sender.Remove(sender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SenderExists(int id)
        {
            return _context.Sender.Any(e => e.Id == id);
        }
    }
}
