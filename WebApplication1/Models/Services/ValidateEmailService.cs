using WebApplication1.Infra;
using WebApplication1.Infra.Repositories;

namespace WebApplication1.Models.Services
{
    public interface IValidateEmailService
    {
        Task<bool> ValidateLoginByToken(string token);
    }
    public class ValidateEmailService(
        IValidateEmailRepository validateEmailRepository,
        ILoginRepository loginRepository,
        AppDbContext appDbContext) : IValidateEmailService
    {
        public async Task<bool> ValidateLoginByToken(string token)
        {

            var validate = await validateEmailRepository.GetValidateByToken(token);

            if (validate == null) {
                throw new InvalidOperationException();
            }

            var login = await loginRepository.GetLoginById(validate.LoginId);
            login.EmailVerified = true;

            await appDbContext.SaveChangesAsync();

            return true;

        }
    }
}
