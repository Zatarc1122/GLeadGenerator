using GLeadGenerator.Infrastructure.Settings;
using GLeadGenerator.Model.Users;
using System.Data;
using System.Data.SqlClient;

namespace GLeadGenerator.Repository.Users;

public class UserRepository : IUserRepository
{
    private readonly ConnectionStrings _connectionStrings;

    public UserRepository(ConnectionStrings connectionStrings)
    {
        _connectionStrings = connectionStrings;
    }

    public async Task InsertAsync(User user)
    {
        using SqlConnection connection = new(_connectionStrings.LeadGenerator);
        await connection.OpenAsync();

        using SqlCommand command = new("User.spInsertUser", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@FirstName", user.FirstName);
        command.Parameters.AddWithValue("@LastName", user.LastName);

        command.Parameters.AddWithValue("@Email", user.ContactDetails.Email);
        command.Parameters.AddWithValue("@Phone", user.ContactDetails?.Phone);
        command.Parameters.AddWithValue("@Website", user.ContactDetails?.Website);

        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@IntegrationId", user.IntegrationId);

        command.Parameters.AddWithValue("@Street", user.Address?.Street);
        command.Parameters.AddWithValue("@Suite", user.Address?.Suite);
        command.Parameters.AddWithValue("@City", user.Address?.City);
        command.Parameters.AddWithValue("@ZipCode", user.Address?.ZipCode);

        command.Parameters.AddWithValue("@Latitude", user.Address?.Geolocation?.Latitude);
        command.Parameters.AddWithValue("@Longitude", user.Address?.Geolocation?.Longitude);

        command.Parameters.AddWithValue("@CompanyName", user.Company?.Name);
        command.Parameters.AddWithValue("@CatchPhrase", user.Company?.CatchPhrase);
        command.Parameters.AddWithValue("@BusinessStrategy", user.Company?.BusinessStrategy);

        command.Parameters.AddWithValue("@DateCreated", DateTime.UtcNow);

        command.ExecuteNonQuery();
    }
}

