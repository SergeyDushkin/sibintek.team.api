using System;
using System.Threading.Tasks;
using sibintek.sibmobile.core;

namespace sibintek.team
{
    public class SetMessageReadCommand : BaseCommand
    {
        public Guid ConversationId { get; set; }
    }

    [CommandHandler("conversation:message-read")]
    public class SetMessageReadCommandHandler : ICommandHandler<SetMessageReadCommand>
    {
        private ConversationService _conversationService;

        public SetMessageReadCommandHandler(ConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        public async Task Handle(SetMessageReadCommand command)
        {
            await _conversationService.SetReadStatus(command.ConversationId, command.Identity.Name);
        }
    }
}