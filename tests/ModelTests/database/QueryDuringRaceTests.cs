using Model.database.scripts;
using Model.@enum;

namespace ModelTests.database;

public class QueryDuringRaceTests
{
    private Circuit circuit;
    [SetUp]
    public void SetUp()
    {
        circuit = Circuit.Bahrain;
    }

    [Test]
    public void GetCircuitLapsFormatsCircuitName()
    {
        string actual = QueryDuringRace.GetCircuitLaps(circuit);
        string expected = $"SELECT laps FROM circuit WHERE circuit_name = \"Bahrain International Circuit\";";
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void GetCircuitPitstopsFormatsCircuitName()
    {
        string actual = QueryDuringRace.GetAvgPitStops(circuit);
        string expected = $"SELECT avg_pitstops FROM circuit_stats WHERE circuit_id IN ( SELECT circuit_id FROM circuit WHERE circuit_name = \"Bahrain International Circuit\");";
        Assert.That(actual, Is.EqualTo(expected));
    }
    
}