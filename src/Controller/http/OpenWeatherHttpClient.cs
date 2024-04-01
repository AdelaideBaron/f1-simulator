using Microsoft.Extensions.Configuration;

namespace Controller.http;

public class OpenWeatherHttpClient
{
    private HttpClient _httpClient;

    public OpenWeatherHttpClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
    }
   // todo review and rename the below  
    public async Task<string> GetTodoAsync( double lat, double lon) // TODO rename
    {
        string endpoint = GetEndpoint(lat, lon);
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        { // todo review the below
            throw new HttpRequestException($"Failed to fetch todo item. Status code: {response.StatusCode}");
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
    
    private string GetEndpoint(double lat, double lon) 
    {
        string apiKey = GetApiKey();
        return $"?lat={lat}&lon={lon}&appid={apiKey}";
    }
    
    private static string? GetApiKey() 
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<OpenWeatherHttpClient>()
            .Build();
            
        return config["OpenWeatherApiKey"];
    }
}