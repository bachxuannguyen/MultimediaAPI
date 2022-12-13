using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MultimediaAPI.Libraries
{
    public static class LibVariable
    {
        public const int viewSmallPercent = 20;
        public const int viewMediumPercent = 50;
        public const int viewLargePercent = 100;
        public const int viewSmallPixel = 150;
        public const int viewMediumPixel = 300;
        public const int viewLargePixel = 500;

        public const string colorGreenBg = "6bac68";
        public const string colorRedBg = "ba6262";
        public const string colorBlueBg = "498ca9";
        public const string colorGreen = "358739";
        public const string colorRed = "a42323";
        public const string colorBlue = "2c629d";

        public static readonly List<string> fileTypes = new() { "bmp", "gif", "png", "jpg", "jpeg", "mp3", "wma", "mp4", "wmv", "avi", "mov", "swf", "fla" };
        public static readonly List<ValueTuple<int, List<string>>> mediaExtensions = new()
        {
            ValueTuple.Create(0, new List<string>() { ".bmp", ".gif", ".png", ".jpg", ".jpeg" }),
            ValueTuple.Create(2, new List<string>() { ".mp4", ".mov", ".wmv", ".mkv" }),
            ValueTuple.Create(3, new List<string>() { ".mp3", ".wma" }),
            ValueTuple.Create(4, new List<string>() { ".fla", ".swf" }),
        };
        public static readonly List<ValueTuple<int, string, string>> mediaTypes = new()
        {
            ValueTuple.Create(0, "Image", "Ảnh"),
            ValueTuple.Create(1, "Infographic", "Infographic"),
            ValueTuple.Create(2, "Video", "Video"),
            ValueTuple.Create(3, "Sound", "Âm thanh"),
            ValueTuple.Create(4, "Animation", "Hoạt họa")
        };
    }
    public static class LibMethod
    {
        private static readonly Random random = new();
        public static string CreateFileNamePrefix(int prefixLength)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(characters, prefixLength).Select(s => s[random.Next(s.Length)]).ToArray()).ToLower();
        }
        public static string GetMediaTypeNameById(int mediaId)
        {
            List<int> allowedIds = (from mediaType in LibVariable.mediaTypes select mediaType.Item1).ToList();
            if (allowedIds.Contains(mediaId))
                return (from mediaType in LibVariable.mediaTypes where (mediaType.Item1 == mediaId) select mediaType.Item2).ToList().FirstOrDefault();
            else
                return String.Empty;
        }
        public static string GetMediaTypeDescById(int mediaId)
        {
            List<int> allowedId = (from mediaType in LibVariable.mediaTypes select mediaType.Item1).ToList();
            if (allowedId.Contains(mediaId))
                return (from mediaType in LibVariable.mediaTypes where (mediaType.Item1 == mediaId) select mediaType.Item3).ToList().FirstOrDefault();
            else
                return String.Empty;
        }
        public static int GetMediaTypeIdByExtension(string mediaExtension)
        {
            List<string> bigExtensionSet = new() { };
            foreach (var extension in LibVariable.mediaExtensions)
                bigExtensionSet = bigExtensionSet.Concat(extension.Item2).Distinct().ToList();
            if (String.IsNullOrEmpty(mediaExtension) || !bigExtensionSet.Contains(mediaExtension.ToLower()))
            {
                return -1;
            }
            else
            {
                int x = (from extension in LibVariable.mediaExtensions
                        where extension.Item2.Contains(mediaExtension.ToLower())
                        select extension.Item1).ToList().FirstOrDefault();
                Debug.WriteLine(x);
                return x;
            }
        }
        public static string GetMediaTypeNameByExtension(string mediaExtension)
        {
            return (GetMediaTypeNameById(GetMediaTypeIdByExtension(mediaExtension)));
        }
        public static string GetMediaTypeDescByExtension(string mediaExtension)
        {
            return (GetMediaTypeDescById(GetMediaTypeIdByExtension(mediaExtension)));
        }
    }
}
