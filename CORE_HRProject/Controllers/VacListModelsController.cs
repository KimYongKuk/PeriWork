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
    public class VacListModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacListModelsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: VacListModels
        public async Task<IActionResult> Index(string id , string pw)
        {
            ViewBag.id = id;
            ViewBag.pw = pw;

            return View(await _context.VacListModel.ToListAsync());

        }

        // GET: VacListModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacListModel = await _context.VacListModel
                .SingleOrDefaultAsync(m => m.id == id);
            if (vacListModel == null)
            {
                return NotFound();
            }

            return View(vacListModel);
        }

        // GET: VacListModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VacListModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,reason,status")] VacListModel vacListModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacListModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vacListModel);
        }

        // GET: VacListModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacListModel = await _context.VacListModel.SingleOrDefaultAsync(m => m.id == id);
            if (vacListModel == null)
            {
                return NotFound();
            }
            return View(vacListModel);
        }

        // POST: VacListModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,reason,status")] VacListModel vacListModel)
        {
            if (id != vacListModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacListModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacListModelExists(vacListModel.id))
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
            return View(vacListModel);
        }

        // GET: VacListModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacListModel = await _context.VacListModel
                .SingleOrDefaultAsync(m => m.id == id);
            if (vacListModel == null)
            {
                return NotFound();
            }

            return View(vacListModel);
        }
        
        public IActionResult Test()
        {
            return View();
        }

        // POST: VacListModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacListModel = await _context.VacListModel.SingleOrDefaultAsync(m => m.id == id);
            _context.VacListModel.Remove(vacListModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VacListModelExists(int id)
        {
            return _context.VacListModel.Any(e => e.id == id);
        }
    }
}
