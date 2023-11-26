using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Smart_Shop.Data;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using System.Collections.ObjectModel;

namespace Smart_Shop.ViewModels
{
    public class NewInvoiceViewModel : ViewModelBase
    {
        private IDbContextFactory<AppDbContext> _dbFactory;
        private INavigator _navigator;

        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;

        private string _invoiceNum;
        public string InvoiceNum
        {
            get => _invoiceNum;
            set => OnPropertyChanged(ref _invoiceNum, value);
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => OnPropertyChanged(ref _selectedCustomer, value);
        }

        private Invoice _invoice;
        public Invoice Invoice
        {
            get => _invoice;
            set => OnPropertyChanged(ref _invoice, value);
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => OnPropertyChanged(ref _customers, value);
        }

        public NewInvoiceViewModel(INavigator navigator, IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
            _navigator = navigator;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            InvoiceNum = "0000-0000-0001";
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            Customers = new();
            using var db = _dbFactory.CreateDbContext();
            var custs = db.Customers
                          .ToList();
            foreach (var c in custs)
            {
                Customers.Add(c);
            }
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
