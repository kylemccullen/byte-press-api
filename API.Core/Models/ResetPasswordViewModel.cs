using API.Core.Models.Email;

namespace API.Core.Models;

public class ResetPasswordViewModel : BaseEmailViewModel
{
    public string Email { get; set; }

    public string ResetCode { get; set; }
}
