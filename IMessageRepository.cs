using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace sibintek.team
{ 
    public interface IMessageRepository
    {
        Task<Message> CreateMessage(Message message);
        IQueryable<Message> GetMessages(Guid conversationId);
        IQueryable<Message> GetMessages(Guid[] conversationId);
    }
}
