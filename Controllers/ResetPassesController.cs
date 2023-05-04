using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKsEMS.Data;
using MKsEMS.ViewModels;

namespace MKsEMS.Controllers
{
    public class ResetPassesController : Controller
    {
        private readonly EMSDbContext _context;

        public ResetPassesController(EMSDbContext context)
        {
            _context = context;
        }

        // GET: ResetPasses
        public async Task<IActionResult> Index()
        {
              return _context.ResetPasses != null ? 
                          View(await _context.ResetPasses.ToListAsync()) :
                          Problem("Entity set 'EMSDbContext.ResetPasses'  is null.");
        }

        // GET: ResetPasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResetPasses == null)
            {
                return NotFound();
            }

            var resetPass = await _context.ResetPasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resetPass == null)
            {
                return NotFound();
            }

            return View(resetPass);
        }

        // GET: ResetPasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResetPasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,CurrentPassword,NewPassword,ReEnterNewPassword")] ResetPass resetPass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resetPass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resetPass);
        }

        // GET: ResetPasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResetPasses == null)
            {
                return NotFound();
            }

            var resetPass = await _context.ResetPasses.FindAsync(id);
            if (resetPass == null)
            {
                return NotFound();
            }
            return View(resetPass);
        }

        // POST: ResetPasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,CurrentPassword,NewPassword,ReEnterNewPassword")] ResetPass resetPass)
        {
            if (id != resetPass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resetPass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResetPassExists(resetPass.Id))
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
            return View(resetPass);
        }

        // GET: ResetPasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResetPasses == null)
            {
                return NotFound();
            }

            var resetPass = await _context.ResetPasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resetPass == null)
            {
                return NotFound();
            }

            return View(resetPass);
        }

        // POST: ResetPasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResetPasses == null)
            {
                return Problem("Entity set 'EMSDbContext.ResetPasses'  is null.");
            }
            var resetPass = await _context.ResetPasses.FindAsync(id);
            if (resetPass != null)
            {
                _context.ResetPasses.Remove(resetPass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResetPassExists(int id)
        {
          return (_context.ResetPasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
