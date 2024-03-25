

namespace Controller.http;

public class WeatherClient
{
   public void GetWeatherFromApi()
   {
      OpenWeatherHttpClient client = new OpenWeatherHttpClient();
      try
      {
         string todo = client.GetTodoAsync(1).Result;
         Console.WriteLine(todo);
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