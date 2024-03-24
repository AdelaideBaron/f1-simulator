using Model.Database.InitDatabase;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Model.Database;

public class DatabaseClient
{
    readonly string connectionString = "server=localhost;user=root;password=password;database=f1_simulator";

    public void InitialiseDatabase()
    {
        string[] scripts = CreateTables.getCreateTableStatements();
        foreach (string statement in scripts)
        {
            CreateTable(statement);
        }
    }

    private void CreateTable(string statement)
    {
        string tableName = ExtractTableName(statement);
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(statement, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Table {tableName} created successfully");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    private string ExtractTableName(string input)
    {
        string pattern = @"CREATE\s+TABLE\s+IF\s+NOT\s+EXISTS\s+(\w+)";
        Match match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        else
        {
            return string.Empty;
        }
    }
}