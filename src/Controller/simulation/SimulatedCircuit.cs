using Controller.http;
using Controller.simulation.simulatedAttributes;
using Model.dto;
using Model.@enum;

namespace System;

public class SimulatedCircuit
{
    private Circuit circuit;
    public CircuitConditions CurrentConditions { get; set; }

    public RaceEvents RaceEvents { get; set; }

    public SimulatedCircuit(Circuit circuitToSimulate)
    {
        circuit = circuitToSimulate;
        InitialiseCircuit();
    }
    
    private void InitialiseCircuit()
    {
        WeatherClient client = new WeatherClient();
        CurrentConditions = client.GetCircuitConditions(circuit);
        RaceEvents = Events.GetSimulatedRaceEvents(CurrentConditions, 2, 90); // Todo placeholders! 
    }
}