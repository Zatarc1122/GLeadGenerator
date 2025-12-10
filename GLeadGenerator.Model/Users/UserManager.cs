using GLeadGenerator.Contract.Users;
using GLeadGenerator.Infrastructure.Domain;

namespace GLeadGenerator.Model.Users;

public class UserManager
{
    private readonly IUserBuilder _userBuilder;
    private readonly IUserQuery _userQuery;

    public UserManager(
        IUserBuilder userBuilder,
        IUserQuery userQuery)
    {
        _userBuilder = userBuilder;
        _userQuery = userQuery;
    }

    public async Task<User> CreateBasicAsync(string firstName, string lastName, string email)
    {
        var lastSaved = await _userQuery.GetDateCreatedByEmailAsync(email);
        
        if (DateTime.UtcNow.AddMinutes(-1) < lastSaved)
        {
            throw new BusinessException(
                "Adding duplicate user records within 1 minute is not permitted",
                "User.DuplicateNotPermitted",
                (DateTime)lastSaved);
        }

        var basicUser = _userBuilder
            .WithBasicInfo(firstName, lastName, email)
            .Build();

        return basicUser;
    }

    public User FillWithIntegrationData(User basicUser, UserIntegrationDataDto integrationData)
    {
        basicUser = _userBuilder
            .WithBasicUser(basicUser)
            .WithContactDetails(
                integrationData.Phone,
                integrationData.Website)
            .WithAddress(
                integrationData.Address?.Street,
                integrationData.Address?.Suite,
                integrationData.Address?.City,
                integrationData.Address?.Zipcode)
            .WithGeolocation(
                integrationData.Address?.Geo?.Lat,
                integrationData.Address?.Geo?.Lng)
            .WithCompany(
                integrationData.Company?.Name,
                integrationData.Company?.CatchPhrase,
                integrationData.Company?.Bs)
            .WithIntegrationData(
                integrationData.Id,
                integrationData.Username)
            .Build();

        return basicUser;
    }
}

