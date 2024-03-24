using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Model.Database;

public class DatabaseClient
{
    // Todo this code needs optimising, as repeating the create table everytime is time consuming - e.g. opening and closing the connection. 
    readonly string connectionString = "server=localhost;user=root;password=password;database=f1_simulator";

    public void InitialiseDatabase()
    {
        foreach (string statement in InitDatabase.CreateTable.getCreateTableStatements())
        {
            RunSqlStatement(statement);
        }

        foreach (string statement in InitDatabase.InsertIntoTable.GetInsertIntoStatements())
        {
            RunSqlStatement(statement);
        }
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
                    GetLogMessage(statement);
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

            if (firstWord == "CREATE")
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