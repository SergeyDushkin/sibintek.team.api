using System;
using System.Linq;
using System.Threading.Tasks;

namespace sibintek.team
{ 
    public interface IConversationRepository
    {
        Task<bool> SetLastMessage(Message message);
        Task<bool> SetConversationStatus(Guid conversationId, string userId, DateTime timestamp);
        Task<Conversation> GetOrCreateDirectConversation(params string[] users);
        IQueryable<Conversation> GetUserConversations(string userId);
    }
}
