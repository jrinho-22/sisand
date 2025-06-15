using System.Data;
using System.Text.Json.Serialization;
using FluentValidation;
using WebApplication1.Models.Validators;

namespace WebApplication1.Models
{
    public class Login
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool EmailVerified { get; set; } = false;
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime? DeletedAt { get; private set; }

        #region Constructor
        public Login() { }

        public Login(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }
        #endregion

        #region Relation
        [JsonIgnore]
        public virtual User User { get; set; } = null!;

        #endregion

        #region Methods
        public void IsValid()
        {
            new LoginValidator().ValidateAndThrow(this);
        }
        #endregion
    }
}
