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
        public ResponseLogin Login(string username, string password); 
        public void Signin(string firstName, string lastName , string username, string password);
    }
    public class UserService: IUserService
    {
        private readonly IDbContext _db;
        public UserService()
        {
            _db = new SMDbContext();
        }
        public ResponseLogin Login(string username, string password)
        {
            var u = _db.Connect(db => db.users.FirstOrDefault(x => x.USER_NAME == username && x.PASSWORD == password));
            if(u == null)
            {
                Console.WriteLine("NO EXIST");
                return null;
            }
            var us = _db.Connect(db => db.user_sessions.OrderByDescending(x => x.DATE_CREATE).FirstOrDefault(x=> x.USER_ID == u.ID));
            if(us == null)
            {
                us = new user_session
                {
                    USER_ID = u.ID,
                    TOKEN = Guid.NewGuid().ToString(),
                    DATE_CREATE = DateTime.Now,
                };
                _db.Connect(db => db.user_sessions.Add(us), true);
            }
             UserSessionDTO userSessionDTO = UserSessionDTO.Parse(us);  
             List<UsersDTO> data = GetUniqueConvertionPartners(us.USER_ID);
            //foreach(var partner in data)
            //{
            //    Console.WriteLine(partner.ID + " " + partner.FIRST_NAME + " " + partner.LAST_NAME);
            //}
            //return UserSessionDTO.Parse(us);
            return new ResponseLogin(data, userSessionDTO);
        }
        public List<UsersDTO> GetUniqueConvertionPartners(long id)
        {
            var usersMas = _db.Connect(db => db.user_messages.Where(x => x.SENDER_USER_ID == id || x.RECEIVER_USER_ID == id)
           .Select(x => x.SENDER_USER_ID == id ? x.RECEIVER_USER_ID : x.SENDER_USER_ID)
           .Distinct().ToList());
            List<UsersDTO> chats = new List<UsersDTO>();
            usersMas.ForEach(usersMas =>
            {
                var u = _db.Connect(db => db.users.FirstOrDefault(x => x.ID == usersMas));
                chats.Add(new UsersDTO
                {
                    Id = u.ID,
                    FirstName = u.FIRST_NAME,
                    LastName = u.LAST_NAME,
                });
            });
            return chats;   
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
    }
}
