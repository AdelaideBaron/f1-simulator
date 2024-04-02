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

    public void RunSimulation()
    {
        // run the unique simulation with the setup circuit and drivers 
    }
    
    private void SetupCircuitAndDrivers()
    {
        // setup the tables - simulated race results - first 
        // setup drivers - todo in next branch
        // add this to the DB 
        SimulatedCircuit simulatedCircuit = new SimulatedCircuit(Circuit);
        string actual = InsertDuringRace.GetRaceConditionsStatement(simulatedCircuit.CurrentConditions);
    }

    private void SetupDrivers()
    {
        // get the race conditions - weather etc
        // find out the avg tyre for the race 
        // setup the teams - decide if they are going for a two stop, one stop, etc 
        // get the positions of each driver 
    }

    private void SetupEvents() // Todo add these events into a race report at the end 
    {
        // eg will it rain, when, will there be crashes, etc 
    }
}