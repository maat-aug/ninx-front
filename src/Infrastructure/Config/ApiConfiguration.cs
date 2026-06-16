namespace NinxERP.Infrastructure.Config;

public class ApiConfiguration
{
    public string BaseUrl { get; set; } = "https://localhost:7093";
    public string LoginEndpoint { get; set; } = "/api/Login";

    public string GetLoginUrl() => $"{BaseUrl}{LoginEndpoint}";

    public string GetEndpointUrl(string endpoint) => $"{BaseUrl}{endpoint}";
}
