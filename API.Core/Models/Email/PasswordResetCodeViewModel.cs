namespace API.Core.Models.Email;

public class PasswordResetCodeViewModel : BaseEmailViewModel
{
    public string Name { get; set; }

    public string ResetLink { get; set; }
}
