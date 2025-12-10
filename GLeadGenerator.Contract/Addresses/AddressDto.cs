using GLeadGenerator.Contract.Geolocations;

namespace GLeadGenerator.Contract.Addresses;

public class AddressDto
{
    public string? Street { get; set; }
    public string? Suite { get; set; }
    public string? City { get; set; }
    public string? Zipcode { get; set; }
    public GeoDto? Geo { get; set; }
}
