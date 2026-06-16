using System.Net.Http.Headers;
using NinxERP.Domain.Entities;

namespace NinxERP.Infrastructure.Auth;

public class AuthHandler : DelegatingHandler
{
    private readonly UserSession _session;

    public AuthHandler(UserSession session)
    {
        _session = session;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(_session.Token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _session.Token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
