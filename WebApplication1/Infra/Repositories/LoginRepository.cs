using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts.Login;
using WebApplication1.Core;
using WebApplication1.Models;

namespace WebApplication1.Infra.Repositories
{
    public interface ILoginRepository
    {
        Task<bool> RegisterLogin(Login login);

        Task<UserSessionDto> GetUserSession(LoginViewModel loginViewModel);
        Task<UserSessionDto> GetLoginById(long loginId);
    }
    public class LoginRepository(AppDbContext appDbContext) : ILoginRepository
    {
        public async Task<UserSessionDto> GetLoginById(long loginId)
        {
            return await appDbContext.Login
                .Where(l => l.Id == loginId)
                .Select(x => new UserSessionDto()
                {
                    Id = x.Id,
                    Email = x.Email,
                    EmailVerified = x.EmailVerified,
                }).FirstOrDefaultAsync();
        }
        public async Task<UserSessionDto> GetUserSession(LoginViewModel loginViewModel)
        {
            var loginByEmail = await appDbContext.Login
                .Where(x => x.Email == loginViewModel.Email)
                .FirstOrDefaultAsync();
            if (loginByEmail == null)
            {
                throw new Exception("Email Not Found");
            }
            var validPassword = PasswordHashService.ValidatePassword(loginViewModel.Password, loginByEmail.PasswordHash);

            if (validPassword == false)
            {
                throw new Exception("Password Incorrect");
            }
            return await appDbContext.Login
            .Where(x => x.Email == loginViewModel.Email)
            .Select(x => new UserSessionDto()
            {
                Id = x.Id,
                Email = x.Email,
                EmailVerified = x.EmailVerified,
            }).FirstOrDefaultAsync();
        }
        public async Task<bool> RegisterLogin(Login login)
        {
            try
            {
                appDbContext.Login.Add(login);

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
