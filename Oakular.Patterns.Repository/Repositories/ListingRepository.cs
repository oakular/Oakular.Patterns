using System.Collections.Immutable;
using Azure.Storage.Blobs;
using Oakular.Patterns.Repository.Models;

namespace Oakular.Patterns.Repository.Repositories;

public sealed class ListingRepository : IListingRepository
{
    private readonly BlobServiceClient client;

    public ListingRepository(BlobServiceClient client)
    {
        this.client = client;
    }

    public IImmutableList<Listing> Get()
    {
        var containers = client.GetBlobContainers().ToImmutableList();
        return containers.Select(_ => new Listing(_.Name)).ToImmutableList();
    }
}
