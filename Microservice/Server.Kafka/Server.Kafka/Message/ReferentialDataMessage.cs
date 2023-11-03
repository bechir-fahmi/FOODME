
using Platform.Shared.Enum;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Kafka.Message
{
    [ProtoContract()]
    public class ReferentialDataMessage
    {
        [ProtoMember(1)]
        public string UserId { get; set; }

        [ProtoMember(2)]
        public int RestaurantId { get; set; }

        [ProtoMember(3)]
        public int OrderId { get; set; }

        [ProtoMember(4)]
        public DeliveryType DeliveryType { get; set; }

        [ProtoMember(5)]
        public double Total { get; set; }

        [ProtoMember(6)]
        public string BusinessKey { get; set; }

       // [ProtoMember(7)]
       // public DeliveryAddressDto DeliveryAddress { get; set; }


    }
}
