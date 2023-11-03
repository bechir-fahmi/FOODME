namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class UserLoginDTO
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderSecret { get; set; }
        public string UserName { get; set; }

    }
}
