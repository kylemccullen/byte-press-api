using Ardalis.SmartEnum;

namespace BytePress.Shared.Classes;

public sealed class Roles : SmartEnum<Roles, string>
{
    public static readonly Roles Admin = new("Admin");
    public static readonly Roles Client = new("Client");

    public Roles(string value) : base(value, value)
    {
    }
}
