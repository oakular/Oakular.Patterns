using Azure.Storage.Blobs;
using Oakular.Patterns.Repository.Repositories;

namespace Repository.GivenAComposite;

[Trait("Context", "When getting")]
public class WhenGetting
{
    private const string ConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1";
    private readonly string containerName = nameof(AllCompositesAreReturned).ToLower();

    [Fact(DisplayName = "All composites are returned.")]
    public void AllCompositesAreReturned()
    {
        // Arrange
        var client = new BlobServiceClient(ConnectionString);
        client.CreateBlobContainer(containerName);
        client.GetBlobContainerClient(containerName)
              .UploadBlob(Guid.NewGuid().ToString(), new MemoryStream());

        var sut = new CompositeRepository(client);

        sut.Get(new(containerName)).Should().NotBeEmpty();

        // Tear down
        client.DeleteBlobContainer(containerName);
    }
}