using Model.database.scripts;
using Model.@enum;

namespace Controller.database;

public class DatabaseClient
{
    DatabaseActions _databaseActions = new DatabaseActions();
    
    public int GetCircuitLaps(Circuit circuit)
    {
        return _databaseActions.GetQueryResultsForInt(QueryDuringRace.GetCircuitLaps(circuit))[0];
    }
}