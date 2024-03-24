using Model.@enum;

namespace ModelTests.entity;

public class CircuitTests
{
    [Test]
    public void CircuitExtensionsRetrievesExpectedDescription()
    {
        string actual = CircuitExtensions.GetCircuitName(Circuit.Bahrain);
        Assert.That(actual, Is.EqualTo("Bahrain International Circuit"));
    }
}