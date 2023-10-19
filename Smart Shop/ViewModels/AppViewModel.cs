using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Smart_Shop.Commands;
using Smart_Shop.Data;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;

namespace Smart_Shop.ViewModels
{
    public partial class AppViewModel : ViewModelBase
    {
        private readonly IUtility _utilityService;
        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;
      
        private string? _buildVer;

        private INavigator _navigator;
        private readonly AppDbContextFactory _dbContext;

        public string BuildVer
        {
            get => _buildVer!;
            set => OnPropertyChanged(ref _buildVer, value);
        }

        //Navigation Commands
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateSettingsCommand { get; }
        public ICommand NavigateNewCustomerCommand { get; }

        public AppViewModel(IUtility utilityService, INavigator navigator, AppDbContextFactory dbContext)
        {
            _dbContext = dbContext;
            _navigator = navigator;
            _utilityService = utilityService;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            BuildVer = _utilityService.GenerateBuildVersion();
            NavigateHomeCommand = new NavigateCommand<HomeViewViewModel>(_navigator, () => new HomeViewViewModel(_navigator, _dbContext));
            NavigateSettingsCommand = new NavigateCommand<SettingsViewModel>(_navigator, () => new SettingsViewModel());
            NavigateNewCustomerCommand = new NavigateCommand<AddCustomerViewModel>(_navigator, () => new AddCustomerViewModel(_navigator, _dbContext));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
