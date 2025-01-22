namespace BytePress.Shared.Configuration;

public class AppSettings
{
    public ConnectionStringSettings ConnectionStrings { get; set; }
}

public class ConnectionStringSettings
{
    public string BytePress { get; set; }
}

