using Controller;

namespace View;

static class UserView
    {
        static void Main(string[] args)
        {
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
