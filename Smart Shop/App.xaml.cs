using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smart_Shop.Interfaces;
using Smart_Shop.Services;
using Smart_Shop.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Smart_Shop;
public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<AppViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<AppViewModel>()
            });
            services.AddSingleton<IUtility, UtilityService>();

        }).Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _host.Start();
        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }
}