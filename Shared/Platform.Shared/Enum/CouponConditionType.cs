namespace Platform.Shared.Enum
{
    public enum CouponConditionType
    {
        OrderMinimum = 1,
        OrderMaximum = 2,
        MaximumDiscount = 3,
        /// <summary>
        /// Cannot have coupon reductions total amount 
        /// more than this value for one month
        /// </summary>
        ReachMonthLimit = 4,

    }
}
