using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimediaAPI.Controllers
{
    public class ExceptionController : Controller
    {
        public IActionResult Index()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var path = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exception == null)
                return View();
            else
                return View(exception.ToString() + "/n" + path.ToString());
        }
    }
}
