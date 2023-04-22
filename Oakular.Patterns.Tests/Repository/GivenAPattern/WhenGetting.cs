
using Oakular.Patterns.Repository;

namespace Repository.GivenAPattern;

public class WhenGetting
{
    [Fact]
    public void AllPatternsAreReturned()
    {
        var sut = new PatternRepository();

        sut.Get().Should().NotBeEmpty();
    }
}