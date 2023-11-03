using System.Security.Claims;

namespace Platform.Shared.Permissions
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("GetAllUsers", "GetAllUsers"),
            new Claim("GetUser", "GetUser"),
            new Claim("AddUser", "AddUser"),
            new Claim("UpdateUser", "UpdateUser"),
            new Claim("DeleteUser", "DeleteUser")
        };
    }
}
