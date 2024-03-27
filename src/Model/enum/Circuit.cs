namespace Model.@enum;

// Todo refactor to use Enum.GetAttribute 

[AttributeUsage(AttributeTargets.Field)]
public class CircuitNameAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}

[AttributeUsage(AttributeTargets.Field)]
public class TemperatureAttribute : Attribute
{
    public double DefaultTempt { get; }

    public TemperatureAttribute(double temp)
    {
        DefaultTempt = temp;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class CircuitCoordinatesAttribute : Attribute
{
    public double Longitude { get; }
    public double Latitude { get; }

    public CircuitCoordinatesAttribute(double longitude, double latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}

public enum Circuit
{
    [CircuitCoordinates(9.28, 45.62)] 
    [CircuitName("Autodromo Nazionale di Monza")]
    [Temperature(25.0)]
    Monza,

    [CircuitCoordinates(50.51, 26.03)] 
    [CircuitName("Bahrain International Circuit")]
    [Temperature(30.0)] 
    Bahrain,

    [CircuitCoordinates(-1.01, 52.07)] 
    [CircuitName("Silverstone Circuit")]
    [Temperature(18.0)]
    Silverstone,

    [CircuitCoordinates(7.41, 43.73)] 
    [CircuitName("Circuit de Monaco")]
    [Temperature(22.0)]
    Monaco
}
public static class CircuitExtensions
{
    public static string GetCircuitName(this Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var field = enumType.GetField(enumValue.ToString());
        var attribute = (CircuitNameAttribute)Attribute.GetCustomAttribute(field, typeof(CircuitNameAttribute));
        return attribute?.Value; // Todo warning about null
    }
    
    public static CircuitCoordinatesAttribute GetCircuitCoordinates(this Circuit enumValue)
    {
        var enumType = enumValue.GetType();
        var field = enumType.GetField(enumValue.ToString());
        return (CircuitCoordinatesAttribute)Attribute.GetCustomAttribute(field, typeof(CircuitCoordinatesAttribute));
    }
    
    public static double GetDefaultTemperature(this Circuit enumValue)
    {
        var enumType = enumValue.GetType();
        var field = enumType.GetField(enumValue.ToString());
        var attribute = (TemperatureAttribute)Attribute.GetCustomAttribute(field, typeof(TemperatureAttribute));
        return attribute.DefaultTempt; // Return default temperature
    }

}