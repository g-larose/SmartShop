using SmartShop.Core.Commands;
using SmartShop.Services.Commands;
using SmartShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private readonly INavigate? _navigator;
        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;
        public CommandBase? NavigateProjectCommand { get; }

        private string _currentDate;
        public string CurrentDate
        {
            get => _currentDate;
            set => OnPropertyChanged(ref _currentDate, value);
        }

        public AppViewModel(INavigate navigator)
        {
            _navigator = navigator;
            CurrentDate = DateTime.Now.ToShortDateString();
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            NavigateProjectCommand = new NavigateCommand<ProjectViewViewModel>(_navigator, () => new ProjectViewViewModel());
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
