using System.ComponentModel.DataAnnotations;

namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class LoginDTO
    {
        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
