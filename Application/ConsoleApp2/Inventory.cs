using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Inventory
    {
        private Dictionary<string, int> stockLevels;

        public Inventory()
        {
            stockLevels = new Dictionary<string, int>
        {
            { "Fuel", 10000 },
            { "Ammunition", 4000 },
            { "FoodSupplies", 5000 },
            { "MedicalSupplies", 4500 }
        };
        }

        public Dictionary<string, int> CheckStockLevels()
        {
            // Simulate checking stock levels
            return new Dictionary<string, int>(stockLevels);
        }

        public void AllocateResources(Demand demand)
        {
            // Simulate resource allocation based on demand
            
        }
    }
}
