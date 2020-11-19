using System;
using sibintek.sibmobile.Domain;

namespace sibintek.team
{
    public class Message : Base, IWithOuterKey<Guid>
    {
        public Guid OuterKey { get; set; }
        public Guid ConversationId { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
    }
    
    public class MessageType 
    {
        public const string Text = "message:text";
        public const string Markdown = "message:markdown";
        public const string Image = "message:image";
    }
}
