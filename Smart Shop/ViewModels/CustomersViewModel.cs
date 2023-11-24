using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly AppDbContextFactory? _dbFactory;
        private readonly INavigator? _navigator;

        public ViewModelBase? CurrentViewModel => _navigator!.CurrentViewModel;

        private List<Customer> _customers;
        public List<Customer> Customers
        {
            get => _customers;
            set => OnPropertyChanged(ref _customers, value);
        }

        public CustomersViewModel( INavigator? navigator, AppDbContextFactory? dbFactory)
        {
            _dbFactory = dbFactory;
            _navigator = navigator;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            Customers = LoadCustomers();
        }

        private List<Customer> LoadCustomers()
        {
            using var db = _dbFactory.CreateDbContext();
            var customers = db.Customers.ToList();
            return customers;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
