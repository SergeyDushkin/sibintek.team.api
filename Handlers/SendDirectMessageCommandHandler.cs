using System;
using System.Threading.Tasks;
using sibintek.sibmobile.core;

namespace sibintek.team
{
    public class SendDirectMessageCommand : BaseCommand
    {
        public string RecipientId { get; set; }
        public string Text { get; set; }
    }

    [CommandHandler("conversation:message-send-direct")]
    public class SendDirectMessageCommandHandler : ICommandHandler<SendDirectMessageCommand>
    {
        private ConversationService _conversationService;

        public SendDirectMessageCommandHandler(ConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        public async Task Handle(SendDirectMessageCommand command)
        {
            var message = new Message 
            { 
                Type = MessageType.Text, 
                UserId = command.Identity.Name, 
                Text = command.Text, 
                Timestamp = DateTime.Now.ToUniversalTime() 
            };

            await _conversationService.SendTo(command.Identity.Name, command.RecipientId, message);
        }
    }
}