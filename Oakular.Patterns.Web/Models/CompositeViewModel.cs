using Oakular.Patterns.Repository.Models;

namespace Oakular.Patterns.Web.Models;

public readonly struct CompositeViewModel : INameable
{
    public CompositeViewModel(string name)
    {
        Name = name;
    }

    public string Name { get; } = default!;
}
