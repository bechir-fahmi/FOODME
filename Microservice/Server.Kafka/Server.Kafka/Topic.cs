namespace Server.Kafka
{
    public static class Topic
    {
        public static readonly string TOPIC_WATCHDOG_RECEIVE_MESSAGE = "WatchDogReceive";
        public static readonly string TOPIC_WATCHDOG_SEND_MESSAGE = "WatchDogSend";

        public static readonly string TOPIC_POS_GET_LOCATION_DATA = "PosGetLocationData";

        public static readonly string TOPIC_POS_GET_RESTAURANTS = "PosGetRestaurants";

        public static readonly string TOPIC_POS_GET_TEMPLATEMENUS = "PosGetTemplateMenus";

        public static readonly string TOPIC_POS_GET_MENUCATEGORIES = "PosGetMenuCategories";

        public static readonly string TOPIC_CHECK_REFERENTIALDATA = "CheckReferentialData";

        public static readonly string TOPIC_PAYMENT_PROCESS = "PaymentProcess";

        public static readonly string TOPIC_DELIVERY_PROCESS = "DeliveryProcess";

        public static readonly string TOPIC_POS_PROCESS = "POSProcess";
    }
}
