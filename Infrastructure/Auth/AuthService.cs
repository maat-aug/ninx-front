using NinxERP.Domain.Entities;
using NinxERP.Domain.Interfaces;
using NinxERP.Domain.DTOs;
using NinxERP.Infrastructure.Config;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;

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

                if (loginResponse != null)
                {
                    if (!string.IsNullOrEmpty(loginResponse.Token))
                    {

                        var handler = new JwtSecurityTokenHandler();
                        var jwt = handler.ReadJwtToken(loginResponse.Token);

                        var usuarioId = int.Parse(jwt.Claims.First(x => x.Type == "usuarioId").Value);
                        var nome = jwt.Claims.First(x => x.Type == "nome").Value;
                        var emailClaim = jwt.Claims.First(x => x.Type == "email").Value;
                        var comercioId = int.Parse(jwt.Claims.First(x => x.Type == "comercioId").Value);
                        var permissao = jwt.Claims.First(x => x.Type == "permissao").Value;
                        _session.Token = loginResponse.Token;

                        var user = new User
                        {
                            Id = usuarioId,
                            Name = nome,
                            Email = emailClaim,
                            ComercioId = comercioId,
                            Role = permissao
                        };

                        var commerces = new List<Commerce>();
                        if (loginResponse.Comercios != null)
                        {
                            foreach(var c in loginResponse.Comercios)
                            {
                                commerces.Add(new Commerce { Id = c.ComercioID.ToString(), Name = c.Nome });
                            }
                        }

                        _session.Start(user, commerces);
                        return (true, "Autenticado com sucesso", loginResponse);
                    }

                    if (loginResponse.Comercios != null && loginResponse.Comercios.Count > 0)
                    {
                        return (false, "Selecione um comércio", loginResponse);
                    }
                }
            }

            return (false, "Falha na autenticação. Verifique suas credenciais.", null);
        }
        catch (Exception ex)
        {
            return (false, $"Erro ao conectar com a API: {ex.Message}", null);
        }
    }
}
