using System;

namespace ConsoleApp2
{
    class Program
    {// main program
        static void Main(string[] args)
        {
            Console.WriteLine("Supply Chain Simulation System for Military");
            Console.WriteLine("===Let's get started with the process==");
            // initialize the supply chain system
            SupplyChainSystem system = new SupplyChainSystem();
            system.Initialize();
            system.StartRealTimeUpdates();
            // main program looping
            while (true)
            {
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Simulate Scenario");
                Console.WriteLine("2. Optimize Routes");
                Console.WriteLine("3. Allocate Resources");
                Console.WriteLine("4. Forecast Demand");
                Console.WriteLine("5. Update Weather");
                Console.WriteLine("6. View System Status");
                Console.WriteLine("7. Exiting.....");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        system.SimulateScenario();
                        break;
                    case "2":
                        system.OptimizeRoutes();
                        break;
                    case "3":
                        system.AllocateResources();
                        break;
                    case "4":
                        system.ForecastDemand();
                        break;
                    case "5":
                        system.UpdateWeather();
                        break;
                    case "6":
                        system.ViewSystemStatus();
                        break;
                    case "7":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
