using Controller;
using Microsoft.Extensions.Configuration;
using Model.Database;

namespace View;

class UserView
    {
        static void Main(string[] args)
        {
            DatabaseClient databaseClient = new DatabaseClient();
            databaseClient.PerformQuery();
            
            // print API key - to be used when setting up the circuit 
            Console.WriteLine(GetApiKey() );
            
            UserMessages.Welcome();
            UserMessages.RunSimulatorQuestion();
            bool runSimulation = UserInteractions.DoesUserWantAction();
            while (runSimulation)
            {
                UserMessages.Simulate();
                Simulator.RunSimulator();
                runSimulation = UserInteractions.DoesUserWantAction();
            }
            
            UserMessages.Exit();

        }

        private static string? GetApiKey() 
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<UserView>()
                .Build();
            
            Console.WriteLine($"Hello, {config["OpenWeatherApiKey"]}");
            return config["OpenWeatherApiKey"];
        }
        
    }
