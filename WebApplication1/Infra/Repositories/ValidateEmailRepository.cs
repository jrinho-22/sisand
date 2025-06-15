using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Infra.Repositories
{
    public interface IValidateEmailRepository
    {
        Task<bool> RegisterValidateEmail(ValidateEmail validateEmail);

        Task<ValidateEmail> GetValidateByToken(string token);
    }
    public class ValidateEmailRepository(AppDbContext appDbContext) : IValidateEmailRepository
    {
        public async Task<ValidateEmail> GetValidateByToken(string token)
        {
            return await appDbContext.ValidateEmail.FirstOrDefaultAsync(p => p.Token == token);
        }
        async public Task<bool> RegisterValidateEmail(ValidateEmail validateEmail)
        {
            try
            {
                appDbContext.ValidateEmail.Add(validateEmail);
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
