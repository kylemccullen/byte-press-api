namespace API.Core.DTO.User;

public class RegisterDto
{
    public required string Email { get; init; }

    public required string Password { get; init; }

    public string Name { get; set; }
}
