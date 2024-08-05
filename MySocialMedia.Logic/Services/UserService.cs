using MySocialMedia.Common.DBTabales;
using MySocialMedia.Common.DTOs;
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
            if(u == null)
            {
                throw new Exception("NO EXIST");
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
            return UserSessionDTO.Parse(us);    
        }
    }
}
