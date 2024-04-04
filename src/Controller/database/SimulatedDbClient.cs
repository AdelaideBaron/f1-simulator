using Model.database;
using Model.database.scripts;

namespace Controller.database;

public class SimulatedDbClient
{
    static DatabaseClient _databaseClient = new DatabaseClient();

    public static void InitSimRaceLiveTable(Guid raceId)
    {
        _databaseClient.RunStatement(InsertDuringRace.GetSimulatedRaceLiveInitialiseStatement(raceId.ToString()));
    }
}