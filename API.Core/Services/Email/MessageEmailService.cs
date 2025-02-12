using API.Core.Classes;
using API.Core.Models.Email;
using BytePress.Shared.Configuration;
using BytePress.Shared.Data.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace API.Core.Services.Email;

public class MessageEmailService(RazorRenderer razorRenderer, IEmailSender emailSender, IOptions<AppSettings> appSettings) : IEmailSender<ApplicationUser>
{
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        var model = new ConfirmationLinkViewModel
        {
            ConfirmationLink = confirmationLink,
            CompanyName = _appSettings.CompanyName
        };

        var view = await razorRenderer.RenderRazorViewToStringAsync("API.Core.Views.Email.ConfirmationLink", model);

        await emailSender.SendEmailAsync(email, "Confirm your email", view);
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        var model = new PasswordResetCodeViewModel
        {
            Name = user.Name ?? email,
            ResetLink = $"{_appSettings.BaseUrl.Trim('/')}/reset-password?email={email}&code={resetCode}",
            CompanyName = _appSettings.CompanyName
        };

        var view = await razorRenderer.RenderRazorViewToStringAsync("API.Core.Views.Email.PasswordResetCode", model);

        await emailSender.SendEmailAsync(email, "Reset your password", view);
    }
}
