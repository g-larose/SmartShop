using SmartShop.Services.Interfaces;
using SmartShop.Services.ViewModels;
using System;

namespace SmartShop.Services.Navigation
{
    public class Navigator : INavigate
    {
        public event Action? CurrentViewModelChanged;

        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChaged();
            }
        }

        private void OnCurrentViewModelChaged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
