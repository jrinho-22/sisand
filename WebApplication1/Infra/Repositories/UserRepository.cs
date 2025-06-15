using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts.User;
using WebApplication1.Models;

namespace WebApplication1.Infra.Repositories
{
    public interface IUserRepository
    {
        Task<Models.User> GetUserDataById(long userId);

        Task<bool> RegisterUser(User user);
    }
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        async public Task<User> GetUserDataById(long userId)
        {
            return await appDbContext.User.FirstOrDefaultAsync(x => x.Id == userId);
        }

        async public Task<bool> RegisterUser(User user)
        {
            //var transaction = appDbContext.Database.BeginTransaction();

            try
            {
                appDbContext.User.Add(user);
                //    await appDbContext.SaveChangesAsync();
                //    await transaction.CommitAsync();

                return true;
            }
            catch (Exception e)
            {
                //    await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
