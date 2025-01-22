using System.ComponentModel.DataAnnotations;

namespace BytePress.Shared.Data.Domain;

public class Task
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsCompleted { get; set; }
}
