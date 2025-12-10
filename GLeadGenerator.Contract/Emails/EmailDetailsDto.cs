namespace GLeadGenerator.Contract.Emails;

public class EmailDetailsDto
{
    public string UserFirstName { get; set; } = default!;
    public string UserLastName { get; set; } = default!;
    public string UserEmail { get; set; } = default!;
    public string? UserStreet { get; set; }
    public string? UserSuite { get; set; }
    public string? UserCity { get; set; }
    public string? UserZipcode { get; set; }

    public string? UserLongitude { get; set; }
    public string? UserLatitude { get; set; }
    public string? UserWebsite { get; set; }
    public string? UserPhone { get; set; }
}

