using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sibintek.team.dto;
using sibintek.http.context;
using sibintek.team.mapping;
using System.Linq;

namespace sibintek.team.api.Controllers
{
    public class MessagesController : BaseConversationController
    {
        public MessagesController(ILogger<ChannelController> logger, IMapper mapper, ConversationService service) 
            : base(logger, mapper, service)
        {

        }

        [HttpGet("{id}")]
        public IEnumerable<MessageDto> Get(Guid id)
        {
            var messages = service.GetMessages(userId, id)
                .UseODataFilter(this.HttpContext)
                .OrderByDescending(r => r.Timestamp)
                .UseODataPagination(this.HttpContext)
                .Map<Message, MessageDto>(mapper);

            return messages;
        }
    }
}
