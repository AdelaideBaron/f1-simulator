using Controller.simulation.util;
using Model.dto;
using Model.@enum;

namespace Controller.simulation.simulatedAttributes;

public class Events // a class to 'predict' the race events
{
    public static RaceEvents GetSimulatedRaceEvents(SimulatedCircuit simulatedCircuit, int avgPitstops,
            int laps) // get avg pitstops from the db 
    {
        bool isRaining = simulatedCircuit.CurrentConditions.IsRaining;
        // get circuit stats 
        RaceEvents raceEvents = new RaceEvents();
        // set when rain 
        if (isRaining)
        {
            raceEvents.RainStarts = RandomGenerator.GetRandomInt(0, laps);
            raceEvents.RainStops = RandomGenerator.GetRandomInt(raceEvents.RainStarts, laps);
        }
        // set race strategy 
        raceEvents.RaceStrategy = (simulatedCircuit.CurrentConditions.IsRaining) ? GetRaceStrategy(raceEvents, avgPitstops) : avgPitstops;
        // set the incidents
        raceEvents.RaceIncidents = GetIncidents(isRaining, laps);
        return raceEvents;
    }

    private static int GetRaceStrategy(RaceEvents raceEvents, int avgPitstops)
    {
        // if raining for more than 50% of the race, increase the pit stops by 2 
        // if raining, increase by 1 
        // otherwise, avg pitstops 
        double percentageOfRaceRaining = ((double)(raceEvents.RainStops - raceEvents.RainStarts)) / avgPitstops;
        return (percentageOfRaceRaining < 0.5) ? avgPitstops + 1 : avgPitstops + 2;
    }

    private static Dictionary<int, Incident> GetIncidents(bool isRaining, int laps)
    {
        Dictionary<int, Incident> raceIncidents = new Dictionary<int, Incident>();

        try
        {
            // Add first lap incident if random condition is met
            if (RandomGenerator.GetRandomBool())
            {
                raceIncidents.Add(1, Incident.FIRST_LAP_INCIDENT);
            }

            // Allocate other incidents
            Incident[] allIncidentTypes = (Incident[])Enum.GetValues(typeof(Incident));
            List<Incident> incidentList = new List<Incident>(allIncidentTypes);
            incidentList.Remove(Incident.FIRST_LAP_INCIDENT);

            // Allocate each incident
            for (int i = 0; i < GetAmountOfIncidents(isRaining); i++)
            {
                // Get a random incident from the list
                Incident incident = RandomGenerator.GetRandomChoice(incidentList);

                // Get a random lap occurrence
                int lapOccurrence;
                do
                {
                    lapOccurrence = RandomGenerator.GetRandomInt(0, laps);
                } while (raceIncidents.ContainsKey(lapOccurrence)); // Ensure lap is not already assigned

                raceIncidents.Add(lapOccurrence, incident);

                // Add safety car after crashes
                if (incident == Incident.SOLO_CRASH || incident == Incident.MULTI_DRIVER_CRASH)
                {
                    // Safety car for next 1-5 laps
                    int durationOfSafetyCar = RandomGenerator.GetRandomInt(1, 5);
                    for (int j = 1; j <= durationOfSafetyCar; j++) // Start from next lap
                    {
                        int safetyCarLap = lapOccurrence + j;
                        if (!raceIncidents.ContainsKey(safetyCarLap))
                        {
                            raceIncidents.Add(safetyCarLap, Incident.SAFETY_CAR);
                        }
                    }
                }
            }

            return raceIncidents;
        }
        catch (ArgumentException ex)
        {
            // If there's a clash in the laps, simply skip the clashing incident simulation for now
            // You can choose to log the exception or handle it differently based on your requirement
            // For now, we'll just re-throw the exception
            throw;
        }

    }

    // private static Dictionary<Incident, int> GetIncident(int laps)
    // {
    //     
    // }

    private static int GetAmountOfIncidents(bool isRaining)
    {
        // if raining - more likely to have crashes & safety cars 
        // lets say it's 40% more likely to crash if raining - just a made up figure
        double chanceOfIncident = (isRaining) ? 1.40 : 1.00;
        // want, on avg, 1-3 incidents per race (but can be 0 or more) 
        double incidents = (RandomGenerator.GetRandomInt(0,5))*chanceOfIncident;
        return (int)Math.Round(incidents);
    }
    

}