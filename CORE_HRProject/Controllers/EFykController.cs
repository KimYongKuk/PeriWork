using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CORE_HRProject.Data;
using CORE_HRProject.Models;

namespace CORE_HRProject.Controllers
{
    public class EFykController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EFykController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: EFyk
        public async Task<IActionResult> Index()
        {
            return View(await _context.EFyk.ToListAsync());
            //return View(await efData.ToListAsync());
        }

        // GET: EFyk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eFyk = await _context.EFyk
                .SingleOrDefaultAsync(m => m.id == id);
            if (eFyk == null)
            {
                return NotFound();
            }

            return View(eFyk);
        }

        // GET: EFyk/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EFyk/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name")] EFyk eFyk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eFyk);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eFyk);
        }

        // GET: EFyk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eFyk = await _context.EFyk.SingleOrDefaultAsync(m => m.id == id);
            if (eFyk == null)
            {
                return NotFound();
            }
            return View(eFyk);
        }

        // POST: EFyk/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name")] EFyk eFyk)
        {
            if (id != eFyk.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eFyk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EFykExists(eFyk.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(eFyk);
        }

        // GET: EFyk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eFyk = await _context.EFyk
                .SingleOrDefaultAsync(m => m.id == id);
            if (eFyk == null)
            {
                return NotFound();
            }

            return View(eFyk);
        }

        // POST: EFyk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eFyk = await _context.EFyk.SingleOrDefaultAsync(m => m.id == id);
            _context.EFyk.Remove(eFyk);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EFykExists(int id)
        {
            return _context.EFyk.Any(e => e.id == id);
        }
    }
}
