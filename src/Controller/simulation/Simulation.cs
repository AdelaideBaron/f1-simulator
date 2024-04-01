using Model.database.scripts;
using Model.@enum;

namespace Controller.simulation;

public class Simulation // for each simulation 
{
    public Circuit Circuit;
    public Guid RaceId { get; }
    private SimulatedCircuit _simulatedCircuit;
    
    public Simulation(Circuit circuit)
    {
        Circuit = circuit;
        RaceId = Guid.NewGuid();
    }

    public void runSimulation()
    {
        // run the unique simulation with the setup circuit and drivers 
    }
    
    private void SetupCircuitAndDrivers()
    {
        // setup the tables - simulated race results - first 
        // setup drivers - todo in next branch
        // add this to the DB 
        SimulatedCircuit simulatedCircuit = new SimulatedCircuit(Circuit);
    }
}