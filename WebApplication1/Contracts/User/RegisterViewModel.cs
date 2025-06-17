using WebApplication1.Models;

namespace WebApplication1.Contracts.User
{
    public class RegisterViewModel
    {
        //User Data
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Cep { get; set; }

        //Login Data
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
