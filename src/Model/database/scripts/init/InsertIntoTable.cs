using System.Text;
using Model.@enum;

namespace Model.database.scripts;

public class InsertIntoTable
{
    private const string InsertTeams = """
                                        INSERT INTO team
                                        (team_name)
                                        VALUES 
                                            ("Mercedes"), 
                                            ("Red Bull Racing"), 
                                            ("Ferrari"), 
                                            ("McLaren"), 
                                            ("Aston Martin"), 
                                            ("RB"), 
                                            ("Haas F1 Team"), 
                                            ("Williams"), 
                                            ("Kick Sauber"), 
                                            ("Alpine");
                                       """;
    
    private const string InsertDrivers = """
                                        INSERT INTO driver
                                        (driver_number, team_id, driver_name)
                                        VALUES
                                            (63,1,"George Russell"),
                                            (44,1,"Hamilton"),
                                            (1,  2, "Verstappen"), 
                                            (11,  2, "Perez"), 
                                           (16,3,"Leclerc"),
                                           (55,3,"Sainz"),
                                           (81,4,"Piastri"),
                                           (4,4,"Norris"),
                                           (14,5,"Alonso"),
                                           (18,5,"Stroll"),
                                           (3,6,"Ricciardo"),
                                           (22,6,"Tsunoda"),
                                           (27 ,7,"Hulkenberg"),
                                           (20 ,7,"Magnussen"),
                                           ( 23,8,"Albon"),
                                           (2 ,8,"Sargeant"),
                                           ( 77,9,"Bottas"),
                                           (24 ,9,"Zhou"),
                                           (31 ,10,"Ocon"),
                                           ( 10,10,"Gasly")
                                       """;
    
    private const string InsertDriverRating = """ 
                                          INSERT INTO driver_rating -- not accurate values, avg position taken from standings as of 24/03 
                                          (driver_number, avg_position, consistency)
                                          VALUES
                                         (63, 7, RAND()), 
                                         (44, 10, RAND()), 
                                         (1, 1, RAND()), 
                                         (11, 3, RAND()), 
                                         (16, 2, RAND()), 
                                         (55, 4, RAND()), 
                                         (81, 5, RAND()), 
                                         (4, 6, RAND()), 
                                         (14, 8, RAND()), 
                                         (18, 9, RAND()), 
                                         (3, 17, RAND()), 
                                         (22, 11, RAND()), 
                                         (27, 13, RAND()), 
                                         (20, 14, RAND()), 
                                         (23, 15, RAND()), 
                                         (2, 21, RAND()), 
                                         (77, 20, RAND()), 
                                         (24, 16, RAND()), 
                                         (31, 18, RAND()), 
                                         (10, 19, RAND());
                                         """;
    
    private const string InsertTyre = """
                                               INSERT INTO tyre  -- rough data gathered from https://www.blackcircles.com
                                                   (compound, max_laps, speed)
                                               VALUES
                                              ("soft", 25, 1),
                                              ("medium", 35, 1),
                                              ("hard", 45, 1);
                                              """;
    
    public static string[] GetInsertIntoStatements()
    {
        string[] statements =
        {
            InsertTeams, InsertDrivers, InsertDriverRating,
            GenerateInsertCircuitStatements(), GenerateInsertCircuitStatsStatements(), InsertTyre
        };
        return statements;
    }
    // The below populate random data for now until I have time to update this to be more accurate
    private static string GenerateInsertCircuitStatements()
    {
        StringBuilder insertStatements = new StringBuilder();

        foreach (Circuit circuit in Enum.GetValues(typeof(Circuit)))
        {
            if (insertStatements.Length > 0)
                insertStatements.AppendLine();

            int laps = GetRandomLaps();
            insertStatements.AppendLine($"INSERT INTO circuit (circuit_name, laps) VALUES ('{circuit.GetCircuitName()}', {laps});");
        }

        return insertStatements.ToString();
    }

    private static int GetRandomLaps()
    {
        Random rnd = new Random();
        return rnd.Next(40, 81); 
    }
    
    
    private static string GenerateInsertCircuitStatsStatements()
    {
        StringBuilder insertStatements = new StringBuilder();

        foreach (Circuit circuit in Enum.GetValues(typeof(Circuit)))
        {
            if (insertStatements.Length > 0)
                insertStatements.AppendLine();

            decimal avgLapTime = GetRandomDecimal(1, 2, 5); // Between 1 minute and 2 minutes (in decimal format)
            decimal avgPitStop = GetRandomDecimal(0.07m, 0.15m, 5); // Between 7 seconds and 15 seconds (in decimal format)
            int avgPitStops = GetRandomInt(1, 3); // Between 1 and 3 pit stops
            
            int circuitId = (int)circuit + 1;

            insertStatements.AppendLine($"INSERT INTO circuit_stats (circuit_id, avg_laptime, avg_pitstop, avg_pitstops) VALUES " +
                                        $"({circuitId}, {avgLapTime}, {avgPitStop}, {avgPitStops});");
        }
        return insertStatements.ToString();
    }
    private static decimal GetRandomDecimal(decimal minValue, decimal maxValue, int decimalPlaces)
    {
        Random rnd = new Random();
        decimal range = maxValue - minValue;
        decimal randomValue = minValue + Convert.ToDecimal(rnd.NextDouble()) * range;
        return Math.Round(randomValue, decimalPlaces);
    }

    private static int GetRandomInt(int minValue, int maxValue)
    {
        Random rnd = new Random();
        return rnd.Next(minValue, maxValue + 1);
    }
}