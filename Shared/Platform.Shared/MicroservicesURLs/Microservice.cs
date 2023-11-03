using static System.Net.WebRequestMethods;

namespace Platform.Shared.MicroservicesURLs
{
    public static class Microservice
    {
        public static readonly string FoodMeIpAddress = Environment.GetEnvironmentVariable("FoodMeIpAddress");
        public static readonly string FoodMeAdminAPIPort = Environment.GetEnvironmentVariable("FoodMeAdminAPIPort");
        public static readonly string FoodMeRefDataPort = Environment.GetEnvironmentVariable("FoodMeRefDataPort");
        public static readonly string FoodMeTrackingPort = Environment.GetEnvironmentVariable("FoodMeTrackingPort");
        public static readonly string FoodMeOrderManagementPort = Environment.GetEnvironmentVariable("FoodMeOrderManagementPort");
        public static readonly string FoodMeNotifSystemsPort = Environment.GetEnvironmentVariable("FoodMeNotifSystemsPort");
        public static readonly string FoodMePaymentPort = Environment.GetEnvironmentVariable("FoodMePaymentPort");
        public static readonly string FoodMePOSPort = Environment.GetEnvironmentVariable("FoodMePOSPort");
        public static readonly string FoodMeDeliveryCompaniesPort = Environment.GetEnvironmentVariable("YijiDeliveryCompaniesort");
        public static readonly string FoodMeExternalOperatoresPort = Environment.GetEnvironmentVariable("FoodMeExternalOperatoresPort");
        //public static readonly string FoodMeRedisPort = Environment.GetEnvironmentVariable("FoodMeRedisPortDev");
        public static readonly string FoodMeRedisPort = Environment.GetEnvironmentVariable("FoodMeRedisPortProd");
        public static readonly string FoodMeCamundaPort = Environment.GetEnvironmentVariable("FoodMeCamundaPort");
        public static readonly string RedisIpAddress = Environment.GetEnvironmentVariable("RedisIpAddress");
        public static readonly string CamundaIpAddress = Environment.GetEnvironmentVariable("CamundaIpAddress");

        public static readonly string AdminAPI = "http://197.14.48.62:5100";
        //public static readonly string AdminAPI = $"https://{IpAddress}:{AdminAPIPort}";
        public static readonly string RefData = $"https://{FoodMeIpAddress}:{FoodMeRefDataPort}/api";
        public static readonly string OrderManagement = $"https://{FoodMeIpAddress}:{FoodMeOrderManagementPort}/api";
        public static readonly string NotifSystems = $"https://{FoodMeIpAddress}:{FoodMeNotifSystemsPort}/api";
        public static readonly string Payment = $"https://{FoodMeIpAddress}:{FoodMePaymentPort}/api";
        public static readonly string POS = $"https://{FoodMeIpAddress}:{FoodMePOSPort}/api";
        public static readonly string DeliveryCompanies = $"https://{FoodMeIpAddress}:{FoodMeDeliveryCompaniesPort}/api";
        public static readonly string ExternalOperatores = $"https://{FoodMeIpAddress}:{FoodMeExternalOperatoresPort}/api";
        public static readonly string FoodMeTracking = $"http://{FoodMeIpAddress}:{FoodMeTrackingPort}/api";
        public static readonly string Redis = $"{RedisIpAddress}:{FoodMeRedisPort}";
        public static readonly string Camunda = $"http://{CamundaIpAddress}:{FoodMeCamundaPort}/engine-rest";
    }
}
