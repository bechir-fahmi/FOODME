namespace Platform.ReferencialData.BusinessModel.Authentification
{
    public class UserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderSecret { get; set; }
        public string UserName { get; set; }
    }
}
