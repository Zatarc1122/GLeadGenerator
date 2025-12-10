namespace GLeadGenerator.Contract.Users;

public interface IUserService
{
    Task<IEnumerable<BusinessUserDto>> GetBusinessUsersAsync();
    Task SaveUserAsync(UserRequestDto request);
}

