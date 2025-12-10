using GLeadGenerator.Contract.Addresses;
using GLeadGenerator.Contract.Companies;

namespace GLeadGenerator.Contract.Users;

public class UserIntegrationDataDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public AddressDto? Address { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public CompanyDto? Company { get; set; }
}
