using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Smart_Shop.Commands;
using Smart_Shop.Data;
using Smart_Shop.Dialogs;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using Smart_Shop.Views;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Smart_Shop.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly IDbContextFactory<AppDbContext>? _dbFactory;
        private readonly INavigator? _navigator;

        public ViewModelBase? CurrentViewModel => _navigator!.CurrentViewModel;

        public ICommand SearchCommand { get; }
        public ICommand DeleteCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }
        public ICommand NavigateNewCustomerCommand { get; }
        public ICommand RefreshCustomersListCommand { get; }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => OnPropertyChanged(ref _customers, value);
        }

        private string _querytext;
        public string QueryText
        {
            get => _querytext;
            set => OnPropertyChanged(ref _querytext, value);
        }

        private Customer _customer;
        public Customer Customer
        {
            get => _customer;
            set => OnPropertyChanged(ref _customer, value);
        }

        public CustomersViewModel(INavigator? navigator, IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
            _navigator = navigator;
            _navigator!.CurrentViewModelChanged += OnCurrentViewModelChanged;
            SearchCommand = new RelayCommand(Search);
            RefreshCustomersListCommand = new RelayCommand(RefreshCustomersList);
            DeleteCustomerCommand = new RelayCommand<Customer>(DeleteCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(EditCustomer);
            NavigateNewCustomerCommand = new NavigateCommand<AddCustomerViewModel>(_navigator, () => new AddCustomerViewModel(_navigator, _dbFactory));
            Customers = new();
            LoadCustomers();
        }

        private void EditCustomer(Customer customer)
        {
            using var db = _dbFactory!.CreateDbContext();
            var c = db!.Customers.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
            //try to open a dialog and fill with customer data.
            Customer = c!;
            EditCustomerCommand = new NavigateCommand<EditCustomerViewModel>(_navigator!, () => new EditCustomerViewModel(_navigator!, _dbFactory, Customer!));
            EditCustomerCommand.Execute(this);
        }

        private void RefreshCustomersList()
        {
            Customers.Clear();
            LoadCustomers();
        }

        private void DeleteCustomer(Customer cust)
        {
            using var db = _dbFactory?.CreateDbContext();
            var c = db!.Customers.Where(x => x.CustomerId == cust.CustomerId).FirstOrDefault();
            db.Customers.Remove(c);
            db.SaveChanges();
            var custToRemove = Customers.Where(c => c == cust).FirstOrDefault();
            Customers.Remove(custToRemove!);

  
        }

        private void Search()
        {
            using var db = _dbFactory?.CreateDbContext();
            var filtered = db!.Customers.Where(x => x.CompanyName == QueryText).ToList();

            if (filtered.Count() > 0)
            {
                Customers.Clear();
                foreach (var c in filtered)
                {
                    Customers.Add(c);
                }
            }
           

        }

        private void LoadCustomers()
        {
            using var db = _dbFactory.CreateDbContext();
            var customers = db.Customers.ToList();

            foreach (var c in customers)
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
