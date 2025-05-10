using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class SupplyPoint : IUpdatable
    {
        public int Id { get; private set; }
        public Location Location { get; private set; }
        public Inventory Inventory { get; private set; }
        public Demand Demand { get; private set; }

        public SupplyPoint(int id, Location location)
        {
            Id = id;
            Location = location;
            Inventory = new Inventory();
            Demand = new Demand();
        }

        public void UpdateStatus()
        {
            // Update the status of the supply point (e.g., check inventory levels, calculate demand, etc.)
            Console.WriteLine($"Updating inventory status for Supply Point {Id}...");
            Inventory.CheckStockLevels();
            Demand.CalculateDemand();
        }

        public void CalculateDemand()
        {
            // Simulate demand calculation for various resources
            Demand.CalculateDemand();
        }

        public void AllocateResources()
        {
            // Allocate resources to meet the demand
            Inventory.AllocateResources(Demand);
        }
    }

}
