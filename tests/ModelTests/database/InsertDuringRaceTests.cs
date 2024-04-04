using System.Text.RegularExpressions;
using Model.database.scripts;
using Model.dto;
using Model.@enum;

namespace ModelTests.database;

public class InsertDuringRaceTests
{
    private CircuitConditions testCircuitConditions;

    [SetUp]
    public void SetUp()
    {
        testCircuitConditions = new CircuitConditions()
        {
            Circuit = Circuit.Bahrain,
            IsRaining = false,
            Temp = 19.00
        };
    }
    
    [Test]
    public void RaceConditionsStatementIsFormattedToQueryDb()
    {
        
        string actual = InsertDuringRace.GetRaceConditionsStatement(testCircuitConditions);
        string expected = """
                          INSERT INTO simulated_race_conditions
                          (simulated_race_id, circuit_id, temp, raining)
                          VALUES
                          ((SELECT DISTINCT simulated_race_id FROM simulated_race_live ),
                          (SELECT circuit_id FROM circuit WHERE circuit_name = "Bahrain International Circuit"),
                          19,
                          0);  -- simulated_race_live table is setup prior, and is cleared after each race
                          """.Trim();
        Console.WriteLine(actual);
        Assert.That(actual.Trim(), Is.EqualTo(expected));
    }
    
    [Test]
    public void SimulatedRaceInitStatementFormatsUuid()
    {
        string actual = InsertDuringRace.GetSimulatedRaceLiveInitialiseStatement("someString").Replace("\r\n", "\n").Trim();
        string expected = $"""
                           INSERT INTO simulated_race_live
                           (simulated_race_id, driver_number)
                           SELECT driver_number, "someString" FROM driver;
                           """.Replace("\r\n", "\n").Trim();
        
        expected = Regex.Replace(expected, @"\s+", " ").Trim(); 
        actual = Regex.Replace(actual, @"\s+", " ").Trim(); 
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
}