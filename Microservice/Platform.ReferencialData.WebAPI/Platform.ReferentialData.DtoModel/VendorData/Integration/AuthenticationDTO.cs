using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.BrandData;

public class AuthenticationDTO
{
    public Guid AuthenticationId { get; set; }
    public AuthenticationType AuthenticationType { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
    public string APIkey { get; set; }
}
