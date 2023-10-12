using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Smart_Shop.Commands;
using Smart_Shop.Interfaces;

namespace Smart_Shop.ViewModels
{
    public partial class AppViewModel : ViewModelBase
    {
        private readonly IUtility _utilityService;
        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;
      
        private string? _buildVer;

        private INavigator _navigator;

        public string BuildVer
        {
            get => _buildVer!;
            set => OnPropertyChanged(ref _buildVer, value);
        }

        //Navigation Commands
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateSettingsCommand { get; }

        public AppViewModel(IUtility utilityService, INavigator navigator)
        {
            _utilityService = utilityService;
            _navigator = navigator;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            BuildVer = _utilityService.GenerateBuildVersion();
            NavigateHomeCommand = new NavigateCommand<HomeViewViewModel>(_navigator, () => new HomeViewViewModel());
            NavigateSettingsCommand = new NavigateCommand<SettingsViewModel>(_navigator, () => new SettingsViewModel());
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
