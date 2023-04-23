using Azure.Storage.Blobs;
using Oakular.Patterns.Repository;

namespace Repository.GivenAPattern;

[Trait("Context", "When getting")]
public class WhenGetting
{
    private const string ConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1";
    private readonly string containerName = nameof(AllPatternsAreReturned).ToLower();

    [Fact(DisplayName = "All patterns are returned.")]
    public void AllPatternsAreReturned()
    {
        var svc = new BlobServiceClient(ConnectionString);
        svc.CreateBlobContainer(containerName);

        var client = new BlobContainerClient(ConnectionString, containerName);
        client.UploadBlob(Guid.NewGuid().ToString(), new MemoryStream());

        var sut = new PatternRepository(client);

        sut.Get().Should().NotBeEmpty();

        svc.DeleteBlobContainer(containerName);
    }
}