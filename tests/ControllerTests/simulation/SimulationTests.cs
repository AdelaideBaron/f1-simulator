using Controller.simulation;
using Model.@enum;

namespace ControllerTests.simulation;

public class SimulationTests
{
    
    private Circuit _testCircuit;

    [SetUp]
    public void SetUp()
    {
    _testCircuit = Circuit.Bahrain;
    }
    
    [Test]
    public void SimulationAutoAssignsRaceIdAndCircuit()
    {
        Guid randomUuid = Guid.NewGuid();
        Simulation simulation = new Simulation(_testCircuit);
        Assert.That(simulation.Circuit, Is.EqualTo(Circuit.Bahrain));
        Assert.That(simulation.RaceId.GetType(), Is.EqualTo(randomUuid.GetType()));
    }
}