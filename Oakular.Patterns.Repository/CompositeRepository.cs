using System.Collections.Immutable;
using Azure.Storage.Blobs;
using Oakular.Patterns.Repository.Models;

namespace Oakular.Patterns.Repository;

public sealed class CompositeRepository
{
    private readonly BlobServiceClient client;

    public CompositeRepository(BlobServiceClient client)
    {
        this.client = client;
    }

    public IImmutableList<Composite> Get(Listing listing)
    {
        var blobs = client.GetBlobContainerClient(listing.Name)
                          .GetBlobs()
                          .ToImmutableList();
                          
        return blobs.Select(_ => new Composite(_.Name)).ToImmutableArray();
    }
}
