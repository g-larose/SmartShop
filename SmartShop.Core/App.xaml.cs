using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartShop.Services.Interfaces;
using SmartShop.Services.Navigation;
using System.Windows;

namespace SmartShop.Core
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<INavigate, Navigator>();

            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            base.OnStartup(e);
        }
    }
}
