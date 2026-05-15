using Microsoft.UI.Xaml;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

// O .NET MAUI gera o ponto de entrada automaticamente para Windows.
// Este arquivo serve para customizações específicas da plataforma.

namespace NinxERP.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        // Customização da janela (Opcional)
        var window = Microsoft.Maui.Controls.Application.Current?.Windows.FirstOrDefault()?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
        if (window != null)
        {
            window.Title = "Ninx ERP";
        }
    }
}
