using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Web_Messaging_MVC.Data;
using Web_Messaging_MVC.Models;

namespace Web_Messaging_MVC.Controllers
{
    public class MessagingTypesController : Controller
    {
        private readonly Web_Messaging_DbContext _context;

        public MessagingTypesController(Web_Messaging_DbContext context)
        {
            _context = context;
        }

        // GET: MessagingTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MessagingType.ToListAsync());
        }

        // GET: MessagingTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messagingType = await _context.MessagingType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messagingType == null)
            {
                return NotFound();
            }

            return View(messagingType);
        }
        [Authorize]
        // GET: MessagingTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessagingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MethodName")] MessagingType messagingType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messagingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messagingType);
        }
        [Authorize]
        // GET: MessagingTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messagingType = await _context.MessagingType.FindAsync(id);
            if (messagingType == null)
            {
                return NotFound();
            }
            return View(messagingType);
        }

        // POST: MessagingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MethodName")] MessagingType messagingType)
        {
            if (id != messagingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messagingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagingTypeExists(messagingType.Id))
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
            return View(messagingType);
        }
        [Authorize]
        // GET: MessagingTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messagingType = await _context.MessagingType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messagingType == null)
            {
                return NotFound();
            }

            return View(messagingType);
        }

        // POST: MessagingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messagingType = await _context.MessagingType.FindAsync(id);
            _context.MessagingType.Remove(messagingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagingTypeExists(int id)
        {
            return _context.MessagingType.Any(e => e.Id == id);
        }
    }
}
