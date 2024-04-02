using Model.@enum;

namespace Model.dto;

public class RaceEvents
{
    public int RainStarts { get; set; } // the lap
    public int RainStops { get; set; } // the lap
    public int RaceStrategy { get; set; } // pitstops
    public Dictionary<int, Incident> RaceIncidents { get; set; } // int = lap it occurs 
}