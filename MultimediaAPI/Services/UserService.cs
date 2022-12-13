using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultimediaAPI.Models;
using MultimediaAPI.Contexts;
using MultimediaAPI.Extentions;

namespace MultimediaAPI.Services
{
    public interface IUserService
    {
        List<string> CreateUser(User user);
        User GetUser(string userName);
        List<string> UpdateUser(User user);
        List<string> DeleteUser(string userName);
        List<User> GetAllUser();
        List<string> ValidateLogin(string userName, string password);
        int IsRoot(string userName);
    }
    public class UserService : IUserService
    {
        private readonly MultimediaContext _dbContext;
        public UserService(MultimediaContext context)
        {
            _dbContext = context;
        }
        public List<string> CreateUser(User user)
        {
            try
            {
                List<string> msg = ValidateUser(user);
                if (msg.Count == 0)
                {
                    _dbContext.UserSet.Add(user);
                    _dbContext.SaveChanges();
                    return new List<string>();
                }
                else
                    return msg;
            }
            catch (Exception e)
            {
                e.Log();
                return new List<string>() { e.ToString() };
            }
        }
        public User GetUser(string userName)
        {
            try
            {
                User existingUser = _dbContext.UserSet.Find(userName);
                return existingUser;
            }
            catch (Exception e)
            {
                e.Log();
                return new User();
            }
        }
        public List<string> UpdateUser(User user)
        {
            try
            {
                List<string> msg = ValidateUser(user);
                if (msg.Count == 0)
                {
                    User existingUser = _dbContext.UserSet.Find(user.UserName);
                    _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
                    _dbContext.SaveChanges();
                    return new List<string>();
                }
                else
                    return msg;
            }
            catch (Exception e)
            {
                e.Log();
                return new List<string>() { e.ToString() };
            }
        }
        public List<string> DeleteUser(string userName)
        {
            try
            {
                User existingUser = _dbContext.UserSet.Find(userName);
                _dbContext.UserSet.Attach(existingUser);
                _dbContext.UserSet.Remove(existingUser);
                _dbContext.SaveChanges();
                return new List<string>();
            }
            catch (Exception e)
            {
                e.Log();
                return new List<string>() { e.ToString() };
            }
        }
        public List<User> GetAllUser()
        {
            try
            {
                return (from u in _dbContext.UserSet select u).ToList();
            }
            catch (Exception e)
            {
                e.Log();
                return new List<User>();
            }
        }
        public List<string> ValidateLogin(string userName, string password)
        {
            try
            {
                User existingUser = _dbContext.UserSet.Find(userName);
                if (existingUser is null)
                    return new List<string>() { "User not found" };
                if (existingUser.Password != password)
                    return new List<string>() { "Wrong password for user " + existingUser.UserName };
                return new List<string>();
            }
            catch (Exception e)
            {
                e.Log();
                return new List<string>() { e.ToString() };
            }
        }
        public int IsRoot(string userName)
        {
            try
            {
                User existingUser = _dbContext.UserSet.Find(userName);
                if (existingUser is null || existingUser.IsRoot != 1)
                    return 0;
                else
                    return 1;
            }
            catch (Exception e)
            {
                e.Log();
                return 0;
            }
        }
        private List<string> ValidateUser(User user)
        {
            List<string> message = new();
            try
            {
                if (!string.IsNullOrEmpty(user.UserName) || !string.IsNullOrEmpty(user.Password))
                {
                    message.Add("User name / password invalid");
                    return message;
                }
                User existingUser = _dbContext.UserSet.Find(user.UserName);
                if (existingUser is null)
                    return message;
                else
                {
                    message.Add("User existed");
                    return message;
                }
            }
            catch (Exception e)
            {
                e.Log();
                message.Clear();
                message.Add(e.ToString());
                return message;
            }
        }
    }
}
