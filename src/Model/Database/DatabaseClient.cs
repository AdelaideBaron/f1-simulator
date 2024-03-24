using MySql.Data.MySqlClient;
namespace Model.Database;

public class DatabaseClient
{
    readonly string _connectionString = "server=localhost;user=root;password=password;database=f1_simulator";

    public void PerformQuery(){
        ConnectToDatabase();
        }
    
    private void ConnectToDatabase()
    {
        MySqlConnection connection = new MySqlConnection(_connectionString);
        try
        {
            connection.Open();
            Console.WriteLine("Connection successful!");

            // Perform database operations here

        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }
}