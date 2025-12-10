namespace GLeadGenerator.Integration.Contract.JsonPlaceholder;

public interface IJsonPlaceholderService
{
    Task<GetUsersResponse> GetUsersAsync(GetUsersRequest request);
}
