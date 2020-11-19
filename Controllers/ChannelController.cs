using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sibintek.team.dto;
using sibintek.http.context;
using sibintek.team.mapping;
using System.Collections.Generic;

namespace sibintek.team.api.Controllers
{
    public class ChannelController : BaseConversationController
    {
        private readonly UserService userService;

        public ChannelController(ILogger<ChannelController> logger, IMapper mapper, 
            ConversationService service, UserService userService) 
            : base(logger, mapper, service)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IEnumerable<ConversationHeaderDto> Get()
        {
            /// TODO перенести вычисления в репозиторий на уровень БД 
            Func<ConversationDto, DateTime> getLastReadTime = (x) 
                => x.Users.Where(u => u.UserId == userId).FirstOrDefault().LastReadTime;

            Func<ConversationDto, string> getRecipientId = (x) 
                => x.Users.Where(u => u.UserId != userId).FirstOrDefault().UserId;
            
            var conversations = service.GetConversations(userId)
                .UseODataFilter(this.HttpContext)
                .OrderByDescending(r => r.Timestamp)
                .UseODataPagination(this.HttpContext)
                .Map<Conversation, ConversationDto>(mapper);

            var conversationHeaders = conversations.Select(r => {
                r.UnreadCount = service.GetMessages(userId, r.Id).Where(m => m.Timestamp >= getLastReadTime(r)).Count();
                return r;
            });

            var usersId = conversationHeaders.SelectMany(r => r.Users.Select(u => u.UserId)).Distinct();
            var users = userService.Get(r => usersId.Contains(r.OuterKey)).ToList();

            conversationHeaders = conversationHeaders.Select(r => {
                var recipient = users.Where(u => getRecipientId(r) == u.OuterKey).FirstOrDefault();
                r.Text = recipient?.FullName;
                r.Icon = new IconDto { Src = recipient?.Links.Thumbnail.ToString() };
                return r;
            });

            return conversationHeaders.Map<ConversationDto, ConversationHeaderDto>(mapper);
        }
    }
}
