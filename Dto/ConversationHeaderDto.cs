using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace sibintek.team.dto
{
    public class ConversationHeaderDto
    {
        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Text { get; set; }
        public MessageDto LastMessage { get; set; }
        public int UnreadCount { get; set; }
        public IconDto Icon { get; set; }
    }
}
