﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultimediaAPI.Models;
using MultimediaAPI.Contexts;
using MultimediaAPI.Extentions;

namespace MultimediaAPI.Services
{
    public interface IAlbumService
    {
        bool CreateAlbum(Album album);
        bool CreateAlbum(Album adlbum, List<int> mediaId);
        Album GetAlbum(int albumId);
        bool UpdateAlbum(Album album);
        bool DeleteAlbum(int albumId);
        List<Album> GetAllAlbum();
    }
    public class AlbumService : IAlbumService
    {
        private readonly MultimediaContext _dbContext;
        private readonly IRelationService _relationService;
        public AlbumService(MultimediaContext dbContext, IRelationService relationService)
        {
            _dbContext = dbContext;
            _relationService = relationService;
        }
        public bool CreateAlbum(Album album)
        {
            try
            {
                _dbContext.AlbumSet.Add(album);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public bool CreateAlbum(Album album, List<int> mediaId)
        {
            try
            {
                _dbContext.AlbumSet.Add(album);
                foreach (int i in mediaId)
                {
                    _relationService.CreateRelation(new Relationship
                    {
                        MediaId = i,
                        AlbumId = album.Id
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
        public Album GetAlbum(int albumId)
        {
            try
            {
                Album existingAlbum = _dbContext.AlbumSet.Find(albumId);
                return existingAlbum;
            }
            catch (Exception e)
            {
                e.Log();
                return new Album();
            }
        }
        public bool UpdateAlbum(Album album)
        {
            try
            {
                Album existingAlbum = _dbContext.AlbumSet.Find(album.Id);
                _dbContext.Entry(existingAlbum).CurrentValues.SetValues(album);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public bool DeleteAlbum(int albumId)
        {
            try
            {
                Album existingAlbum = _dbContext.AlbumSet.Find(albumId);
                _dbContext.AlbumSet.Attach(existingAlbum);
                _dbContext.AlbumSet.Remove(existingAlbum);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public List<Album> GetAllAlbum()
        {
            try
            {
                return (from a in _dbContext.AlbumSet select a).ToList();
            }
            catch (Exception e)
            {
                e.Log();
                return new List<Album>();
            }
        }
    }
}
