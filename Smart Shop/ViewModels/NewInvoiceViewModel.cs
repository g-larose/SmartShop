using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.ViewModels
{
    public class NewInvoiceViewModel : ViewModelBase
    {
        private AppDbContextFactory _dbFactory;
        private INavigator _navigator;

        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;

        public NewInvoiceViewModel(INavigator navigator, AppDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
            _navigator = navigator;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
