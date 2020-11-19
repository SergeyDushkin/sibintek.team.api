using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace sibintek.team.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseConversationController : ControllerBase
    {
        public readonly ILogger<ChannelController> logger;
        public readonly ConversationService service;
        public readonly IMapper mapper;
        public string userId { get { return this.User.Identity.Name; } }

        public BaseConversationController(ILogger<ChannelController> _logger, IMapper _mapper, ConversationService _service)
        {
            this.logger = _logger;
            this.service = _service;
            this.mapper = _mapper;
        }
    }
}
