using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.LocationData
{
   
    public class CoordinateEntity:ReferentialDataBase
    {
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public int AreaId { get; set; }

        public AreaEntity Area { get; set; }
    }

}
