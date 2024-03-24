namespace Model.@enum;

[AttributeUsage(AttributeTargets.Field)]
public class CircuitNameAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}

public enum Circuit
{
    [CircuitName("Autodromo Nazionale di Monza")]
    Monza,

    [CircuitName("Bahrain International Circuit")]
    Bahrain,
    
    [CircuitName("Silverstone Circuit")]
    Silverstone,

    [CircuitName("Circuit de Monaco")]
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
}