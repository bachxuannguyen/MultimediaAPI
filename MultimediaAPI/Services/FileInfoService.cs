using MultimediaAPI.Extentions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MultimediaAPI.Services
{
    public interface IFileInfoService
    {
        List<string> GetFileInfo(string fileUrl);
    }
    public class FileInfoService : IFileInfoService
    {
        private readonly MediaBaseService _mediaBaseService;
        public FileInfoService(MediaBaseService mediaBaseService)
        {
            _mediaBaseService = mediaBaseService;
        }
        public List<string> GetFileInfo(string fileUrl)
        {
            string fileExtension = new System.IO.FileInfo(fileUrl).Extension;
            int mediaTypeId = _mediaBaseService.GetTypeIdByExtension(fileExtension);
            switch (mediaTypeId)
            {
                case 1:
                    {
                        IFileInfo fileInfo = new ImageFileInfo();
                        return fileInfo.GetFileInfo(fileUrl);
                    }
                case 4:
                    {
                        IFileInfo fileInfo = new VideoFileInfo();
                        return fileInfo.GetFileInfo(fileUrl);
                    }
                default:
                    {
                        IFileInfo fileInfo = new FileInfo();
                        return fileInfo.GetFileInfo(fileUrl);
                    }
            }
        }
    }
    public interface IFileInfo
    {
        List<string> GetFileInfo(string fileUrl);
    }
    public class FileInfo : IFileInfo
    {
        public virtual List<string> GetFileInfo(string fileUrl)
        {
            return new List<string> { GetFileSize(fileUrl).ToString() };
        }
        public long GetFileSize(string fileUrl)
        {
            try
            {
                return new System.IO.FileInfo(fileUrl).Length;
            }
            catch (Exception e)
            {
                e.Log();
                return 0;
            }
        }
    }
    public class ImageFileInfo : FileInfo
    {
        public override List<string> GetFileInfo(string fileUrl)
        {
            List<string> fileInfo = new() { base.GetFileSize(fileUrl).ToString() };
            Bitmap bitmap = new(fileUrl);
            fileInfo.Add(bitmap.Width.ToString());
            fileInfo.Add(bitmap.Height.ToString());
            return fileInfo;
        }
    }
    public class VideoFileInfo : FileInfo
    {
        public override List<string> GetFileInfo(string fileUrl)
        {
            List<string> fileInfo = new() { base.GetFileSize(fileUrl).ToString() };
            fileInfo.Add("video specific info");
            return fileInfo;
        }
    }
}
