using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using sibintek.db.mongodb;

namespace sibintek.team
{ 
    public class ConversationRepository : BaseMongoDbService<Conversation>, IConversationRepository
    {
        public ConversationRepository(IDatabaseSettings settings) : base (settings, "Conversations")
        {
        }

        public async Task<bool> SetLastMessage(Message message) 
        {
            var filter = Builders<Conversation>.Filter.Eq(r => r.OuterKey, message.ConversationId);
            var update = Builders<Conversation>.Update
                .Set(r => r.LastMessage, message)
                .Inc(r => r.MessagesCount, 1)
                .CurrentDate(r => r.Timestamp);

            try
            {
                var updateResult = await collection.UpdateOneAsync(filter, update);

                return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SetConversationStatus(Guid conversationId, string userId, DateTime timestamp) 
        {
            var conversationFilter = Builders<Conversation>.Filter.Eq(r => r.OuterKey, conversationId);
            var userFilter = Builders<Conversation>.Filter.ElemMatch(r => r.Users, r => r.UserId == userId);
            var filter = Builders<Conversation>.Filter.And(conversationFilter, userFilter);

            var update = Builders<Conversation>.Update
                .Set(r => r.Users[-1].Read, true)
                .Set(r => r.Users[-1].LastReadTime, timestamp);

            try
            {
                var updateResult = await collection.UpdateOneAsync(filter, update);

                return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Conversation> GetOrCreateDirectConversation(params string[] users)
        {
            var u1 = users[0];
            var u2 = users[1];
            
            var conversation = Get(r => r.Type == ConversationType.Direct)
                .Where(r => r.Users.Any(n => n.UserId == u1))
                .Where(r => r.Users.Any(n => n.UserId == u2))
                .SingleOrDefault();
                
            //var direct = Get(r => r.Type == ConversationType.Direct);
            //var conversation = direct.Where(r => r.Users.All(n => users.Contains(n.UserId))).FirstOrDefault();

            if (conversation == null)
            {
                var create = new Conversation 
                {
                    OuterKey = Guid.NewGuid(),
                    Type = ConversationType.Direct,
                    MessagesCount = 0,
                    Users = users.Select(r => new ConversationUser { UserId = r }).ToList()
                };

                conversation = await CreateAsync(create);
            }

            return conversation;
        }
        
        public IQueryable<Conversation> GetUserConversations(string userId)
        {
            var conversations = Get(r => r.Users.Any(n => n.UserId == userId))
                .OrderByDescending(r => r.Timestamp);

            return conversations;
        }
    }
}
