using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Entity
{
    /// <summary>
    /// Model poruke
    /// </summary>
    public class Message
    {
        /// <summary>
        /// ID poruke
        /// </summary>
        public int MessageID { get; set; }

        /// <summary>
        /// ID posiljaoca
        /// </summary>
        public int SenderID { get; set; }

        /// <summary>
        /// ID primaoca
        /// </summary>
        public int ReceiverID { get; set; }

        /// <summary>
        /// Tekst
        /// </summary>
        public String Body { get; set; }

        /// <summary>
        /// Poruka je poslata ili nije
        /// </summary>
        public bool IsSent { get; set; }


    }
}
