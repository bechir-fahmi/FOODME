using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Shared.OperationResult
{
    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Z { get; set; }

        public GeoCoordinate()
        {
        }

        public GeoCoordinate(double latitude, double longitude, double z = 0)
        {
            Latitude = latitude;
            Longitude = longitude;
            Z = z;
        }
    }
}
