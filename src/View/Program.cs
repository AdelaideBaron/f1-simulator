using Controller;
using Model.database;

namespace View;

class UserView
    {
        static void Main(string[] args)
        {
            DatabaseClient databaseClient = new DatabaseClient();
            databaseClient.InitialiseDatabase();
            
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
        
    }
