using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Common.DBTabales
{
    [Table(nameof(user_messages))]
    public class user_messages
    {
        public long ID { get; set; }
        public long SENDER_USER_ID { get; set; }
        public long RECEIVER_USER_ID { get; set; }
        public string? MESSAGE_DATA { get; set; }
        public DateTime MESSAGE_DATE { get; set; }
        public virtual users? send_user { get; set; }
        public virtual users? receive_user { get; set; }
    }
}
