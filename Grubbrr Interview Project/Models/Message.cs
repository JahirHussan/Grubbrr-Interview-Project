using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grubbrr_Interview_Project.Models
{
    public class Message
    {
        public MessageType MessageType { get; set; }
        public string Description { get; set; }
    }

    public enum MessageType
    {
        Success = 0,
        Error = 1,
    }
}