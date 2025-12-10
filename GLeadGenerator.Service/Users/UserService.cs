using GLeadGenerator.Contract.Emails;
using GLeadGenerator.Contract.Users;
using GLeadGenerator.Model.Users;
using GLeadGenerator.Service.Settings;
using System.Net;
using System.Net.Http.Json;

namespace GLeadGenerator.Service.Users;

public class UserService : IUserService
{
    private readonly IntegrationApiSettings _integrationApiSettings;
    private readonly HttpClient _httpClient;

    private readonly UserManager _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IUserQuery _userQuery;

    private readonly IEmailService _emailService;

    public UserService(IntegrationApiSettings integrationApiSettings,
        HttpClient httpClient,
        IUserRepository userRepository,
        UserManager userManager,
        IUserQuery userQuery,
        IEmailService emailService)
    {
        _integrationApiSettings = integrationApiSettings;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_integrationApiSettings.BaseUrl);
        _userRepository = userRepository;
        _userManager = userManager;
        _userQuery = userQuery;
        _emailService = emailService;
    }

    public async Task<IEnumerable<BusinessUserDto>> GetBusinessUsersAsync()
    {
        return await _userQuery.GetBusinessUsersAsync();
    }

    public async Task SaveUserAsync(UserRequestDto request)
    {
        var user = await _userManager
            .CreateBasicAsync(request.FirstName, request.LastName, request.Email);

        var integrationRequestUrl = ResolveIntegrationUrl(request);

        var integrationResponse = await _httpClient.GetAsync(integrationRequestUrl);

        if (integrationResponse.StatusCode == HttpStatusCode.OK)
        {
            var userIntegrationData = await integrationResponse.Content.ReadFromJsonAsync<UserIntegrationDataDto>();

            _userManager.FillWithIntegrationData(user, userIntegrationData!);
        }

        await _userRepository.InsertAsync(user);

        await _emailService.SendAsync(new EmailDetailsDto
        {
            UserFirstName = request.FirstName,
            UserLastName = request.LastName,
            UserEmail = request.Email,
            UserStreet = user.Address?.Street,
            UserSuite = user.Address?.Suite,
            UserCity = user.Address?.City,
            UserZipcode = user.Address?.ZipCode,
            UserLatitude = user.Address?.Geolocation?.Latitude,
            UserLongitude = user.Address?.Geolocation?.Longitude,
            UserPhone = user.ContactDetails?.Phone,
            UserWebsite = user.ContactDetails?.Website
        });
    }

    private string ResolveIntegrationUrl(UserRequestDto request)
    {
        var firstName = request.FirstName.Replace(" ", "+");
        var lastName = request.LastName.Replace(" ", "+");
        var url =
            $"{_integrationApiSettings.Users}?firstName={firstName}&lastName={lastName}&email={request.Email}";

        return url;
    }
}

