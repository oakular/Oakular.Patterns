using System.Security.Claims;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Oakular.Patterns.Web.Options;

namespace Oakular.Patterns.Web.Controllers;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly IOptions<Authentication> authenticationOptions;

    public LoginController(IOptions<Authentication> authenticationOptions)
    {
        this.authenticationOptions = authenticationOptions;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(authenticationOptions);
    }

    [HttpPost]
    public async Task<IActionResult> Index(string credential)
    {
        _ = credential ?? throw new ArgumentNullException(nameof(credential));

        var payload = await GoogleJsonWebSignature.ValidateAsync(credential);

        if (payload.HostedDomain != authenticationOptions.Value.Domain)
        {
            throw new InvalidJwtException("Payload containd incorrect domain");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, payload.Name),
            new(ClaimTypes.Name, payload.Name),
            new(ClaimTypes.Email, payload.Email),
        };

        var principal = new ClaimsPrincipal();
        principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return RedirectToAction("Index", "Home");
    }
}