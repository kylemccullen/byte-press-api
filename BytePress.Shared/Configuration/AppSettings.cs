namespace BytePress.Shared.Configuration;

public class AppSettings
{
    public string BaseUrl { get; set; }
    public string CompanyName { get; set; }
    public ConnectionStringSettings ConnectionStrings { get; set; }
    public EmailSettings EmailSettings { get; set; }
}

public class ConnectionStringSettings
{
    public string BytePress { get; set; }
}

public class EmailSettings
{
    public bool IsEnabled { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string Sender { get; set; }
    public string FromEmail { get; set; }
    public string SenderPassword { get; set; }
}
