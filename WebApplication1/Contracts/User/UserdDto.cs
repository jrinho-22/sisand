namespace WebApplication1.Contracts.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Cep { get; set; }

        //Login Data
        public string Email { get; set; }
    }
}
