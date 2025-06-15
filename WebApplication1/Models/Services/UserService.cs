using WebApplication1.Contracts.User;
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

            //var transaction = appDbContext.Database.BeginTransaction();

            try
            {
                await userRepository.RegisterUser(user);
                await loginRepository.RegisterLogin(login);

                await appDbContext.SaveChangesAsync();
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
