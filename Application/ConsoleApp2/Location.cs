using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Location
    {
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public Location(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

}
