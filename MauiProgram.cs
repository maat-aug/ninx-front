using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using NinxERP.Domain.Entities;
using NinxERP.Domain.Interfaces;
using NinxERP.Infrastructure.Auth;
using NinxERP.Infrastructure.Config;
using NinxERP.Services;

namespace NinxERP;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddMauiBlazorWebView();
        
        // Configurações da API
        builder.Services.AddSingleton<ApiConfiguration>();
        
        // Cliente HTTP
        builder.Services.AddScoped(sp => new HttpClient());
        
        builder.Services.AddScoped<UserSession>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<SessionService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
