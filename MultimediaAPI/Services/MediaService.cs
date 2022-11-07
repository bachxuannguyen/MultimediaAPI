using System;
using System.Collections.Generic;
using System.Linq;
using MultimediaAPI.Models;
using MultimediaAPI.Contexts;
using MultimediaAPI.Extentions;
using System.IO;

namespace MultimediaAPI.Services
{
    public interface IMediaService
    {
        bool CreateMedia(Media media);
        bool CreateMedia(Media media, List<int> albumId);
        Media GetMedia(int mediaId);
        bool UpdateMedia(Media media);
        bool DeleteMedia(int mediaId);
        List<Media> GetAllMedia();
        List<string> GetFileInfo(int mediaId);
    }
    public class MediaService : IMediaService
    {
        private readonly MultimediaContext _dbContext;
        private readonly IFileInfoService _fileInfoService;
        private readonly IRelationService _relationService;
        public MediaService(MultimediaContext dbContext, IFileInfoService fileInfoService, IRelationService relationService)
        {
            _dbContext = dbContext;
            _fileInfoService = fileInfoService;
            _relationService = relationService;
        }
        public bool CreateMedia(Media media)
        {
            try
            {
                _dbContext.MediaSet.Add(media);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public bool CreateMedia(Media media, List<int> albumId)
        {
            try
            {
                _dbContext.MediaSet.Add(media);
                foreach (int i in albumId)
                {
                    _relationService.CreateRelation(new Relationship
                    {
                        MediaId = media.Id,
                        AlbumId = i
                    });
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public Media GetMedia(int mediaId)
        {
            try
            {
                Media existingMedia = _dbContext.MediaSet.Find(mediaId);
                return existingMedia;
            }
            catch (Exception e)
            {
                e.Log();
                return new Media();
            }
        }
        public bool UpdateMedia(Media media)
        {
            try
            {
                Media existingMedia = _dbContext.MediaSet.Find(media.Id);
                _dbContext.Entry(existingMedia).CurrentValues.SetValues(media);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public bool DeleteMedia(int mediaId)
        {
            try
            {
                Media existingMedia = _dbContext.MediaSet.Find(mediaId);
                _dbContext.MediaSet.Attach(existingMedia);
                _dbContext.MediaSet.Remove(existingMedia);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public List<Media> GetAllMedia()
        {
            try
            {
                return (from m in _dbContext.MediaSet select m).ToList();
            }
            catch (Exception e)
            {
                e.Log();
                return new List<Media>();
            }
        }
        public List<string> GetFileInfo(int mediaId)
        {
            Media existingMedia = GetMedia(mediaId);
            string fileUrl = Path.Combine(AppEnvironment.GetMediaFolder(), existingMedia.FileName);
            return _fileInfoService.GetFileInfo(fileUrl);
        }
    }
}
