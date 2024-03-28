using System.Text.RegularExpressions;
using Model.database.scripts;
using MySql.Data.MySqlClient;

namespace Controller.database;

public class DatabaseClient
{
    // Todo this code needs optimising, as repeating the create table everytime is time consuming - e.g. opening and closing the connection. 
    readonly string connectionString = "server=localhost;user=root;password=password;database=f1_simulator";

    public void InitialiseDatabase()
    {
        foreach (string statement in CreateTable.GetCreateTableStatements())
        {
            RunSqlStatement(statement);
        }

        foreach (string statement in InsertIntoTable.GetInsertIntoStatements())
        {
            RunSqlStatement(statement);
        }
        
        foreach (string statement in CreateTrigger.GetTriggers())
        {
            RunSqlStatement(statement);
        }
        
        // also add the trigger. 
    }

    private void RunSqlStatement(string statement)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(statement, connection))
                {
                    command.ExecuteNonQuery();
                    GetLogMessage(statement); // update get log message 
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    private void GetLogMessage(string input)
    {
        string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length > 0)
        {
            string firstWord = words[0].ToUpper();

            if (firstWord == "CREATE") // need to update as won't work for trigger, but will still fall into here 
            {
                string pattern = @"CREATE\s+TABLE\s+IF\s+NOT\s+EXISTS\s+(\w+)";
                string tableName = ExtractTableName(input, pattern);
                Console.WriteLine($"CREATE {tableName} created SUCCESSFUL");
            }
            else if (firstWord == "INSERT")
            {
                string pattern = @"INSERT\s+INTO\s+(\w+)";
                string tableName = ExtractTableName(input, pattern);
                Console.WriteLine($"INSERT INTO {tableName} SUCCESSFUL");
            }
        }
        else
        {
            Console.WriteLine("Input string is empty.");
        }
    }

    private string ExtractTableName(string input, string pattern)
    {
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