using Smart_Shop.Commands;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Smart_Shop.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly AppDbContextFactory? _dbFactory;
        private readonly INavigator? _navigator;

        public ViewModelBase? CurrentViewModel => _navigator!.CurrentViewModel;

        public ICommand SearchCommand { get; }

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

        public CustomersViewModel( INavigator? navigator, AppDbContextFactory? dbFactory)
        {
            _dbFactory = dbFactory;
            _navigator = navigator;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            SearchCommand = new RelayCommand(Search);
            Customers = new();
            LoadCustomers();
        }

        private void Search()
        {
            using var db = _dbFactory.CreateDbContext();
            var filtered = db.Customers.Where(x => x.CompanyName == QueryText).ToList();

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
