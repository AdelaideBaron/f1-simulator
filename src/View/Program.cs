using Controller;
using Controller.database;
using Model.database;
using Model.database.scripts;
using Model.@enum;

namespace View;

class UserView
    {
        static void Main(string[] args)
        {
            DatabaseActions databaseActions = new DatabaseActions();
            // databaseActions.InitialiseDatabase();
            databaseActions.GetQueryResultsForInt(QueryDuringRace.GetCircuitLaps(Circuit.Bahrain));
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
        
    }
