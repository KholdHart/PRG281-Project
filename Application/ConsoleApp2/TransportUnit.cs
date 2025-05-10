using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class TransportUnit : IUpdatable
    {
        public int Id { get; private set; }
        public string Type { get; private set; }
        public int Capacity { get; private set; }
        public Location Location { get; private set; }
        public string Status { get; private set; }

        public TransportUnit(int id, string type, int capacity, Location location)
        {
            Id = id;
            Type = type;
            Capacity = capacity;
            Location = location;
            Status = "Idle";
        }

        public void UpdateStatus()
        {
            // Update the status of the transportation unit (e.g., location, fuel levels, etc.)
            Status = "Operational";
        }
    }

}
