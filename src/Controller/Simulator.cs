using Model.@enum;

namespace Controller;

public class Simulator
{
    public static void RunSimulator()
    {
        Circuit circuitToSimulate = Circuit.Bahrain; // Todo placeholder, will be passed in 
        // all code to run simulator 
        Console.WriteLine("running simulator..."); // Todo remove this once implemented
        SimulatedCircuit simulatedCircuit = new SimulatedCircuit(circuitToSimulate);
    }
}

   