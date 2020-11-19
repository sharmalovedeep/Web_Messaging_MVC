using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Web_Messaging_MVC.Data;
using Web_Messaging_MVC.Models;

namespace Web_Messaging_MVC.Controllers
{
    public class ReceiversController : Controller
    {
        private readonly Web_Messaging_DbContext _context;

        public ReceiversController(Web_Messaging_DbContext context)
        {
            _context = context;
        }

        // GET: Receivers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Receiver.ToListAsync());
        }

        // GET: Receivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiver = await _context.Receiver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiver == null)
            {
                return NotFound();
            }

            return View(receiver);
        }
        [Authorize]
        // GET: Receivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReceiverName,ReceiverEmail,RecieverMobile")] Receiver receiver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receiver);
        }
        [Authorize]
        // GET: Receivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiver = await _context.Receiver.FindAsync(id);
            if (receiver == null)
            {
                return NotFound();
            }
            return View(receiver);
        }

        // POST: Receivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReceiverName,ReceiverEmail,RecieverMobile")] Receiver receiver)
        {
            if (id != receiver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiverExists(receiver.Id))
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
            return View(receiver);
        }
        [Authorize]
        // GET: Receivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiver = await _context.Receiver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiver == null)
            {
                return NotFound();
            }

            return View(receiver);
        }

        // POST: Receivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiver = await _context.Receiver.FindAsync(id);
            _context.Receiver.Remove(receiver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiverExists(int id)
        {
            return _context.Receiver.Any(e => e.Id == id);
        }
    }
}
