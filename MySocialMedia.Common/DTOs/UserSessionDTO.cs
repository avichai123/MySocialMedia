using MySocialMedia.Common.DBTabales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Common.DTOs
{
    public class UserSessionDTO
    {
        public long Id { get; set; }
        public string? Token { get; set; }
        public long UserId { get; set; }
        public DateTime DateCreate { get; set; }
        public static UserSessionDTO Parse(user_session p_us)
        {
            return new UserSessionDTO
            {
                Id = p_us.ID,
                Token = p_us.TOKEN,
                UserId = p_us.USER_ID,
                DateCreate = p_us.DATE_CREATE,
            };
        }
    }
}
