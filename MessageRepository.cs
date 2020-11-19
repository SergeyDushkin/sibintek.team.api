using System;
using System.Linq;
using System.Threading.Tasks;
using sibintek.db.mongodb;

namespace sibintek.team
{
    public class MessageRepository : BaseMongoDbService<Message>, IMessageRepository
    {
        public MessageRepository(IDatabaseSettings settings) : base (settings, "Messages")
        {
        }

        public async Task<Message> CreateMessage(Message message)
        {
            message.OuterKey = Guid.NewGuid();
            return await base.CreateAsync(message);
        }

        public IQueryable<Message> GetMessages(Guid conversationId)
        {
            return Get(r => r.ConversationId == conversationId);
        }
        public IQueryable<Message> GetMessages(Guid[] conversationId)
        {
            return Get(r => conversationId.Contains(r.ConversationId));
        }
    }
}
