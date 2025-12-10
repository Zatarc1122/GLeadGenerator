using GLeadGenerator.Integration.Service.Settings;
using System.Web;

namespace GLeadGenerator.Integration.Service.HttpClients.JsonPlaceholder;

public class JsonPlaceholderHttpClient
{
    private readonly JsonPlaceholderApiSettings _jsonPlaceholderApiSettings;
    private readonly HttpClient _httpClient;

    public JsonPlaceholderHttpClient(JsonPlaceholderApiSettings jsonPlaceholderApiSettings,
        HttpClient httpClient)
    {
        _jsonPlaceholderApiSettings = jsonPlaceholderApiSettings;
        _httpClient = httpClient;
    }

    public async Task<string> GetUsersAsTextAsync(GetUsersRequest request)
    {
        var requestUrl = ResolveRequestUrl(request);
        var response = await _httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        //TODO: log the exception
        throw new Exception("Failed to get data from JsonPlaceholder.");
    }

    private string ResolveRequestUrl(GetUsersRequest request)
    {
        var name = $"{request.FirstName}+{request.LastName}";
        var url = $"{_jsonPlaceholderApiSettings.BaseUrl}?email={request.Email}&name={name}";

        return url;
    }
}
