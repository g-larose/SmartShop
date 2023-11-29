using Microsoft.EntityFrameworkCore;
using Smart_Shop.Commands;
using Smart_Shop.Data;
using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Smart_Shop.ViewModels
{
    public class EditCustomerViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IDbContextFactory<AppDbContext> _dbContext;

        private Customer _customer;
        public Customer Customer
        {
            get => _customer;
            set => OnPropertyChanged(ref _customer, value);
        }

        public ICommand UpdateCustomerCommand { get; }
        public ICommand CancelUpdateCommand { get; }
        public ICommand NavigateBackCommand { get; set; }

        public EditCustomerViewModel(INavigator navigator, IDbContextFactory<AppDbContext> dbContext, Customer customer)
        {
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Customer = customer;
            UpdateCustomerCommand = new RelayCommand(UpdateCustomer);
            CancelUpdateCommand = new RelayCommand(CancelUpdate);
        }

        private void CancelUpdate()
        {
            NavigateBackCommand = new NavigateCommand<CustomersViewModel>(_navigator, () => new CustomersViewModel(_navigator, _dbContext));
            NavigateBackCommand.Execute(this);
        }

        private void UpdateCustomer()
        {
            using var db = _dbContext!.CreateDbContext();
            db.Update<Customer>(Customer);
            db.SaveChanges();

            NavigateBackCommand = new NavigateCommand<CustomersViewModel>(_navigator, () => new CustomersViewModel(_navigator, _dbContext));
            NavigateBackCommand.Execute(this);
        }
    }
}
