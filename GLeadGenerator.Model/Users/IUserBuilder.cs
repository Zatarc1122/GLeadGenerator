namespace GLeadGenerator.Model.Users;

public interface IUserBuilder
{
    UserBuilder WithBasicInfo(string firstName, string lastName, string email);
    UserBuilder WithBasicUser(User user);
    UserBuilder WithContactDetails(string? phone, string? website);
    UserBuilder WithAddress(string? street, string? suite, string? city, string? zipcode);
    UserBuilder WithGeolocation(string? latitude, string? longitude);
    UserBuilder WithCompany(string? name, string? catchPhrase, string? businessStrategy);
    UserBuilder WithIntegrationData(int? integrationId, string? username);
    User Build();
}

