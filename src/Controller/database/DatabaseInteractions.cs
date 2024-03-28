namespace Model.database;

public class DatabaseInteractions // rename this 
{
 private static DatabaseClient databaseClient = new DatabaseClient();

    public static void initialiseDatabase()
    {
        databaseClient.InitialiseDatabase();
    }

    public static void setupSimulation()
    {
        
    }
}