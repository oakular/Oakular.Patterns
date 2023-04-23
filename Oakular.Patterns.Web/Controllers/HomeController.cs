using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Oakular.Patterns.Repository.Models;
using Oakular.Patterns.Repository.Repositories;
using Oakular.Patterns.Web.Models;

namespace Oakular.Patterns.Web.Controllers;

public class HomeController : Controller
{
    private readonly IListingRepository listingRepository;
    private readonly ICompositeRepository compositeRepository;
    private readonly CompositeContentRepository compositeContentRepository;

    public HomeController(IListingRepository listingRepository,
                          ICompositeRepository compositeRepository,
                          CompositeContentRepository compositeContentRepository)
    {
        this.listingRepository = listingRepository;
        this.compositeRepository = compositeRepository;
        this.compositeContentRepository = compositeContentRepository;
    }

    public IActionResult Index()
    {
        var listings = listingRepository.Get().Select(_ => 
        {
            var composites = compositeRepository.Get(_).Select(_ => new CompositeViewModel(_.Name));
            return new ListingViewModel(_.Name, composites);
        });

        return View(listings);
    }

    public IActionResult Get(string listing, string composite)
    {
        var listingModel = new Listing(listing);
        var content = compositeContentRepository.Get(listingModel, composite);

        return new FileStreamResult(content.Stream, content.ContentType);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}