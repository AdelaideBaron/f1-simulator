using Model.database.scripts;

namespace ModelTests.database;

public class CreateTableTests
{
    [Test]
    public void GetCreateTableStatementsReturnsCreateStatements()
    {
        foreach (string statement in CreateTable.GetCreateTableStatements())
        {
            Assert.That(statement.Contains("CREATE TABLE IF NOT EXISTS"));
        }
    }
}
