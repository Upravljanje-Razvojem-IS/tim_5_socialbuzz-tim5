using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.DTO
{
    public class MessageDTO
    {
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public String Body { get; set; }
        public bool IsSent { get; set; }
    }
}
