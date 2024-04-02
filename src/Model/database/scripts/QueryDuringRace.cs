using Model.@enum;

namespace Model.database.scripts;

public class QueryDuringRace
{
    public static string GetCircuitLaps(Circuit circuit)
    {
        string circuitName = CircuitExtensions.GetCircuitName(circuit);
        return $"SELECT laps FROM circuit WHERE circuit_name = \"{circuitName}\";";
    }
    
    public static string GetAvgPitStops(Circuit circuit)
    {
        string circuitName = CircuitExtensions.GetCircuitName(circuit);
        return $"SELECT avg_pitstops FROM circuit_stats WHERE circuit_id IN ( SELECT circuit_id FROM circuit WHERE circuit_name = \"{circuitName}\");";
    }
}