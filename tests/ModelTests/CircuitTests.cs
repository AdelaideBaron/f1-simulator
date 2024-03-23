using Model.@enum;

namespace ModelTests;

public class CircuitTests
{
    [Test]
    public void CircuitExtensionsRetrievesExpectedDescription()
    {
        string actual = CircuitExtensions.GetCircuitName(Circuit.BAHRAIN);
        Assert.That(actual, Is.EqualTo("Bahrain International Circuit"));
    }
}