using System.Collections.Immutable;
using Azure.Storage.Blobs;
using Oakular.Patterns.Repository.Models;

namespace Oakular.Patterns.Repository;

public sealed class PatternRepository
{
    public IImmutableList<Pattern> Get()
    {
        var client = new BlobContainerClient();

        var blobs = client.GetBlobs().ToImmutableList();
        return blobs.Select(_ => new Pattern()).ToImmutableList();
    }
}
