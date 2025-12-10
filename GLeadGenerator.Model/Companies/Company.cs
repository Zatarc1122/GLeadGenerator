using GLeadGenerator.Infrastructure.Domain;

namespace GLeadGenerator.Model.Companies;

public class Company : Entity
{
    public string? Name { get; internal set; }
    public string? CatchPhrase { get; internal set; }
    public string? BusinessStrategy { get; internal set; }

    internal Company()
    {
    }
}

