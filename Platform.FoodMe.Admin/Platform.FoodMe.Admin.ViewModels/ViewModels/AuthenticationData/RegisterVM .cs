using System.ComponentModel.DataAnnotations;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;

public class RegisterViewModel
{

    [StringLength(maximumLength: 10, ErrorMessage = "First Name is too long")]
    public string FirstName { get; set; }

    [StringLength(maximumLength: 10, ErrorMessage = "Last Name is too long")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    public string? Picture { get; set; }
    public string? Location { get; set; }

    public bool TwoFactorEnabled { get; set; } = false;
    public string? MacAddress { get; set; }

}
