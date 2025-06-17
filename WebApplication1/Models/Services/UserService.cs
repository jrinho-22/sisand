using System.Text;
using WebApplication1.Contracts.User;
using WebApplication1.Core;
using WebApplication1.Infra;
using WebApplication1.Infra.Repositories;

namespace WebApplication1.Models.Services
{
    public interface IUserService
    {
        Task<User> GetUserDataById(long userId);
        Task<User> RegisterUser(RegisterViewModel registerViewModel);
        Task<UserDto[]> GetAllUsers();
        Task<User> UpdateUser(EditViewModel editViewModel, long userId);
        Task<bool> DeleteUserById(long userId);
    }
    public class UserService(
        IUserRepository userRepository,
        ILoginRepository loginRepository,
        IEmailService emailService,
        IValidateEmailRepository validateEmailRepository,
        AppDbContext appDbContext) : IUserService
    {
        public async Task<bool> DeleteUserById(long userId)
        {
            return await userRepository.DeleteByUserId(userId);
        }
        public async Task<UserDto[]> GetAllUsers()
        {
            return await userRepository.GetAllUsers();
        }
        public  async Task<User> GetUserDataById(long userId)
        {
            return await userRepository.GetUserById(userId);
        }

        public async Task<User> UpdateUser(EditViewModel editViewModel, long userId)
        {
            try
            {
                var user = await userRepository.GetUserById(userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                user.ChangeUserData(
                    editViewModel.Name,
                    editViewModel.Phone,
                    editViewModel.Cep);
                user.IsValid();

                await appDbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        async public Task<User> RegisterUser(RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                Name = registerViewModel.Name,
                Phone = registerViewModel.Phone,
                Cep = registerViewModel.Cep,
            };
            user.IsValid();

            var login = new Login
            {
                Email = registerViewModel.Email,
                PasswordHash = PasswordHashService.HashPassword(registerViewModel.Password),
                User = user
            };
            login.IsValid();

            var token = Guid.NewGuid().ToString("N");
            var validate = new ValidateEmail(registerViewModel.Email, token);
            validate.Login = login;

            try
            {
                await userRepository.RegisterUser(user);
                await loginRepository.RegisterLogin(login);
                await validateEmailRepository.RegisterValidateEmail(validate);

                await appDbContext.SaveChangesAsync();

                var link = $"<a href=\"https://sisand-app-cvghg2hxe6djamh2.canadacentral-01.azurewebsites.net/ValidateEmail?token={token}\">Confirmar Email</a>";
                emailService.SendEmailAsync(registerViewModel.Email, "Validação de Email", "", link);

                return user;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
