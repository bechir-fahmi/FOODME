using System.ComponentModel.DataAnnotations;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;

public class LoginVM
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}