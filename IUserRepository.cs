using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace sibintek.team
{ 
    public interface IUserRepository
    {
        IQueryable<User> Get(Expression<Func<User, bool>> filter);
        Task<User> CreateOrUpdateAsync(User user);
    }
}
