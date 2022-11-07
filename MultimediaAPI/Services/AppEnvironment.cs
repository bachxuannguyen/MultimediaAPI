using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MultimediaAPI.Services
{
    public static class AppEnvironment
    {
        private static readonly IWebHostEnvironment _env;
        static AppEnvironment()
        {
            HttpContextAccessor accessor = new HttpContextAccessor();
            _env = accessor.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
        }
        //Path processes.
        public static string GetRootFolder()
        {
            return _env.ContentRootPath;
        }
        public static string GetLogFolder()
        {
            return Path.Combine(_env.ContentRootPath, "Logs", "Exceptions");
        }
        public static string GetMediaFolder()
        {
            return Path.Combine(_env.ContentRootPath, "Contents", "Media");
        }
        //Media base processes.
    }
}
