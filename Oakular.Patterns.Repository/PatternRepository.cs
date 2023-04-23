using System.Collections.Immutable;
using Azure.Storage.Blobs;
using Oakular.Patterns.Repository.Models;

namespace Oakular.Patterns.Repository;

public sealed class PatternRepository
{
    private readonly BlobContainerClient client;

    public PatternRepository(BlobContainerClient client)
    {
        this.client = client;
    }

    public IImmutableList<Pattern> Get()
    {
        var blobs = client.GetBlobs().ToImmutableList();
        return blobs.Select(_ => new Pattern()).ToImmutableList();
    }
}
