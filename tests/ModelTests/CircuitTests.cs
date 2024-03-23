using Model.@enum;

namespace ModelTests;

public class CircuitTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CircuitExtensionsRetrievesExpectedDescription()
    {
        string actual = CircuitExtensions.GetCircuitName(Circuit.BAHRAIN);
        Assert.AreEqual("Bahrain International Circuit", actual);
    }
}