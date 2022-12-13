using System;
using System.Collections.Generic;
using System.IO;

namespace MultimediaAPI.Extentions
{
    public static class ExceptionExtention
    {
        public static Exception Log(this Exception e)
        {
            //HttpContextAccessor accessor = new HttpContextAccessor();
            //IWebHostEnvironment env = accessor.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            string rootPath = "Logs/Exceptions";
            File.AppendAllLines(
                path: Path.Combine(rootPath, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".log"),
                contents: new List<string>
                {
                    DateTime.Now.ToString("HH:mm:ss") + ": " + e.Message + "\n" + e.ToString() + "\n"
                }
                );
            return e;
        }
    }
}
