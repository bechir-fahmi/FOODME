using System.ComponentModel.DataAnnotations;

namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string UserType { get; set; } = "client";
        public string? Picture { get; set; }
        public string? Location { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public string? MacAddress { get; set; }

    }
}
