using Model.database.scripts;
using Model.dto;
using Model.@enum;

namespace ModelTests.database;

public class InsertDuringRaceTests
{
    [Test]
    public void RaceConditionsStatementIsFormattedToQueryDb()
    {
        CircuitConditions testCircuitConditions = new CircuitConditions()
        {
            Circuit = Circuit.Bahrain,
            IsRaining = false,
            Temp = 19.00
        };
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
    
}