using WebApplication1.Contracts.Login;
using WebApplication1.Infra.Repositories;

namespace WebApplication1.Models.Services
{
    public interface ILoginService
    {
        Task<UserSessionDto> GetLoginById(long loginId);
        Task<UserSessionDto> GetUserSession(LoginViewModel loginViewModel);
    }
    public class LoginService(ILoginRepository loginRepository) : ILoginService
    {
        public async Task<UserSessionDto> GetLoginById(long loginId)
        {
            var login = await loginRepository.GetLoginById(loginId);
            if (login == null)
            {
                throw new Exception("Login not found");
            }

            return login;
        }
        public async Task<UserSessionDto> GetUserSession(LoginViewModel loginViewModel)
        {
            try
            {
                var login = await loginRepository.GetUserSession(loginViewModel);
                if (login == null)
                {
                    throw new Exception("Login not found");
                }
                return login;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
