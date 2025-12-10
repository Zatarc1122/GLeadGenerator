using GLeadGenerator.Integration.Service.JsonPlaceholder;
using GLeadGenerator.Integration.Service.HttpClients.JsonPlaceholder;
using GLeadGenerator.Integration.Contract.JsonPlaceholder;
using System.Text.Json;
using GLeadGenerator.Integration.Contract.JsonPlaceholder.Users;

namespace GLeadGenerator.Integration.Service.JsonPlaceholder;

public class JsonPlaceholderService : IJsonPlaceholderService
{
    private readonly JsonPlaceholderHttpClient _jsonPlaceholderHttpClient;

    public JsonPlaceholderService(JsonPlaceholderHttpClient jsonPlaceholderHttpClient)
    {
        _jsonPlaceholderHttpClient = jsonPlaceholderHttpClient;
    }

    public async Task<GetUsersResponse> GetUsersAsync(GetUsersRequest request)
    {
        var textUsers = await _jsonPlaceholderHttpClient.GetUsersAsTextAsync(request);

        var users = JsonSerializer.Deserialize<IEnumerable<UserDto>>(textUsers);

        var response = new GetUsersResponse { Users = users };

        return response;
    }
}
