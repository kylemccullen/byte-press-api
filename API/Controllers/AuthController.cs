using API.Core.Classes;
using API.Core.Models;
using BytePress.Shared.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

[Route("api/[controller]")]
public class AuthController : Controller
{
    public readonly AppSettings _appSettings;
    public readonly RazorRenderer _razorRenderer;

    public AuthController(IOptions<AppSettings> appSettings, RazorRenderer razorRenderer)
    {
        _appSettings = appSettings.Value;
        _razorRenderer = razorRenderer;
    }


    [HttpGet("/reset-password")]
    public async Task<ActionResult> ResetPasswordAsync([FromQuery] string email, [FromQuery] string code)
    {
        var model = new ResetPasswordViewModel()
        {
            Email = email,
            ResetCode = code,
            CompanyName = _appSettings.CompanyName
        };

        var view = await _razorRenderer.RenderRazorViewToStringAsync("API.Core.Views.ResetPassword", model);

        return Content(view, "text/html");
    }
}
