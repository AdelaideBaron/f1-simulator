using System.Text;
using Model.@enum;

namespace Model.database.scripts;

public class CreateTable
{
    // Todo re-review these scripts 
    private const string CreateTeam = """
                                      CREATE TABLE IF NOT EXISTS team
                                      (
                                         team_id int AUTO_INCREMENT,
                                         team_name ENUM("Mercedes","Red Bull Racing", "Ferrari", "McLaren", "Aston Martin", "RB", "Haas F1 Team", "Williams", "Kick Sauber", "Alpine"),
                                         PRIMARY KEY (team_id)
                                      );
                                      """;

    private const string CreateDriver = """
                                        CREATE TABLE IF NOT EXISTS driver
                                        ( driver_number int, team_id int, driver_name VARCHAR(50),
                                        PRIMARY KEY (driver_number),
                                        FOREIGN KEY(team_id) REFERENCES team(team_id)
                                         );
                                        """;

    private const string CreateDriverRating = """
                                              CREATE TABLE IF NOT EXISTS driver_rating
                                              ( driver_number int UNIQUE, avg_position int,
                                              consistency DECIMAL(3, 2), -- a decimal representation of %age, how consistent they are with position - e.g. if always gets pole position - consistency = 1.0000
                                              avg_distance_to_fastest_laptime DECIMAL(6, 4), -- how far off the fastest lap time they are on average per race
                                              FOREIGN KEY(driver_number) REFERENCES driver(driver_number)
                                               );
                                              """;

    private const string CreateCircuitStats = """

                                              CREATE TABLE IF NOT EXISTS circuit_stats
                                              (
                                                  circuit_id INT,
                                                  avg_laptime DECIMAL(6, 5), -- m:SSmSms - e.g 1:19.915 = 1.19915
                                                  avg_pitstop DECIMAL(6, 5), -- m:SSmSms - e.g 1:19.915 = 1.19915
                                                  avg_pitstops INT, -- amount of pitstops, e.g. onestop race = 1
                                                  FOREIGN KEY(circuit_id) REFERENCES circuit(circuit_id)
                                              );
                                              """;

    private const string CreateTyre = """
                                      CREATE TABLE IF NOT EXISTS tyre
                                      (
                                          tyre_id INT AUTO_INCREMENT,
                                          compound ENUM("soft", "medium", "hard", "intermediate", "wet"),
                                          max_laps INT,
                                          speed_ratio DECIMAL(2,1), -- a decimal representation speed, fastest (softs) = 1, slower will be less than
                                          PRIMARY KEY (tyre_id)
                                      );
                                      """;

    private const string CreateSimulatedRaceLive = """
                                                   CREATE TABLE IF NOT EXISTS simulated_race_live -- table to store the drivers and their race progression
                                                   (
                                                       simulated_race_id VARCHAR(36),
                                                       driver_number INT UNIQUE, -- driver entry to be updated, only one entry per driver
                                                       position TINYINT UNSIGNED NOT NULL CHECK (position >= 0 AND position <= 20),
                                                       status ENUM("is_driving", "race_ban", "dnf", "no_start"),
                                                       lap_no INT,
                                                       laptime DECIMAL(6, 5), -- m:SSmSms - e.g 1:19.915 = 1.19915,
                                                       tyre_id INT,
                                                       tyre_age INT, -- in laps
                                                       pit_stops INT,
                                                        PRIMARY KEY (simulated_race_id),
                                                       FOREIGN KEY(driver_number) REFERENCES driver(driver_number),
                                                       FOREIGN KEY(tyre_id) REFERENCES tyre(tyre_id)
                                                   );
                                                   """;

    private const string CreateSimulatedRaceAudit = """
                                                    CREATE TABLE IF NOT EXISTS simulated_race_audit -- table to store the drivers and their race progression
                                                    (
                                                        simulated_race_id VARCHAR(36),
                                                        driver_number INT UNIQUE, -- driver entry to be updated, only one entry per driver
                                                        position TINYINT UNSIGNED NOT NULL CHECK (position >= 0 AND position <= 20),
                                                        status ENUM("is_driving", "race_ban", "dnf", "no_start"),
                                                        lap_no INT,
                                                        laptime DECIMAL(6, 5), -- m:SSmSms - e.g 1:19.915 = 1.19915,
                                                        tyre_id INT,
                                                        tyre_age INT, -- in laps
                                                        pit_stops INT,
                                                        FOREIGN KEY(simulated_race_id) REFERENCES simulated_race_live(simulated_race_id)
                                                    );
                                                    """;

    private const string CreateSimulatedRaceResults = """
                                                      CREATE TABLE IF NOT EXISTS simulated_race_results-- table to store the drivers and their race progression
                                                      (
                                                          simulated_race_id VARCHAR(36),
                                                          winning_driver_number INT UNIQUE, -- driver entry to be updated, only one entry per driver
                                                          position_results_id INT,
                                                          fastest_lap DECIMAL(6, 5), -- m:SSmSms - e.g 1:19.915 = 1.19915,
                                                          incidents INT,
                                                          PRIMARY KEY (simulated_race_id),
                                                          FOREIGN KEY(winning_driver_number) REFERENCES driver(driver_number)
                                                      );
                                                      """;

    private const string CreateSimulatedRacePositionResults = """
                                                              CREATE TABLE IF NOT EXISTS simulated_race_position_results -- table to store the drivers and their race progression
                                                              (
                                                                  simulated_race_id VARCHAR(36),
                                                                  driver_number INT UNIQUE, -- driver entry to be updated, only one entry per driver
                                                                  position INT UNIQUE,
                                                                  FOREIGN KEY(driver_number) REFERENCES driver(driver_number),
                                                                  FOREIGN KEY(simulated_race_id) REFERENCES simulated_race_results(simulated_race_id)
                                                              );
                                                              """;

    private const string CreateSimulatedRaceConditions = """
                                                         CREATE TABLE IF NOT EXISTS simulated_race_conditions
                                                         (
                                                         simulated_race_id VARCHAR(36) NOT NULL,
                                                         circuit_id INT NOT NULL, 
                                                         temp DEC NOT NULL, 
                                                         raining BOOL NOT NULL
                                                         );
                                                         """;

    public static string[] GetCreateTableStatements()
    {
        string[] statements =
        {
            CreateTeam, CreateDriver, CreateDriverRating, CreateCircuitTable(), CreateCircuitStats, CreateTyre,
            CreateSimulatedRaceLive, CreateSimulatedRaceAudit, CreateSimulatedRaceResults,
            CreateSimulatedRacePositionResults, CreateSimulatedRaceConditions
        };
        return statements;
    }

    private static string CreateCircuitTable()
    {
        StringBuilder enumValuesBuilder = new StringBuilder();
        foreach (Circuit circuit in Enum.GetValues(typeof(Circuit)))
        {
            if (enumValuesBuilder.Length > 0)
                enumValuesBuilder.Append(", ");

            enumValuesBuilder.Append($"'{circuit.GetCircuitName()}'");
        }

        string enumValues = enumValuesBuilder.ToString();

        return $@"
        CREATE TABLE IF NOT EXISTS circuit
        (
            circuit_id INT AUTO_INCREMENT,
            circuit_name ENUM({enumValues}),
            laps INT,
            PRIMARY KEY (circuit_id)
        );";
    }
    
    
}