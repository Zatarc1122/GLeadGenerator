using GLeadGenerator.Integration.Contract.JsonPlaceholder.Users;

namespace GLeadGenerator.Integration.Contract.JsonPlaceholder;

public class GetUsersResponse
{
    public IEnumerable<UserDto>? Users { get; set; }
}
