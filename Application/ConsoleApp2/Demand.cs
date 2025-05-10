using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public class Demand
    {
        public int Id { get; private set; }
        private Dictionary<string, int> demandLevels;

        public Demand()
        {
            demandLevels = new Dictionary<string, int>
        {
            { "Fuel", 500 },
            { "Ammunition", 200 },
            { "FoodSupplies", 400 },
            { "MedicalSupplies", 100 }
        };
        }

        public void CalculateDemand()
        {
            // Simulate demand calculation
            Console.WriteLine($"Calculating demand for Supply Point {Id}...");
            
           
        }

        public Dictionary<string, int> GetDemandLevels()
        {
            // Return the current demand levels
            return demandLevels;
        }

        public void AdjustForScenario()
        {
            // Adjust the demand levels based on simulated scenarios
            Console.WriteLine("Adjusting demand based on scenario...");
        }

        public void Forecast()
        {
            // Simulate demand forecasting
            Console.WriteLine("Forecasting demand...");
            var rand = new Random();
            foreach (var item in demandLevels.Keys.ToList())
            {
                demandLevels[item] = rand.Next(50, 500);
            }
        }
    }
}
