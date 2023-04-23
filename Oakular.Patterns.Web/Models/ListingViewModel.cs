using System.Collections.Immutable;

namespace Oakular.Patterns.Web.Models;

public readonly struct ListingViewModel
{
    public ListingViewModel(string name,
                            IEnumerable<CompositeViewModel> composites)
    {
        Name = name.Replace("-", " ");
        Composites = composites.ToImmutableArray();
    }

    public string Name { get; } = default!;

    public IImmutableList<CompositeViewModel> Composites { get; }
}
