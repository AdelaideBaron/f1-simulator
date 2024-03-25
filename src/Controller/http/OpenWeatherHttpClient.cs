namespace Controller.http;

public class OpenWeatherHttpClient
{
    private HttpClient _httpClient;

    public OpenWeatherHttpClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    }

    public async Task<string> GetTodoAsync(int id)
    {
        string endpoint = $"todos/{id}";
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
}