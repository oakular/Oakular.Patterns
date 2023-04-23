using Azure.Storage.Blobs;
using Oakular.Patterns.Repository.Repositories;

namespace Repository.GivenAListing;

[Trait("Context", "When getting")]
public class WhenGetting
{
    private const string ConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1";
    private readonly string containerName = nameof(AllListingsAreReturned).ToLower();

    [Fact(DisplayName = "All listings are returned.")]
    public void AllListingsAreReturned()
    {
        var client = new BlobServiceClient(ConnectionString);
        client.CreateBlobContainer(containerName);

        var sut = new ListingRepository(client);

        sut.Get().Should().NotBeEmpty();

        client.DeleteBlobContainer(containerName);
    }
}