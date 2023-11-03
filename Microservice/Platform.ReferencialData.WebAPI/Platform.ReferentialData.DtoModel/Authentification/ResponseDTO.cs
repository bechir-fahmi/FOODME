using System.ComponentModel.DataAnnotations;

namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class ResponseDTO
    {
        [Required]
        public int StatusCodes { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Error { get; set; } = string.Empty;


        public string Token { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }

        public DateTime ExpiredOn { get; set; }
    }
}
