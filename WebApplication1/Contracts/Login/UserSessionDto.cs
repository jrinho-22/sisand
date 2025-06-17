namespace WebApplication1.Contracts.Login
{
    public class UserSessionDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
    }
}
