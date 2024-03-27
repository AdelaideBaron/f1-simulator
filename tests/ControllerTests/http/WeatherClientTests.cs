using Controller.http;
using Model.dto;
using Model.@enum;

namespace ControllerTests.http;

public class WeatherClientTests
{
    [Test]
    public void GetCircuitConditionsPopulatesDataFromApi()
    {
        WeatherClient weatherClient = new WeatherClient();
        CircuitConditions circuitConditions = weatherClient.GetCircuitConditions(Circuit.Bahrain);
        Assert.NotNull(circuitConditions.Rain);
        Assert.NotNull(circuitConditions.Temp);
        Assert.That(circuitConditions.Circuit, Is.EqualTo(Circuit.Bahrain));
    }
}