using Microsoft.EntityFrameworkCore;
using MySocialMedia.Common.DBTabales;
using MySocialMedia.Common.DTOs;
using MySocialMedia.Common.ResponseLogin;
using MySocialMedia.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Logic.Services
{
    public interface IUserService
    {
        public UserSessionDTO Login(string username, string password); 
        public void Signin(string firstName, string lastName , string username, string password);
        public List<LimitedUsers> GetUsers(long p_userId);
        public void LogOut(long p_userId);
    }
    public class UserService: IUserService
    {
        private readonly IDbContext _db;
        public UserService()
        {
            _db = new SMDbContext();
        }
        public UserSessionDTO Login(string username, string password)
        {
            var u = _db.Connect(db => db.users.FirstOrDefault(x => x.USER_NAME == username && x.PASSWORD == password));
            if (u == null)
            {
                Console.WriteLine("NO EXIST");
                return null;
            }
            var us = _db.Connect(db => db.user_sessions.OrderByDescending(x => x.DATE_CREATE).FirstOrDefault(x => x.USER_ID == u.ID && !x.IS_DELETE));
            if (us == null)
            {
                us = new user_session
                {
                    USER_ID = u.ID,
                    TOKEN = Guid.NewGuid().ToString(),
                    DATE_CREATE = DateTime.Now,
                };
                _db.Connect(db => db.user_sessions.Add(us), true);
               
            }
            Console.WriteLine("Sucsses");
            return UserSessionDTO.Parse(us);
        }
        public void LogOut(long p_userId)
        {
            _db.Connect(db =>
            {
                foreach (var item in db.user_sessions.Where(x => x.USER_ID == p_userId))
                {
                    item.IS_DELETE = true;
                }
            }, true);
        }
        public void Signin(string firstName , string lastName , string userName , string password)
        {
            var u = _db.Connect(db => db.users.FirstOrDefault(x => x.USER_NAME == userName));
            if (u != null)
            {
                Console.WriteLine("The user name alredy exist");
            }
            else
            {
                List<string> valid = Validation.ValidationAll(firstName, lastName, userName, password);
                if (valid.Count == 0)
                {
                    var newUs = new users
                    {
                        FIRST_NAME = firstName,
                        LAST_NAME = lastName,
                        USER_NAME = userName,
                        PASSWORD = password,
                        DATE_CREATE= DateTime.Now,
                    };
                    _db.Connect(y => y.users.Add(newUs) ,true);
                    Console.WriteLine("Register complite");
                }
                else
                {
                    foreach (var item in valid)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("not complite");
                }
            }
        }
        public List<LimitedUsers> GetUsers(long p_userId)
        {
            return _db.Connect(db =>
            {
                var uid = db.user_messages.Where(x => x.RECEIVER_USER_ID == p_userId || x.SENDER_USER_ID == p_userId)
                .SelectMany(x => new[] {x.RECEIVER_USER_ID , x.SENDER_USER_ID})
                .Distinct().ToList();
                return db.users.Where(x => uid.Contains(x.ID)).Select(x => new LimitedUsers
                {
                    Id = x.ID,
                    FullName = $"{x.FIRST_NAME}{x.LAST_NAME}",
                    Username = x.USER_NAME
                }).ToList();
            });
        } 
    }
    public class LimitedUsers
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
    }

}
