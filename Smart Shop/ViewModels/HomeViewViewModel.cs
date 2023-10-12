using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Smart_Shop.Data;
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
        private readonly IDbContextFactory<AppDbContext>? _dbFactory;

        public HomeViewViewModel(INavigator? navigator, IDbContextFactory<AppDbContext>? dbFactory)
        {
            _navigator = navigator;
            _dbFactory = dbFactory;
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => OnPropertyChanged(ref _customers, value);
        }
    }
}
