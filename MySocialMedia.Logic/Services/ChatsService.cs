using MySocialMedia.Common.DBTabales;
using MySocialMedia.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Logic.Services
{
    public interface IChatsService
    {
        public void AddMessage(MessageDTO p_mess);
        public List<MessageDTO> GetAllByUser(long p_userId);
    }
    public class ChatsService : IChatsService 
    {
        private readonly IDbContext _db;
        public ChatsService()
        {
            _db = new SMDbContext();
        }
        public void AddMessage(MessageDTO p_mess)
        {
            if(p_mess.Id != default)
            {
                throw new Exception("Send to update no add");
            }
            var mess = p_mess.Parse();
            _db.Connect(db => db.user_messages.Add(mess),true);
        }
        public List<MessageDTO> GetAllByUser(long p_userId)
        {
            return _db.Connect(db =>
            {
                return db.user_messages.Where(x => x.SENDER_USER_ID == p_userId || x.RECEIVER_USER_ID == p_userId).Select(
                    x => new MessageDTO
                    {
                        Id = x.ID,
                        SenderId = x.SENDER_USER_ID,
                        ReciverId = x.RECEIVER_USER_ID,
                        MessageData = x.MESSAGE_DATA,
                        MessageDate = x.MESSAGE_DATE,
                    }).ToList();
            });
        }
    }
}
