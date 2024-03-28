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
        SimulatedCircuit simulatedCircuit = new SimulatedCircuit(Circuit);
        // setup drivers - todo in next branch
    }
}