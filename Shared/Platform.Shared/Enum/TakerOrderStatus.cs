using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Shared.Enum
{
    public enum TakerOrderStatus
    {
        NO_STATUS = 0,
        PENDING = 11,
        ASSIGNED = 12,
        WAY_TO_STORE = 13,
        ARRIVED_AT_STORE = 14,
        WAY_TO_CUSTOMER = 15,
        NEAR_CUSTOMER = 16,
        DELIVERED = 21,
        CANCELED = 31,
        RETURNED = 32,
        DSP_ISSUE = 33,
        NOT_SENT = 34
    }
}
