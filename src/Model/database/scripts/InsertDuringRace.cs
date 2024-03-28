using Model.dto;
using Model.@enum;

namespace Model.database.scripts;

public class InsertDuringRace
{
    public static string GetRaceConditionsStatement(CircuitConditions circuitConditions) // can unit test this 
    {
        // get the circuit ID here 
        string circuitName = CircuitExtensions.GetCircuitName(circuitConditions.Circuit);
        string temp = circuitConditions.Temp.ToString();
        string rain = (circuitConditions.IsRaining) ? "1" : "0"; 
        return $"""
                INSERT INTO simulated_race_conditions
                (simulated_race_id, circuit_id, temp, raining)
                VALUES
                ((SELECT DISTINCT simulated_race_id FROM simulated_race_live ),
                (SELECT circuit_id FROM circuit WHERE circuit_name = "{circuitName}"),
                {temp},
                {rain});  -- simulated_race_live table is setup prior, and is cleared after each race 
                """;
    }
}