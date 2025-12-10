using GLeadGenerator.Integration.Contract.JsonPlaceholder.Address.Geo;
using System.Text.Json.Serialization;

namespace GLeadGenerator.Integration.Contract.JsonPlaceholder.Address;

public class AddressDto
{
    [JsonPropertyName("street")]
    public string? Street { get; set; }

    [JsonPropertyName("suite")]
    public string? Suite { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("zipcode")]
    public string? Zipcode { get; set; }

    [JsonPropertyName("geo")]
    public GeoDto? Geo { get; set; }
}
