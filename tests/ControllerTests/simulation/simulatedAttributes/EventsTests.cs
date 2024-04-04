using Controller.simulation.simulatedAttributes;
using Model.dto;
using Model.@enum;

namespace ControllerTests.simulation.simulatedAttributes;

public class EventsTests
{
    private CircuitConditions _simulatedCircuitConditions;
    private RaceEvents _actualEvent;
    private int _testAvgPitstops;
    private int _testLaps;


    [SetUp]
    public void SetUp()
    {
        _simulatedCircuitConditions = new SimulatedCircuit(Circuit.Monaco).CurrentConditions;
        _testAvgPitstops = 2;
        _testLaps = 70;
        _actualEvent = Events.GetSimulatedRaceEvents(_simulatedCircuitConditions, _testAvgPitstops, _testLaps);
    }

    [Test]
    public void SimulatedRaceRainLapsBetween0AndMaxLaps()
    {
        Assert.That(0 <= _actualEvent.RainStarts && _testLaps >= _actualEvent.RainStarts);
        Assert.That(0 <= _actualEvent.RainStops && _testLaps >= _actualEvent.RainStops);
    }

    [Test]
    public void SimulatedRaceRainDoesNotStartBeforeEnd()
    {
        Assert.That(_actualEvent.RainStarts <= _actualEvent.RainStops);
    }

    [Test]
    public void SimulatedRaceRainLapsIsZeroIfCircuitNotRaining()
    {
        if (!_simulatedCircuitConditions.IsRaining)
        {
            Assert.That(_actualEvent.RainStarts == 0);
            Assert.That(_actualEvent.RainStops == 0);
        }
    }

    [Test]
    public void SimulatedRaceStrategyIncreasesIfRain()
    {
        if (_simulatedCircuitConditions.IsRaining)
        {
            Assert.That(_actualEvent.RaceStrategy > _testAvgPitstops);
        }
    }

    [Test]
    public void SimulatedRaceStrategyRemainsSameIfNoRain()
    {
        if (!_simulatedCircuitConditions.IsRaining)
        {
            Assert.That(_actualEvent.RaceStrategy == _testAvgPitstops);
        }
    }

    [Test]
    public void SimulatedIncidentsWithinCircuitLapsIfNotNull()
    {
        if (_actualEvent.RaceIncidents.Count > 0)
        {
            Dictionary<int, Incident> incidents = _actualEvent.RaceIncidents;
            List<int> keys = new List<int>(incidents.Keys);
            bool allKeysInRange = keys.All(key => key >= 0 && key <= _testLaps);
            Assert.True(allKeysInRange);
        }
    }

    [Test]
    public void SimulatedFirstLapIncidentOnFirstIfNotNull()
    {
        if (_actualEvent.RaceIncidents.Count > 0)
        {
            Dictionary<int, Incident> incidents = _actualEvent.RaceIncidents;
            if (incidents.ContainsKey(1))
            {
                Assert.That(incidents[1] == Incident.FIRST_LAP_INCIDENT);
            }
        }
    }
}