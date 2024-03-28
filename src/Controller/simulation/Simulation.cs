using Model.dto;
using Model.@enum;

namespace System;

public class Simulation // for each simulation 
{
    public Circuit Circuit;
    private SimulatedCircuit simulatedCircuit;
    
    public Simulation(Circuit circuit)
    {
        Circuit = circuit;
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

    public void teardownDb()
    {
        clearDb();
    }

    private void clearDb()
    {
        //clear the tables that can be cleared
    }
}