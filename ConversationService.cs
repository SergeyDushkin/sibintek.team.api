using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using sibintek.sibmobile.core;

namespace sibintek.team
{
    public class ConversationService
    {
        private readonly IConversationRepository conversationRepository;
        private readonly IMessageRepository messageRepository;

        public ConversationService(IConversationRepository _conversationRepository, IMessageRepository _messageRepository)
        {
            conversationRepository = _conversationRepository;
            messageRepository = _messageRepository;
        }

        public async Task<bool> Send(Conversation conversation, Message message)
        {
            message.ConversationId  = conversation.OuterKey;

            await messageRepository.CreateMessage(message);
            await conversationRepository.SetLastMessage(message);

            return true;
        }

        public async Task<bool> SendTo(string userId, string recipientId, Message message)
        {
            var users = new string[] { userId, recipientId };
            var direct = await conversationRepository.GetOrCreateDirectConversation(users);

            return await Send(direct, message);
        }

        public IQueryable<Conversation> GetConversations(string userId)
        {
            return conversationRepository.GetUserConversations(userId);
        }

        public Conversation GetConversation(string userId, Guid conversationId)
        {
            var conversation = conversationRepository
                .GetUserConversations(userId)
                .SingleOrDefault(r => r.OuterKey == conversationId);
            
            return conversation;
        }
        public IQueryable<Message> GetMessages(string userId, Guid conversationId)
        {
            var conversation = conversationRepository
                .GetUserConversations(userId)
                .SingleOrDefault(r => r.OuterKey == conversationId);
            
            return messageRepository.GetMessages(conversationId);
        }

        public async Task<bool> SetReadStatus(Guid conversationId, string userId)
        {
            return await conversationRepository.SetConversationStatus(conversationId, userId, DateTime.Now);
        }

        public IQueryable<Message> Search(string userId, string query)
        {
            var conversations = conversationRepository
                .GetUserConversations(userId)
                .Select(r => r.OuterKey)
                .ToArray();
                
            return messageRepository
                .GetMessages(conversations)
                .Search(query);
        }
    }
}
