using System;
using Newtonsoft.Json;

namespace sibintek.team.dto
{
    public class MessageDto
    {
        [JsonProperty("_id")]
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; } 
    }
}
