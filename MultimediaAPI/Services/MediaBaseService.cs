using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimediaAPI.Services
{
    public class MediaBaseService
    {
        private readonly List<string> FileTypes = new List<string> { ".bmp", ".gif", ".png", ".jpg", ".jpeg", ".mp3", ".wma", ".mp4", ".wmv", ".avi", ".mov", ".swf", ".fla" };
        private readonly List<ValueTuple<int, List<string>>> MediaExtensions = new List<ValueTuple<int, List<string>>>()
        {
            ValueTuple.Create(1, new List<string>() { ".bmp", ".gif", ".png", ".jpg", ".jpeg" }),
            ValueTuple.Create(3, new List<string>() { ".mp3", ".wma" }),
            ValueTuple.Create(4, new List<string>() { ".mp4", ".mov", ".wmv", ".mkv" }),
            ValueTuple.Create(5, new List<string>() { ".fla", ".swf" }),
        };
        private readonly List<ValueTuple<int, string, string>> MediaTypes = new List<ValueTuple<int, string, string>>()
        {
            ValueTuple.Create(1, "Image", "Ảnh"),
            ValueTuple.Create(2, "Infographic", "Infographic"),
            ValueTuple.Create(3, "Audio", "Âm thanh"),
            ValueTuple.Create(4, "Video", "Video"),
            ValueTuple.Create(5, "Animation", "Hoạt họa")
        };
        public string GetTypeNameById(int id)
        {
            List<int> allowedIds = (from mediaType in MediaTypes select mediaType.Item1).ToList();
            if (allowedIds.Contains(id))
                return (from mediaType in MediaTypes where (mediaType.Item1 == id) select mediaType.Item2).ToList().FirstOrDefault();
            else
                return String.Empty;
        }
        public string GetTypeDescById(int id)
        {
            List<int> allowedId = (from mediaType in MediaTypes select mediaType.Item1).ToList();
            if (allowedId.Contains(id))
                return (from mediaType in MediaTypes where (mediaType.Item1 == id) select mediaType.Item3).ToList().FirstOrDefault();
            else
                return String.Empty;
        }
        public int GetTypeIdByExtension(string extension)
        {
            return (from mediaExtension in MediaExtensions
                    where mediaExtension.Item2.Contains(extension.ToLower())
                    select mediaExtension.Item1).ToList().FirstOrDefault();
        }
        public string GetTypeNameByExtension(string _extension)
        {
            return (GetTypeNameById(GetTypeIdByExtension(_extension)));
        }
        public string GetTypeDescByExtension(string _extension)
        {
            return (GetTypeDescById(GetTypeIdByExtension(_extension)));
        }
    }
}
