using System.Text.Json.Serialization;

namespace GLeadGenerator.Integration.Contract.JsonPlaceholder.Company;

public class CompanyDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("catchPhrase")]
    public string? CatchPhrase { get; set; }

    [JsonPropertyName("bs")]
    public string? Bs { get; set; }
}
