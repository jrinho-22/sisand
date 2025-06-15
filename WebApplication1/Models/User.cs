using System.Text.Json.Serialization;
using FluentValidation;
using WebApplication1.Models.Validators;

namespace WebApplication1.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime? DeletedAt { get; private set; }

        #region Constructor
        public User() { }
        #endregion

        #region Relation
        [JsonIgnore]
        public virtual Login Login { get; set; }
        #endregion

        #region Methods
        public void IsValid()
        {
            new UserValidator().ValidateAndThrow(this);
        }
        #endregion
    }
}
