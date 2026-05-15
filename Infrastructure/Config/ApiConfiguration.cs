namespace NinxERP.Infrastructure.Config
{
    /// <summary>
    /// Configuração centralizada para URLs da API
    /// Suporta múltiplos ambientes (localhost, staging, production)
    /// </summary>
    public class ApiConfiguration
    {
        public string BaseUrl { get; set; } = "https://localhost:7093";
        public string LoginEndpoint { get; set; } = "/api/Login";

        /// <summary>
        /// Retorna a URL completa do endpoint de login
        /// </summary>
        public string GetLoginUrl() => $"{BaseUrl}{LoginEndpoint}";

        /// <summary>
        /// Retorna a URL completa para qualquer endpoint
        /// </summary>
        public string GetEndpointUrl(string endpoint) => $"{BaseUrl}{endpoint}";
    }
}
