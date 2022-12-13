using MultimediaAPI.Contexts;
using MultimediaAPI.Extentions;
using MultimediaAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultimediaAPI.Services
{
    public interface IMediaService
    {
        bool CreateMedia(Media media);
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
                if (media.AlbumId is not null && media.AlbumId.Count > 0)
                {
                    foreach (int i in media.AlbumId)
                    {
                        _relationService.CreateRelation(new Relationship
                        {
                            MediaId = media.Id,
                            AlbumId = i
                        });
                    }
                }
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
                _relationService.DeleteRelationByMediaId(media.Id);
                if (media.AlbumId is not null && media.AlbumId.Count > 0)
                {
                    foreach (int i in media.AlbumId)
                    {
                        _relationService.CreateRelation(new Relationship
                        {
                            MediaId = media.Id,
                            AlbumId = i
                        });
                    }
                }
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
                _relationService.DeleteRelationByMediaId(mediaId);
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
