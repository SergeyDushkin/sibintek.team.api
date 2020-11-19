using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace sibintek.team
{ 
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task<User> CreateOrUpdateAsync(User user)
        {
            return await userRepository.CreateOrUpdateAsync(user);
        }

        public IQueryable<User> Get(Expression<Func<User, bool>> filter)
        {
            return userRepository.Get(filter);
        }
    }
}
