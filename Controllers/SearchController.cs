using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sibintek.team.dto;
using sibintek.http.context;
using sibintek.team.mapping;

namespace sibintek.team.api.Controllers
{
    public class SearchController : BaseConversationController
    {
        public SearchController(ILogger<ChannelController> logger, IMapper mapper, ConversationService service) 
            : base(logger, mapper, service)
        {
        }

        [HttpGet()]
        public IEnumerable<MessageDto> Get(string query)
        {
            var messages = service.Search(userId, query)
                .UseODataFilter(this.HttpContext)
                .OrderByDescending(r => r.Timestamp)
                .UseODataPagination(this.HttpContext)
                .Map<Message, MessageDto>(mapper);
            
            return messages;
        }
    }
}
