using Model.@enum;

namespace System;

public class SimulatedCircuit
{
    public Circuit circuit;

    public SimulatedCircuit(Circuit circuitToSimulate)
    {
        circuit = circuitToSimulate;
        InitialiseCircuit();
    }
    
    private void InitialiseCircuit()
    {
        
    }
}