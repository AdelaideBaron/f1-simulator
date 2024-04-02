using System.Text.RegularExpressions;
using Model.database.scripts;
using MySql.Data.MySqlClient;

namespace Controller.database;

public class DatabaseActions
{
    // Todo this code needs optimising, as repeating the create table everytime is time consuming - e.g. opening and closing the connection. 
    readonly string _connectionString = "server=localhost;user=root;password=password;database=f1_simulator";

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

    public List<int> GetQueryResultsForInt(string query)
    {
        return RunSqlQueryForInt(query);
    }
    
    private List<int> RunSqlQueryForInt(string query)
    {
        Console.WriteLine(query);
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                List<int> results = new List<int>();
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Execute the SELECT query to retrieve data
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Loop through the results and process them
                        while (reader.Read())
                        {
                            int intValue = reader.GetInt32(0); // Retrieve the integer value from the reader
                            results.Add(intValue); // Add the integer value to the results list
                        }
                    }
                }

                return results;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
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