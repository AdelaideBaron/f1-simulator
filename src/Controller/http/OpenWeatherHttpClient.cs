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
    
    

    public async Task<string> GetTodoAsync(int id) // TODO rename
    {
        string endpoint = GetEndpoint();
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new HttpRequestException($"Failed to fetch todo item. Status code: {response.StatusCode}");
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    private string GetEndpoint() // todo pass in the values
    {
        double lat = 43.73;
        double lon = 7.42;
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