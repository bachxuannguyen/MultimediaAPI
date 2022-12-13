using MultimediaAPI.Contexts;
using MultimediaAPI.Extentions;
using MultimediaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultimediaAPI.Services
{
    public interface IRelationService
    {
        bool CreateRelation(Relationship relationship);
        List<int> GetAlbumIdByMediaId(int mediaId);
        List<int> GetMediaIdByAlbumId(int albumId);
        bool UpdateRelation(Relationship relationship);
        bool DeleteRelation(int relationId);
        bool DeleteRelation(int mediaId, int albumId);
        bool DeleteRelationByMediaId(int mediaId);
        bool DeleteRelationByAlbumId(int albumId);
    }
    public class RelationService : IRelationService
    {
        private readonly MultimediaContext _dbContext;
        public RelationService(MultimediaContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateRelation(Relationship relationship)
        {
            try
            {
                _dbContext.RelationSet.Add(relationship);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return true;
            }
        }
        public List<int> GetAlbumIdByMediaId(int mediaId)
        {
            try
            {
                return (from r in _dbContext.RelationSet where r.MediaId == mediaId select r.AlbumId).Distinct().ToList();
            }
            catch (Exception e)
            {
                e.Log();
                return new List<int>();
            }
        }
        public List<int> GetMediaIdByAlbumId(int albumId)
        {
            try
            {
                return (from r in _dbContext.RelationSet where r.AlbumId == albumId select r.MediaId).Distinct().ToList();
            }
            catch (Exception e)
            {
                e.Log();
                return new List<int>();
            }
        }
        public bool UpdateRelation(Relationship relationship)
        {
            try
            {
                Relationship existingRelation = _dbContext.RelationSet.Find(relationship.Id);
                _dbContext.Entry(existingRelation).CurrentValues.SetValues(relationship);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public bool DeleteRelation(int relationId)
        {
            try
            {
                Relationship existingRelation = _dbContext.RelationSet.Find(relationId);
                _dbContext.RelationSet.Attach(existingRelation);
                _dbContext.RelationSet.Remove(existingRelation);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return false;
            }
        }
        public bool DeleteRelation(int mediaId, int albumId)
        {
            try
            {
                List<Relationship> relationList = (from r in _dbContext.RelationSet
                                                   where (r.MediaId == mediaId && r.AlbumId == albumId)
                                                   select r).ToList();
                foreach (Relationship r in relationList)
                {
                    _dbContext.RelationSet.Attach(r);
                    _dbContext.RelationSet.Remove(r);
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
        public bool DeleteRelationByMediaId(int mediaId)
        {
            try
            {
                List<Relationship> relationList = (from r in _dbContext.RelationSet where r.MediaId == mediaId select r).ToList();
                foreach (Relationship r in relationList)
                {
                    _dbContext.Attach(r);
                    _dbContext.Remove(r);
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return true;
            }
        }
        public bool DeleteRelationByAlbumId(int albumId)
        {
            try
            {
                List<Relationship> relationList = (from r in _dbContext.RelationSet where r.AlbumId == albumId select r).ToList();
                foreach (Relationship r in relationList)
                {
                    _dbContext.Attach(r);
                    _dbContext.Remove(r);
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Log();
                return true;
            }
        }
    }
}
