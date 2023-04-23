using System.Collections.Immutable;
using Oakular.Patterns.Repository.Models;

namespace Oakular.Patterns.Repository.Repositories;

public interface ICompositeRepository
{
    IImmutableList<Composite> Get(Listing listing);
}
