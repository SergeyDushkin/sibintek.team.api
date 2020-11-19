using System.Threading.Tasks;
using sibintek.db.mongodb;

namespace sibintek.team
{
    public class UserRepository : BaseMongoDbService<User>, IUserRepository
    {
        public UserRepository(IDatabaseSettings settings) : base (settings, "Users")
        {
        }

        public async Task<User> CreateOrUpdateAsync(User user)
        {
            var updated = await GetByAsync(r => r.OuterKey == user.OuterKey);

            if (updated != null) 
            {
                updated.FullName = user.FullName;
                updated.Links = user.Links;

                return await UpdateAsync(updated.Id, updated);
            }

            return await CreateAsync(user);
        }
    }
}
