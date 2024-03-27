using Controller.http;
using Model.dto;
using Model.@enum;

namespace System;

public class SimulatedCircuit
{
    private Circuit circuit;
    public CircuitConditions CurrentConditions { get; set; }

    public SimulatedCircuit(Circuit circuitToSimulate)
    {
        circuit = circuitToSimulate;
        InitialiseCircuit();
    }
    
    private void InitialiseCircuit()
    {
        WeatherClient client = new WeatherClient();
        CurrentConditions = client.GetCircuitConditions(circuit);
    }
}