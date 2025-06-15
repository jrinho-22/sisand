namespace WebApplication1.Models
{
    public class ValidateEmail
    {
        public long Id { get; set; }
        public string Email { get; private set; }
        public string Token { get; private set; }
        public long LoginId { get; set; }

        #region Relation
        public Login Login { get; set; }
        #endregion

        public ValidateEmail() { }

        public ValidateEmail(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}
