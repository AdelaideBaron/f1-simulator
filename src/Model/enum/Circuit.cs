namespace Model.@enum;

[AttributeUsage(AttributeTargets.Field)]
public class CircuitNameAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}

public enum Circuit
{
    [CircuitName("Autodromo Nazionale di Monza")]
    MONZA,

    [CircuitName("Bahrain International Circuit")]
    BAHRAIN,
    
    [CircuitName("Silverstone Circuit")]
    SILVERSTONE,

    [CircuitName("Circuit de Monaco")]
    MONACO
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
}