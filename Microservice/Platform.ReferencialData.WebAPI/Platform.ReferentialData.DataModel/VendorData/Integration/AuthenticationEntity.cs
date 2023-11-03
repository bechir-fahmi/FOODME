using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.BrandData;
[Table("IntegrationAuthentication")]
public class AuthenticationEntity
{
    [Key]
    [Required]
    public Guid AuthenticationId { get; set; }
    [Required]
    public AuthenticationType AuthenticationType { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
    public string APIkey { get; set; }

}
