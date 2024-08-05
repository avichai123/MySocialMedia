using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Common.DBTabales
{
    [Table(nameof(user_session))]
    public class user_session
    {
        public long ID { get; set; }    
        public string? TOKEN { get; set; }
        public long USER_ID { get; set; }   
        public DateTime DATE_CREATE { get; set; }  
        public virtual users users { get; set; }    
    }
}
