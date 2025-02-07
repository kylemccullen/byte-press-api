using BytePress.Shared.Classes;

namespace API.Core.DTO.User;

public class BaseUserDto
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Role { get; set; }
}
