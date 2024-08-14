using MySocialMedia.Common.DBTabales;
using MySocialMedia.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Common.ResponseLogin
{
    public class ResponseLogin
    {
        public List<UsersDTO> chats {  get; set; }
        public UserSessionDTO userSession { get; set;}
        public ResponseLogin(List<UsersDTO> chats, UserSessionDTO userSession)
        {
            this.chats = chats;
            this.userSession = userSession;
        }
    }
}
