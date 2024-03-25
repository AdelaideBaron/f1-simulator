﻿using Controller;
using Controller.http;
using Microsoft.Extensions.Configuration;
using Model.database;
using Model.@enum;

namespace View;

class UserView
    {
        static void Main(string[] args)
        {
            WeatherClient client = new WeatherClient();
            client.GetCircuitWeather(Circuit.Bahrain);
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
