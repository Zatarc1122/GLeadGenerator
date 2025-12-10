namespace GLeadGenerator.Contract.Users;

public interface IUserQuery
{
    Task<DateTime?> GetDateCreatedByEmailAsync(string email);
    Task<IEnumerable<BusinessUserDto>> GetBusinessUsersAsync();
}

