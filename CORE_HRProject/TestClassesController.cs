using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CORE_HRProject.Data;
using CORE_HRProject.Models;

namespace CORE_HRProject
{
    public class TestClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestClassesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TestClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestClass.ToListAsync());
        }

        // GET: TestClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testClass = await _context.TestClass
                .SingleOrDefaultAsync(m => m.Id == id);
            if (testClass == null)
            {
                return NotFound();
            }

            return View(testClass);
        }

        // GET: TestClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] TestClass testClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testClass);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(testClass);
        }

        // GET: TestClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testClass = await _context.TestClass.SingleOrDefaultAsync(m => m.Id == id);
            if (testClass == null)
            {
                return NotFound();
            }
            return View(testClass);
        }

        // POST: TestClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] TestClass testClass)
        {
            if (id != testClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestClassExists(testClass.Id))
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
            return View(testClass);
        }

        // GET: TestClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testClass = await _context.TestClass
                .SingleOrDefaultAsync(m => m.Id == id);
            if (testClass == null)
            {
                return NotFound();
            }

            return View(testClass);
        }

        // POST: TestClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testClass = await _context.TestClass.SingleOrDefaultAsync(m => m.Id == id);
            _context.TestClass.Remove(testClass);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TestClassExists(int id)
        {
            return _context.TestClass.Any(e => e.Id == id);
        }
    }
}
