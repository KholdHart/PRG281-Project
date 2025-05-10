using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Route : IUpdatable
    {
        public int Id { get; private set; }
        public SupplyPoint StartPoint { get; private set; }
        public SupplyPoint EndPoint { get; private set; }
        public float Distance { get; private set; }
        public string RiskLevel { get; private set; }

        public Route(int id, SupplyPoint startPoint, SupplyPoint endPoint, float distance)
        {
            Id = id;
            StartPoint = startPoint;
            EndPoint = endPoint;
            Distance = distance;
            RiskLevel = "Low";
        }

        public void UpdateStatus()
        {
            // Update the status of the route (e.g., check for obstacles, weather impact, etc.)
            Console.WriteLine($"Updating route {Id}...");
            EvaluateRisk();
        }

        public void CalculateShortestPath()
        {
            // Simulate shortest path calculation between start and end points

            Console.WriteLine($"Calculating shortest path for Route {Id}...");

        }

        public void EvaluateRisk()
        {
            // Simulate risk evaluation based on various factors such as weather, distance, etc.
            Console.WriteLine($"Evaluating risk for Route {Id}...");
            // Implement risk evaluation
            RiskLevel = new Random().Next(2) == 0 ? "High" : "Low";
        }
    }

}
