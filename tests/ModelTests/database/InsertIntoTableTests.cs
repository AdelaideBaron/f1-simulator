using Model.database.scripts;

namespace ModelTests.database;

public class InsertIntoTableTests
{
    [Test]
    public void GetInsertIntoStatementsReturnsInsertIntoStatements()
    {
        foreach (string statement in InsertIntoTable.GetInsertIntoStatements())
        {
            Assert.That(statement.Contains("INSERT INTO"));
        }
    }
}