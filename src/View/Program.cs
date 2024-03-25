using Controller;
using Controller.http;
using Microsoft.Extensions.Configuration;
using Model.database;

namespace View;

class UserView
    {
        static void Main(string[] args)
        {
            OpenWeatherHttpClient client = new OpenWeatherHttpClient();
            try
            {
                string todo = client.GetTodoAsync(1).Result;
                Console.WriteLine(todo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                client.Dispose();
            }
            // DatabaseClient databaseClient = new DatabaseClient();
            // databaseClient.InitialiseDatabase();
            //
            // UserMessages.Welcome();
            // UserMessages.RunSimulatorQuestion();
            // bool runSimulation = UserInteractions.DoesUserWantAction();
            // while (runSimulation)
            // {
            //     UserMessages.Simulate();
            //     Simulator.RunSimulator();
            //     runSimulation = UserInteractions.DoesUserWantAction();
            // }
            //
            // UserMessages.Exit();

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
