using System.Collections.Immutable;
using Azure.Storage.Blobs;
using Oakular.Patterns.Repository.Models;

namespace Oakular.Patterns.Repository.Repositories;

public sealed class CompositeRepository : ICompositeRepository
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

public sealed class CompositeContentRepository
{
    private readonly BlobServiceClient client;

    public CompositeContentRepository(BlobServiceClient client) => this.client = client;

    public CompositeContent Get(Listing listing, string name)
    {
        var containerClient = client.GetBlobContainerClient(listing.Name);
        var b = containerClient.GetBlobClient(name);
        var response = b.DownloadContent();

        return new(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }
}
