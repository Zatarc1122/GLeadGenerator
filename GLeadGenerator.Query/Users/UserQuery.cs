using GLeadGenerator.Contract.Users;
using GLeadGenerator.Infrastructure.Settings;
using System.Data;
using System.Data.SqlClient;

namespace GLeadGenerator.Query.Users;

public class UserQuery : IUserQuery
{
    private readonly ConnectionStrings _connectionStrings;

    public UserQuery(ConnectionStrings connectionStrings)
    {
        _connectionStrings = connectionStrings;
    }

    public async Task<DateTime?> GetDateCreatedByEmailAsync(string email)
    {
        using SqlConnection connection = new(_connectionStrings.LeadGenerator);
        await connection.OpenAsync();

        using SqlCommand command = new("User.spGetDateCreatedByEmail", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Email", email);

        var queryResult = (DateTime?)await command.ExecuteScalarAsync();

        return queryResult;
    }

    public async Task<IEnumerable<BusinessUserDto>> GetBusinessUsersAsync()
    {
        var businessUsers = new List<BusinessUserDto>();

        using SqlConnection connection = new(_connectionStrings.LeadGenerator);
        await connection.OpenAsync();

        using SqlCommand command = new("SELECT * FROM [User].vwBusinessUsers", connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var businessUser = new BusinessUserDto
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Email = (string)reader["Email"],
                Phone = reader["Phone"]?.ToString(),
                Website = reader["Website"]?.ToString()
            };
            businessUsers.Add(businessUser);
        }

        return businessUsers;
    }
}
