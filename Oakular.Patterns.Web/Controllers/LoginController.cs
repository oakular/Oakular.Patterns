using Microsoft.AspNetCore.Mvc;

namespace Oakular.Patterns.Web.Controllers;

public class LoginController : Controller
{
    [HttpPost]
    public IActionResult Index(string credential)
    {
        _ = credential ?? throw new ArgumentNullException(nameof(credential));

        return Ok();
    }
}