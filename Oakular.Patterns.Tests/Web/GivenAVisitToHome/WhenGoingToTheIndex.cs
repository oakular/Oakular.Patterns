using System.Collections.Immutable;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Oakular.Patterns.Repository.Models;
using Oakular.Patterns.Repository.Repositories;
using Oakular.Patterns.Web.Controllers;
using Oakular.Patterns.Web.Models;

namespace Web.GivenAVisitToHome;

[Trait("Context", "When going to the index")]
public class WhenGoingToTheIndex
{
    private readonly HomeController sut;

    public WhenGoingToTheIndex()
    {
        var fakeListingRepo = A.Fake<IListingRepository>();
        A.CallTo(() => fakeListingRepo.Get()).Returns(Enumerable.Range(0, 10).Select(_ => new Listing(_.ToString())).ToImmutableList());

        var fakeCompositeRepo = A.Fake<ICompositeRepository>();
        A.CallTo(() => fakeCompositeRepo.Get(A<Listing>.Ignored)).Returns(Enumerable.Range(0, 3).Select(_ => new Composite(_.ToString())).ToImmutableArray());
        var fakeContentRepo = A.Fake<CompositeContentRepository>();

        sut = new HomeController(fakeListingRepo, fakeCompositeRepo, fakeContentRepo);
    }

    [Fact(DisplayName = "The view has the correct model type.")]
    public void HasListingViewModel()
    {
        var viewResult = sut.Index().As<ViewResult>();

        viewResult.Model.Should().BeAssignableTo<IEnumerable<ListingViewModel>>();
    }

    [Fact]
    public void ViewContainsListings()
    {
        var viewResult = sut.Index().As<ViewResult>();

        viewResult.Model.As<IEnumerable<ListingViewModel>>().Should().NotBeEmpty();
    }
}