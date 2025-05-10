using System;

namespace ConsoleApp2
{
    public class Weather
    {
        public string Type { get; set; }
        public string Forecast { get; private set; }
        public string Severity { get; private set; }

        public Weather()
        {
            Type = "clear";
            Forecast = "Clear";
            Severity = "Mild";
        }

        public void UpdateForecast()
        {
            // Simulate weather forecast update
            Console.WriteLine("Updating weather forecast...");
            var rand = new Random();
            Type = rand.Next(2) == 0 ? "Rain" : "Clear";
            Severity = rand.Next(2) == 0 ? "High" : "Low";
            Forecast = Type + " with " + Severity + " severity";
        }

        public void SimulateImpactOnRoutes()
        {
            // Simulate the impact of weather conditions on supply routes
            Console.WriteLine("Simulating impact of weather on routes...");
        }
    }
}
