using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.UserData
{
    public class UserRoleEntity : IdentityUserRole<String>
    {
       // [ForeignKey("BrandGroupEntityId")]
      //  public BrandGroupEntity BrandGroupId { get; set; }

      //  public int BrandGroupEntityId { get; set; } 
    }
}
