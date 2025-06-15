using System;
using WebApplication1.Models;

namespace WebApplication1.Infra.Repositories
{
    public interface ILoginRepository
    {
        Task<bool> RegisterLogin(Login login);
    }
    public class LoginRepository(AppDbContext appDbContext) : ILoginRepository
    {
        public async Task<bool> RegisterLogin(Login login)
        {
            //var transaction = appDbContext.Database.BeginTransaction();

            try
            {
                appDbContext.Login.Add(login);
                //await appDbContext.SaveChangesAsync();
                //await transaction.CommitAsync();

                //return login;
                return true;
            }
            catch (Exception e)
            {
                //await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
