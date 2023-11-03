using System.ComponentModel.DataAnnotations;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;

public class ResponseVM
{
    [Required]
    public int StatusCodes { get; set; }
    [Required]
    public string Message { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    public string? RefreshToken { get; set; }

    public DateTime ExpiredOn { get; set; }
}
