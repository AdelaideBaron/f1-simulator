using Controller.simulation;
using Model.dto;
using Model.@enum;

namespace ControllerTests;

public class SimulationTests
{
    
    private Circuit testCircuit;

    [SetUp]
    public void SetUp()
    {
    //     testCircuitConditions = new CircuitConditions()
    //     {
    testCircuit = Circuit.Bahrain;
    //         IsRaining = false,
    //         Temp = 19.00
    //     };
    }
    
    [Test]
    public void SimulationAutoAssignsRaceIdAndCircuit()
    {
        Guid randomUuid = Guid.NewGuid();
        Simulation simulation = new Simulation(testCircuit);
        Assert.That(simulation.Circuit, Is.EqualTo(Circuit.Bahrain));
        Assert.That(simulation.RaceId.GetType(), Is.EqualTo(randomUuid.GetType()));
    }
}