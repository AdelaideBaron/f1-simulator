using Model.dto;
using Model.@enum;
using Model.pojo;
using Newtonsoft.Json;

namespace Controller.http;

public class WeatherClient
{

   public CircuitConditions GetCircuitConditions(Circuit circuit)
   {
      WeatherData weatherData = GetCircuitWeather(circuit);
      
      double rain = (weatherData.rain == null) ? 0 : weatherData.rain._1h;
      double temp = (weatherData.main.temp == null) ? circuit.GetDefaultTemperature() : weatherData.main.temp;
    
      CircuitConditions circuitConditions = new CircuitConditions
      {
         Circuit = circuit,
         Rain = rain,
         Temp = temp
      };
      return circuitConditions;
   }
   public WeatherData GetCircuitWeather(Circuit circuit)
   {
      OpenWeatherHttpClient client = new OpenWeatherHttpClient();
      try
      {
         CircuitCoordinatesAttribute circuitCoordinates = circuit.GetCircuitCoordinates();
         string apiResponse = client.GetTodoAsync(circuitCoordinates.Latitude, circuitCoordinates.Longitude).Result;
         WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(apiResponse) ?? throw new InvalidOperationException();
         return weatherData;
      }
      catch (Exception ex)
      {
         Console.WriteLine($"An error occurred: {ex.Message}");
         throw new InvalidOperationException("Failed to fetch weather data for the circuit.", ex);
      }
      finally
      {
         client.Dispose();
      }
   }

   
   
}