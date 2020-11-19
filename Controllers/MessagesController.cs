using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Web_Messaging_MVC.Data;
using Web_Messaging_MVC.Models;

namespace Web_Messaging_MVC.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly Web_Messaging_DbContext _context;

        public MessagesController(Web_Messaging_DbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var web_Messaging_DbContext = _context.Message.Include(m => m.MessagingType).Include(m => m.Receiver).Include(m => m.Sender);
            return View(await web_Messaging_DbContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.MessagingType)
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["MessagingTypeId"] = new SelectList(_context.Set<MessagingType>(), "Id", "Id");
            ViewData["ReceiverId"] = new SelectList(_context.Set<Receiver>(), "Id", "Id");
            ViewData["SenderId"] = new SelectList(_context.Set<Sender>(), "Id", "Id");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SenderId,ReceiverId,MessagingTypeId,MessageBody")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MessagingTypeId"] = new SelectList(_context.Set<MessagingType>(), "Id", "Id", message.MessagingTypeId);
            ViewData["ReceiverId"] = new SelectList(_context.Set<Receiver>(), "Id", "Id", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Set<Sender>(), "Id", "Id", message.SenderId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["MessagingTypeId"] = new SelectList(_context.Set<MessagingType>(), "Id", "Id", message.MessagingTypeId);
            ViewData["ReceiverId"] = new SelectList(_context.Set<Receiver>(), "Id", "Id", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Set<Sender>(), "Id", "Id", message.SenderId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SenderId,ReceiverId,MessagingTypeId,MessageBody")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Id))
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
            ViewData["MessagingTypeId"] = new SelectList(_context.Set<MessagingType>(), "Id", "Id", message.MessagingTypeId);
            ViewData["ReceiverId"] = new SelectList(_context.Set<Receiver>(), "Id", "Id", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Set<Sender>(), "Id", "Id", message.SenderId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.MessagingType)
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.FindAsync(id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}
