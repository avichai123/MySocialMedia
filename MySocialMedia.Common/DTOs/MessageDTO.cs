using MySocialMedia.Common.DBTabales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Common.DTOs
{
    public class MessageDTO
    {
        public long Id { get; set; }    
        public string MessageData {  get; set; }    
        public DateTime MessageDate { get; set; }
        public long SenderId {  get; set; }
        public long ReciverId {  get; set; }    
        public user_messages Parse()
        {
            return new user_messages
            {
                ID = Id,
                MESSAGE_DATA = MessageData,
                MESSAGE_DATE = MessageDate,
                SENDER_USER_ID = SenderId,
                RECEIVER_USER_ID = ReciverId,
            };
        }
    }
}
