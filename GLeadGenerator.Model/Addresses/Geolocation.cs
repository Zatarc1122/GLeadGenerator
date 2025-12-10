using GLeadGenerator.Infrastructure.Domain;

namespace GLeadGenerator.Model.Addresses;

public class Geolocation : Entity
{
    public string? Latitude { get; internal set; }
    public string? Longitude { get; internal set; }

    internal Geolocation()
    {
    }
}

