using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts.User;
using WebApplication1.Models;

namespace WebApplication1.Infra.Repositories
{
    public interface IUserRepository
    {
        Task<bool> DeleteByUserId(long userId);
        Task<UserDto[]> GetAllUsers();
        Task<User> GetUserById(long userId);
        Task<bool> RegisterUser(User user);
    }
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        public async Task<bool> DeleteByUserId(long userId)
        {
            var user = await appDbContext.User
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User not found");
            }

            appDbContext.User.Remove(user);
            await appDbContext.SaveChangesAsync();

            return true;
        }
        async public Task<UserDto[]> GetAllUsers()
        {
            return await appDbContext.User
                .Include(x => x.Login)
                .Select(x => new UserDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Cep= x.Cep,
                    Email = x.Login.Email
                }).ToArrayAsync();
        }
        async public Task<User> GetUserById(long userId)
        {
            return await appDbContext.User.FirstOrDefaultAsync(x => x.Id == userId);
        }

        async public Task<bool> RegisterUser(User user)
        {
            try
            {
                appDbContext.User.Add(user);

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
