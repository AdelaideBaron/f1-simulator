using Model.@enum;

namespace ModelTests.entity;

public class CircuitTests
{
    // Todo update tests to be expected : actual 
    [Test]
    public void CircuitExtensionsRetrievesExpectedDescription()
    {
        string actual = CircuitExtensions.GetCircuitName(Circuit.Bahrain);
        Assert.That(actual, Is.EqualTo("Bahrain International Circuit"));
    }
    
    [Test]
    public void CircuitExtensionsRetrievesExpectedCoordinates()
    {
        CircuitCoordinatesAttribute actual = CircuitExtensions.GetCircuitCoordinates(Circuit.Monza);
        Assert.That(actual.Latitude, Is.EqualTo(45.62));
        Assert.That(actual.Longitude, Is.EqualTo(9.28));
    }
    
    [Test]
    public void CircuitExtensionsRetrievesDefaultTemp()
    {
        double actual = CircuitExtensions.GetDefaultTemperature(Circuit.Silverstone);
        Assert.That(actual, Is.EqualTo(18.0));
    }
}