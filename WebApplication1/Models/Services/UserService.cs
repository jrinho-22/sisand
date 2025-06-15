using System.Text;
using WebApplication1.Contracts.User;
using WebApplication1.Core;
using WebApplication1.Infra;
using WebApplication1.Infra.Repositories;

namespace WebApplication1.Models.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Method responsible for retriving a user data
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetUserDataById(long userId);

        /// <summary>
        /// Method responsible for registering a user
        /// </summary>

        /// <returns></returns>
        Task<User> RegisterUser(RegisterViewModel registerViewModel);
    }
    public class UserService(
        IUserRepository userRepository,
        ILoginRepository loginRepository,
        IEmailService emailService,
        IValidateEmailRepository validateEmailRepository,
        AppDbContext appDbContext) : IUserService
    {
        async public Task<User> GetUserDataById(long userId)
        {
            return await userRepository.GetUserDataById(userId);
        }

        async public Task<User> RegisterUser(RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                Name = registerViewModel.Name,
                Phone = registerViewModel.Phone,
                Cep = registerViewModel.Cep,
                Logradouro = registerViewModel.Logradouro,
                Numero = registerViewModel.Numero,
                Complemento = registerViewModel.Complemento,
                Bairro = registerViewModel.Bairro,
                Cidade = registerViewModel.Cidade
            };
            user.IsValid();

            var login = new Login
            {
                Email = registerViewModel.Email,
                PasswordHash = registerViewModel.Password,
                User = user
            };
            login.IsValid();

            var token = Guid.NewGuid().ToString("N");
            var validate = new ValidateEmail(registerViewModel.Email, token);
            validate.Login = login;
            //var transaction = appDbContext.Database.BeginTransaction();

            try
            {
                await userRepository.RegisterUser(user);
                await loginRepository.RegisterLogin(login);
                await validateEmailRepository.RegisterValidateEmail(validate);

                await appDbContext.SaveChangesAsync();

                var link = $"<a href=\"https://sisand-app-cvghg2hxe6djamh2.canadacentral-01.azurewebsites.net/ValidateEmail?token={token}\">Confirmar Email</a>";
                emailService.SendEmailAsync("jrinho22@gmail.com", "Validação de Email", "", link);
                //await transaction.CommitAsync();

                return user;
            }
            catch (Exception e)
            {
                //await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
