using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CORE_HRProject.Models;
using CORE_HRProject.Data;
using Microsoft.EntityFrameworkCore;
using CORE_HRProject.Services;

namespace CORE_HRProject.ViewComponents
{
    public class EFykViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public EFykViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchString)
        {
            var items = from m in _context.EFyk
                        select m;

            //if(!String.IsNullOrEmpty(searchString))
            //{
            //    items = items.Where(s => s.name.Contains(searchString));
            //}

            //var items = await GetItemsAsync();
            //return View(items);

            return View(await items.ToListAsync());
        }

        private Task<List<EFyk>> GetItemsAsync()
        {
            return _context.EFyk.ToListAsync();
        }

        //public async Task<IActionResult> Edit(int? id)
        public IViewComponentResult Edit()
        {
            ITest test = new ITest();

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var eFyk = await _context.EFyk.SingleOrDefaultAsync(m => m.id == id);
            //if (eFyk == null)
            //{
            //    return NotFound();
            //}
            ViewData["url"] = test.getUrl();
            return View("Default");
        }


    }
}
