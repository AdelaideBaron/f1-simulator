namespace Model.database.scripts;

public class CreateTrigger
{
    private const string CreateTriggerOnSimulatedRaceLive =  @"
            CREATE TRIGGER after_simulated_race_live_update
            AFTER UPDATE ON simulated_race_live
            FOR EACH ROW
            BEGIN
                IF OLD.driver_number IS NOT NULL THEN
                    INSERT INTO simulated_race_audit 
                    (simulated_race_id, driver_number, position, status, lap_no, laptime, tyre_id, tyre_age, pit_stops)
                    VALUES 
                    (OLD.simulated_race_id, OLD.driver_number, OLD.position, OLD.status, OLD.lap_no, OLD.laptime, OLD.tyre_id, OLD.tyre_age, OLD.pit_stops);
                END IF;
            END;";

    public static string[] GetTriggers()
    {
        string[] statements = { CreateTriggerOnSimulatedRaceLive };
        return statements;
    }
}