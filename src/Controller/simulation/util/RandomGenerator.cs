namespace Controller.simulation.util;

public class RandomGenerator
{
    static Random rnd = new Random();
    
    public static decimal GetRandomDecimal(decimal minValue, decimal maxValue, int decimalPlaces)
    {
        decimal range = maxValue - minValue;
        decimal randomValue = minValue + Convert.ToDecimal(rnd.NextDouble()) * range;
        return Math.Round(randomValue, decimalPlaces);
    }

    public static int GetRandomInt(int minValue, int maxValue)
    {
        return rnd.Next(minValue, maxValue + 1);
    }

    public static bool GetRandomBool()
    {
        return rnd.Next(2) == 0;
    }

    public static T GetRandomChoice<T>(List<T> listOfItems) // unit test this 
    {
        if (listOfItems == null || listOfItems.Count == 0)
        {
            throw new ArgumentException("The list of items is null or empty.");
        }
        int randomIndex = rnd.Next(listOfItems.Count);
        return listOfItems[randomIndex];
    }
}