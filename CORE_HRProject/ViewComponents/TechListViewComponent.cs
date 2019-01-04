using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CORE_HRProject.Models;

namespace CORE_HRProject.ViewComponents
{


    public class TechListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var techLists = new List<Tech>
            {
                new Tech { Id = 1, Title = "ASP.NET Core" },
                new Tech { Id = 2, Title = "Bootstrap" },
                new Tech { Id = 3, Title = "C#" }
            };

            return View(techLists);
        }
    }
}
