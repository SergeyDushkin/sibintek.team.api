using System;

namespace sibintek.team
{
    public class ConversationUser
    {
        public string UserId { get; set; }
        public bool Read { get; set; }
        public DateTime LastReadTime { get; set; }
        public bool isAdmin { get; set; }
    }
}
