using BytePress.Shared.Data.Domain.Identity;

namespace BytePress.Shared.Data.Domain;

public class Task : BaseEntity
{
    public string UserId { get; set; }

    public string Name { get; set; }

    public bool IsCompleted { get; set; }

    public ApplicationUser User { get; set; }
}
