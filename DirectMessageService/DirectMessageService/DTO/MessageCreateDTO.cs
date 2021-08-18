using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.DTO
{
    public class MessageCreateDTO
    {
        public int ReceiverID { get; set; }
        public String Body { get; set; }
    }
}
