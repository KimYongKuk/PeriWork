using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CORE_HRProject.Controllers
{
    public class SweetAlertBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public void Alert(string message, Enum.NotificationType notificationType)
        {
            var msg = "swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "";
            TempData["notification"] = msg;
        }

        public class Enum
        {
            public enum NotificationType
            {
                error,
                success,
                warning,
                info
            }

        }

    }
}