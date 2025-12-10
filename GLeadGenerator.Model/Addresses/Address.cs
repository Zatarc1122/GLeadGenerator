using GLeadGenerator.Infrastructure.Domain;

namespace GLeadGenerator.Model.Addresses;

public class Address : Entity
{
    public string? Street { get; internal set; }
    public string? Suite { get; internal set; }
    public string? City { get; internal set; }
    public string? ZipCode { get; internal set; }
    public Geolocation? Geolocation { get; internal set; }

    internal Address()
    {
    }
}

