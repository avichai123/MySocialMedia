using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Common.DBTabales
{
    [Table(nameof(users))]
    public class users
    {
        public long ID { get; set; }
        public string? FIRST_NAME { get; set; }
        public string? LAST_NAME { get; set; }
        public string? USER_NAME { get; set; }
        public string? PASSWORD { get; set; }
        public DateTime DATE_CREATE { get; set; }
        public virtual ICollection<user_messages>? send_user_messages { get; set; }
        public virtual ICollection<user_messages>? receive_user_messages { get; set; }
        public virtual ICollection<user_session>? user_sessions {  get; set; }
    }
}
