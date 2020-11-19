using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace sibintek.team.dto
{
    public class ConversationDto : ConversationHeaderDto
    {
        public List<ConversationUserDto> Users { get; set; }
    }
}
