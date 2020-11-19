using System;
using System.Collections.Generic;
using sibintek.sibmobile.Domain;

namespace sibintek.team
{
    public class Conversation : Base, IWithOuterKey<Guid>
    {
        public string Type { get; set; }
        public List<ConversationUser> Users { get; set; }

        public Message LastMessage { get; set; }
        public int MessagesCount { get; set; }
        public Guid OuterKey { get; set; }
    }
 
    public class ConversationType 
    {
        public const string Direct = "conversation:direct";
        public const string Group = "conversation:group";
        public const string Channel = "conversation:channel";
    }
}
