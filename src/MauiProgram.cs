using Microsoft.Extensions.Logging;
using NinxERP.Application.UseCases;
using NinxERP.Domain.Entities;
using NinxERP.Domain.Interfaces;
using NinxERP.Infrastructure.Auth;
using NinxERP.Infrastructure.Config;
using NinxERP.Infrastructure.Services;


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
        
        // Sessão e Autenticação
        builder.Services.AddSingleton<UserSession>();
        builder.Services.AddTransient<AuthHandler>();

        // Configuração do HttpClient com AuthHandler automático
        builder.Services.AddHttpClient("NinxAPI", (sp, client) => {
            // Configurações base se necessário
        }).AddHttpMessageHandler<AuthHandler>();

        // Registra o HttpClient padrão para usar a factory configurada
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("NinxAPI"));
        
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IVendaService, VendaService>();
        builder.Services.AddScoped<IProdutoService, ProdutoService>();
        builder.Services.AddScoped<IEstoqueService, EstoqueService>();
        builder.Services.AddScoped<IClienteService, ClienteService>();
        builder.Services.AddSingleton<SessionService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
