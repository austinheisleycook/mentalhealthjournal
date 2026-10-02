using healthjournal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace healthjournal.Controllers
{
    public class HealthController : Controller
    {
        private readonly HealthConfig _context;

        public HealthController(HealthConfig context)
        {
            _context = context;
        }

        // GET: HealthController
        public async Task<IActionResult> Index()
        {
            var entries = await _context.HealthModels
                .OrderByDescending(h => h.Date)
                .ToListAsync();

            return View(entries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HealthModel model)
        {
            if (ModelState.IsValid)
            {
                _context.HealthModels.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
