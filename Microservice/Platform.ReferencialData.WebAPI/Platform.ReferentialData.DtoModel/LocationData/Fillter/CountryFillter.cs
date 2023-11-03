using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel.LocationData.Fillter
{
    public class CountryFillter
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string CountryKey { get; set; }
    }
}
