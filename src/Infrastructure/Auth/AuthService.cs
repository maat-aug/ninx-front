using NinxERP.Domain.Entities;
using NinxERP.Domain.Interfaces;
using NinxERP.Domain.DTOs;
using NinxERP.Infrastructure.Config;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NinxERP.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly UserSession _session;
    private readonly HttpClient _httpClient;
    private readonly ApiConfiguration _apiConfig;

    public AuthService(UserSession session, HttpClient httpClient, ApiConfiguration apiConfig)
    {
        _session = session;
        _httpClient = httpClient;
        _apiConfig = apiConfig;
    }

    public async Task<(bool Success, string Message, LoginResponse? Response)> AuthenticateAsync(string email, string password, int? comercioIdLogin = null)
    {
        try
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Senha = password,
                ComercioId = comercioIdLogin
            };

            var response = await _httpClient.PostAsJsonAsync(_apiConfig.GetLoginUrl(), loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    UpdateSessionFromToken(loginResponse.Token, loginResponse.Comercios);
                    return (true, "Autenticado com sucesso", loginResponse);
                }
                
                if (loginResponse?.Comercios != null && loginResponse.Comercios.Count > 0)
                {
                    return (false, "Selecione um comércio", loginResponse);
                }
            }

            return (false, "Falha na autenticação. Verifique suas credenciais.", null);
        }
        catch (Exception ex)
        {
            return (false, $"Erro ao conectar com a API: {ex.Message}", null);
        }
    }

    public async Task<bool> TrocarComercioAsync(int comercioId)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_apiConfig.GetEndpointUrl($"/api/TrocarComercio/{comercioId}"), "");
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result != null && !string.IsNullOrEmpty(result.Token))
                {
                    UpdateSessionFromToken(result.Token, result.Comercios);
                    return true;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Commerce>> GetComerciosUsuarioAsync(int usuarioId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<ComercioUsuarioDTO>>(_apiConfig.GetEndpointUrl($"/api/Comercio/usuario/{usuarioId}"));
            if (response != null)
            {
                return response.Select(c => new Commerce { Id = c.ComercioID.ToString(), Name = c.NomeComercio }).ToList();
            }
            return new List<Commerce>();
        }
        catch
        {
            return new List<Commerce>();
        }
    }

    private void UpdateSessionFromToken(string token, List<ComercioOption>? comerciosOptions)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        // Busca claims de forma mais robusta (case-insensitive e suportando diferentes mapeamentos)
        var usuarioIdStr = jwt.Claims.FirstOrDefault(c => c.Type.Equals("usuarioId", StringComparison.OrdinalIgnoreCase))?.Value;
        var nome = jwt.Claims.FirstOrDefault(c => c.Type.Equals("nome", StringComparison.OrdinalIgnoreCase) || c.Type == ClaimTypes.Name)?.Value;
        var email = jwt.Claims.FirstOrDefault(c => c.Type.Equals("email", StringComparison.OrdinalIgnoreCase) || c.Type == ClaimTypes.Email)?.Value;
        var comercioIdStr = jwt.Claims.FirstOrDefault(c => c.Type.Equals("comercioId", StringComparison.OrdinalIgnoreCase))?.Value;
        var permissao = jwt.Claims.FirstOrDefault(c => c.Type.Equals("permissao", StringComparison.OrdinalIgnoreCase) || c.Type == ClaimTypes.Role)?.Value;
        var nomeComercio = jwt.Claims.FirstOrDefault(c => c.Type.Equals("nomeComercio", StringComparison.OrdinalIgnoreCase))?.Value;
        _session.Token = token;

        var user = new User
        {
            Id = int.TryParse(usuarioIdStr, out var id) ? id : 0,
            Name = nome ?? "Usuário",
            Email = email ?? "",
            ComercioId = int.TryParse(comercioIdStr, out var cid) ? cid : 0,
            Role = permissao ?? "Funcionario",
            nomeComercio = nomeComercio.ToString()
        };

        var commerces = new List<Commerce>();
        if (comerciosOptions != null)
        {
            commerces.AddRange(comerciosOptions.Select(c => new Commerce { Id = c.ComercioID.ToString(), Name = c.Nome }));
        }

        _session.Start(user, commerces, comercioIdStr, nomeComercio);
        
        // Configura o token no HttpClient para chamadas futuras
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
}

public class ComercioUsuarioDTO
{
    public int ComercioID { get; set; }
    public string NomeComercio { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
}
