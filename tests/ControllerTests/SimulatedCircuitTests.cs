using Model.@enum;

namespace ControllerTests;

public class SimulatedCircuitTests
{
    [Test]
    public void SimulatedCircuitInitialisesCircuit()
    {
        SimulatedCircuit simulatedCircuit = new SimulatedCircuit(Circuit.Monaco);
        Assert.NotNull(simulatedCircuit.CurrentConditions.IsRaining);
    }
}