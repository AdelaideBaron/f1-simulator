using Model.@enum;
using Model.pojo;
using Newtonsoft.Json;

namespace Controller.http;

public class WeatherClient
{

   public void GetCircuitConditions(Circuit circuit)
   {
      // make an object for the circuit conditions 
      // use the below method for it 
   }
   public void GetCircuitWeather(Circuit circuit)
   {
      OpenWeatherHttpClient client = new OpenWeatherHttpClient();
      try
      {
         CircuitCoordinatesAttribute circuitCoordinates = CircuitExtensions.GetCircuitCoordinates(circuit);
         string apiResponse = client.GetTodoAsync(circuitCoordinates.Latitude, circuitCoordinates.Longitude).Result;
         WeatherData? weatherData = JsonConvert.DeserializeObject<WeatherData>(apiResponse);
         Console.WriteLine(weatherData.name);
      }
      catch (Exception ex)
      {
         Console.WriteLine($"An error occurred: {ex.Message}");
      }
      finally
      {
         client.Dispose();
      }
   }
   
   
}