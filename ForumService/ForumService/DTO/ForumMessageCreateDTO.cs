using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.DTO
{
    public class ForumMessageCreateDTO
    {
        public int ForumID { get; set; }
        public int SenderID { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
    }
}
