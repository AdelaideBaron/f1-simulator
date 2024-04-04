using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Model.database.scripts;
using Model.database.scripts.init;

namespace Model.database;

public class DatabaseClient
{
    // Todo this code needs optimising, as repeating the create table everytime is time consuming - e.g. opening and closing the connection. 
    readonly string _connectionString = "server=localhost;user=root;password=password;database=f1_simulator";

    public void InitialiseDatabase()
    {
        RunStatements(CreateTable.GetCreateTableStatements());
        RunStatements(InsertIntoTable.GetInsertIntoStatements());
        RunStatements(CreateTrigger.GetCreateTriggerStatements());
    }

    public void RunStatement(string statement)
    {
        RunSqlStatement(statement);
    }

    private void RunStatements(string[] statements)
    {
        foreach (string statement in statements)
        {
            RunSqlStatement(statement);
        }
    }

    private void RunSqlStatement(string statement)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
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
                if (words.Length >= 2 && words[1].ToUpper() == "TABLE")
                {
                    string pattern = @"CREATE\s+TABLE\s+IF\s+NOT\s+EXISTS\s+(\w+)";
                    string tableName = ExtractTableName(input, pattern);
                    Console.WriteLine($"CREATE TABLE {tableName} created SUCCESSFULLY");
                }
                else if (words.Length >= 2 && words[1].ToUpper() == "TRIGGER")
                {
                    string pattern = @"CREATE\s+TRIGGER\s+(\w+)";
                    string triggerName = ExtractTableName(input, pattern);
                    Console.WriteLine($"CREATE TRIGGER {triggerName} created SUCCESSFULLY");
                }
            }
            else if (firstWord == "INSERT")
            {
                string pattern = @"INSERT\s+INTO\s+(\w+)";
                string tableName = ExtractTableName(input, pattern);
                Console.WriteLine($"INSERT INTO {tableName} SUCCESSFULLY");
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