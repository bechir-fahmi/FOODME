namespace Platform.Shared.Enum
{
    public enum OrderStatus
    {
        Initial = 0,
        PendingPayment = 1,
        Received = 2,
        FindingDriver = 3,
        DriverAccept = 4,
        Inkitchen = 5,  
        Manual = 6,
        ReadyToPickup = 7,
        Indelivery = 8,
        Delivered = 9,
        Closed = 10,
        Canceled = 11,
        ForceCancel = 12,
        ForceClosed = 13,

        Arrived = 65,

        BUMPED = 14,
        Ready = 15,
        ASSIGNED = 16,
        ENROUTE = 32,
        Suspended = 96,
        FUTURE = 100,
        FAILURE = 256,
        UNKNOWN = 1024,
        REQUESTFORCANCELORDER = 4096,
    }
}
