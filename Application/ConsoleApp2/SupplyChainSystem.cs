using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp2
{
    public class SupplyChainSystem
    {
        private List<SupplyPoint> supplyPoints;
        private List<TransportUnit> transportationUnits;
        private List<Route> routes;
        private Weather weatherConditions;
        // event for critical situations
        public event Action<string> CriticalSituationOccurred;
        //constructor
        public SupplyChainSystem()
        {
            supplyPoints = new List<SupplyPoint>();
            transportationUnits = new List<TransportUnit>();
            routes = new List<Route>();
            weatherConditions = new Weather();
        }

        public void Initialize()// initialize the system with the user input
        {
            Console.WriteLine("Initializing supply chain system...");

            // Initialize supply points
            int supplyPointCount = GetValidInteger("Enter number of supply points:");
            for (int i = 0; i < supplyPointCount; i++)
            {
                Console.WriteLine($"Enter latitude and longitude for Supply Point {i + 1} (comma separated):");
                string[] location = GetValidLocation();
                float latitude = float.Parse(location[0]);
                float longitude = float.Parse(location[1]);
                supplyPoints.Add(new SupplyPoint(i + 1, new Location(latitude, longitude)));
            }

            // Initialize transportation units
            int transportUnitCount = GetValidInteger("Enter number of transportation units:");
            for (int i = 0; i < transportUnitCount; i++)
            {
                Console.WriteLine($"Enter type, capacity, and location (latitude, longitude) for Transport Unit {i + 1} (comma separated):");
                string[] input = GetValidTransportInput();
                string type = input[0];
                int capacity = int.Parse(input[1]);
                float latitude = float.Parse(input[2]);
                float longitude = float.Parse(input[3]);
                transportationUnits.Add(new TransportUnit(i + 1, type, capacity, new Location(latitude, longitude)));
            }

            // Initialize routes
            int routeCount = GetValidInteger("Enter number of routes:");
            for (int i = 0; i < routeCount; i++)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine($"Enter start point ID, end point ID, and distance for Route {0 + 1} (comma separated):");//These routes are assigned based on the Id of the supply points that you provide.
                        string[] input = Console.ReadLine().Split(',');

                        if (input.Length != 3)
                        {
                            throw new FormatException("Invalid input format. Please enter three comma-separated values.");
                        }

                        int startPointId = int.Parse(input[0]);
                        int endPointId = int.Parse(input[1]);
                        float distance = float.Parse(input[2]);

                        ValidateRouteInput(startPointId, endPointId, distance);

                        SupplyPoint startPoint = supplyPoints.FirstOrDefault(sp => sp.Id == startPointId);
                        SupplyPoint endPoint = supplyPoints.FirstOrDefault(sp => sp.Id == endPointId);

                        if (startPoint == null)
                        {
                            throw new ArgumentException($"No Supply Point found with ID {startPointId}. Please enter a valid ID.");
                        }

                        if (endPoint == null)
                        {
                            throw new ArgumentException($"No Supply Point found with ID {endPointId}. Please enter a valid ID.");
                        }

                        // Check if this combination of start and end points already exists
                        if (routes.Any(r => r.StartPoint.Id == startPointId && r.EndPoint.Id == endPointId))
                        {
                            throw new ArgumentException($"A route already exists between Supply Point {startPointId} and Supply Point {endPointId}. Please enter a different combination.");
                        }

                        // Add the route
                        routes.Add(new Route(i + 1, startPoint, endPoint, distance));
                        break;
                    }
                    catch (FormatException fe)
                    {
                        Console.WriteLine($"Error: {fe.Message}. Please try again.");
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine($"Error: {ae.Message}. Please try again.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"An unexpected error occurred: {e.Message}. Please try again.");
                    }
                }
            }

            // Initialize weather
            weatherConditions.UpdateForecast();
        }

        public void StartRealTimeUpdates()
        {
            Thread weatherThread = new Thread(UpdateWeatherPeriodically);
            Thread statusThread = new Thread(UpdateStatusPeriodically);

            weatherThread.Start();
            statusThread.Start();
        }

        private void UpdateWeatherPeriodically()
        {
            while (true)
            {
                UpdateWeather();
                Thread.Sleep(60000); // Update every minute
            }
        }

        private void UpdateStatusPeriodically()// Update Weather Periodically
        {
            while (true)
            {
                UpdateAllStatuses();
                Thread.Sleep(60000); // Update every minute
            }
        }

        protected virtual void OnCriticalSituationOccurred(string message)// Trigger Critical situation event
        {
            CriticalSituationOccurred?.Invoke(message);
        }

        public void SimulateScenario()// Simulate a supply chain scenario
        {
            Console.WriteLine("Simulating supply chain scenario...");
            foreach (var supplyPoint in supplyPoints)
            {
                supplyPoint.UpdateStatus();
            }

            foreach (var transportUnit in transportationUnits)
            {
                transportUnit.UpdateStatus();
            }

            foreach (var route in routes)
            {
                route.UpdateStatus();
            }

            weatherConditions.SimulateImpactOnRoutes();
        }

        public void OptimizeRoutes()
        {
            Console.WriteLine("Optimizing supply routes...");
            foreach (var route in routes)
            {
                route.CalculateShortestPath();
                route.EvaluateRisk();
            }
        }

        public void AllocateResources()
        {
            Console.WriteLine("Allocating resources across supply points...");
            foreach (var supplyPoint in supplyPoints)
            {
                supplyPoint.CalculateDemand();
                supplyPoint.AllocateResources();
            }
        }

        public void ForecastDemand()
        {
            Console.WriteLine("Forecasting future demand...");
            foreach (var supplyPoint in supplyPoints)
            {
                Console.WriteLine($"Supply Point {supplyPoint.Id}:");
                supplyPoint.Demand.Forecast();
                supplyPoint.Demand.AdjustForScenario();
            }
        }

        public void UpdateWeather()
        {
            Console.WriteLine("Updating weather conditions...");
            weatherConditions.UpdateForecast();
            weatherConditions.SimulateImpactOnRoutes();

            if (weatherConditions.Severity == "Severe")
            {
                OnCriticalSituationOccurred("Severe weather conditions detected. Routes may be affected.");
            }
        }

        public void ViewSystemStatus()
        {
            Console.WriteLine("\nSystem Status:");
            Console.WriteLine("---------------");

            Console.WriteLine("Supply Points:");
            foreach (var sp in supplyPoints)
            {
                Console.WriteLine($"  ID: {sp.Id}, Location: ({sp.Location.Latitude}, {sp.Location.Longitude})");
                var inventory = sp.Inventory.CheckStockLevels();
                Console.WriteLine($"    Inventory: Fuel: {inventory["Fuel"]}, Ammo: {inventory["Ammunition"]}, Food: {inventory["FoodSupplies"]}, Medical: {inventory["MedicalSupplies"]}");
                var demand = sp.Demand.GetDemandLevels();
                Console.WriteLine($"    Demand: Fuel: {demand["Fuel"]}, Ammo: {demand["Ammunition"]}, Food: {demand["FoodSupplies"]}, Medical: {demand["MedicalSupplies"]}");
            }

            Console.WriteLine("\nTransportation Units:");
            foreach (var tu in transportationUnits)
            {
                Console.WriteLine($"  ID: {tu.Id}, Type: {tu.Type}, Capacity: {tu.Capacity}, Status: {tu.Status}");
                Console.WriteLine($"    Location: ({tu.Location.Latitude}, {tu.Location.Longitude})");
            }

            Console.WriteLine("\nRoutes:");
            foreach (var route in routes)
            {
                Console.WriteLine($"  ID: {route.Id}, Start Point: {route.StartPoint.Id}, End Point: {route.EndPoint.Id}, Distance: {route.Distance}");
                Console.WriteLine($"    Risk Level: {route.RiskLevel}");
            }

            Console.WriteLine("\nWeather Conditions:");
            Console.WriteLine($"  Forecast: {weatherConditions.Forecast}");
            Console.WriteLine($"  Severity: {weatherConditions.Severity}");
        }

        // security measure : input validation for integers
        private int GetValidInteger(string message)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    string input = Console.ReadLine();
                    return int.Parse(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");//If the input format is incorrect, it prompts the user to enter the correct format
                }
            }
        }

        private string[] GetValidLocation()
        {
            while (true)
            {
                try
                {
                    string[] location = Console.ReadLine().Split(',');
                    if (location.Length != 2)
                    {
                        throw new FormatException("Please enter exactly two values separated by a comma.");
                    }
                    float.Parse(location[0]); // Validate latitude
                    float.Parse(location[1]); // Validate longitude
                    return location;
                }
                catch (FormatException fe)
                {
                    Console.WriteLine($"Error: {fe.Message}. Please try again.");
                }
            }
        }

        private string[] GetValidTransportInput()
        {
            while (true)
            {
                try
                {
                    string[] input = Console.ReadLine().Split(',');
                    if (input.Length != 4)
                    {
                        throw new FormatException("Please enter exactly four values separated by commas.");
                    }
                    // Validate the values
                    int.Parse(input[1]); // Validate capacity
                    float.Parse(input[2]); // Validate latitude
                    float.Parse(input[3]); // Validate longitude
                    return input;
                }
                catch (FormatException fe)
                {
                    //catching built-in FormationException for invalid input Format
                    Console.WriteLine($"Error: {fe.Message}. Please try again.");
                }
            }
        }

        // Custom Exception handling for route input validation
        private void ValidateRouteInput(int startPointId, int endPointId, float distance)
        {
            if (startPointId == endPointId)
            {
                throw new ArgumentException("Start point and end point cannot be the same.");
            }
            //Custom check for valid Distance
            if (distance <= 0)
            {
                //Throwing a built-in ArgumentException with a custom message 
                throw new ArgumentException("Distance must be greater than zero.");
            }
        }

        private void UpdateAllStatuses()
        {
            foreach (var supplyPoint in supplyPoints)
            {
                supplyPoint.UpdateStatus();
            }

            foreach (var transportUnit in transportationUnits)
            {
                transportUnit.UpdateStatus();
            }

            foreach (var route in routes)
            {
                route.UpdateStatus();
            }
        }
    }
}
