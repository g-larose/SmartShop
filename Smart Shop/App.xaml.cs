﻿using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smart_Shop.Data;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Navigation;
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
            services.AddSingleton<AppDbContextFactory>();
            services.AddSingleton<AppViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<AppViewModel>()
            });
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IUtility, UtilityService>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<WeakReferenceMessenger>();
            

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