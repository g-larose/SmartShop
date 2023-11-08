using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Smart_Shop.Data;
using Smart_Shop.Extensions;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.ViewModels
{
    public class HomeViewViewModel : ViewModelBase
    {
        private readonly INavigator? _navigator;
        private readonly AppDbContextFactory? _dbContext;

        public HomeViewViewModel(INavigator? navigator, AppDbContextFactory? dbContext)
        {
            _navigator = navigator;
            _dbContext = dbContext;
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            Customers = new();
            using var db = _dbContext!.CreateDbContext();
            Customers = db!.Customers.Select(x => new Customer()
            {
                Identifier = x.Identifier,
                CompanyName = x.CompanyName,
                ContactName = x.ContactName,
                Email = x.Email,
                Phone = x.Phone
            }).ToList().ToObservableCollection();


        }

        private ObservableCollection<Customer>? _customers;
        public ObservableCollection<Customer>? Customers
        {
            get => _customers;
            set => OnPropertyChanged(ref _customers, value);
        }
    }
}
