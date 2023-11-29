using Microsoft.EntityFrameworkCore;
using Smart_Shop.Commands;
using Smart_Shop.Data;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using Smart_Shop.Navigation;
using Smart_Shop.Validation;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace Smart_Shop.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        //TODO: work on data validation
        private readonly INavigator _navigator;
        private readonly IDbContextFactory<AppDbContext>? _dbContext;
        private readonly ErrorsViewModel _errorsViewModel;

        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }

        public bool CanSave => _errorsViewModel.HasErrors;
        public bool HasErrors { get; }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        #region FIELDS

        private string? _companyName;
        private string? _contactName;
        private string? _email;
        private string? _phone;
        private string? _address;

        #endregion

        #region PROPERTIES
        
        public string? CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                _errorsViewModel.ClearErrors(nameof(CompanyName));
                if (string.IsNullOrEmpty(CompanyName))
                {
                    _errorsViewModel.AddError(nameof(CompanyName), "Company Name cannot be empty");
                }
                OnPropertyChanged(ref _companyName!, value);
            }
        }

       
        public string? ContactName
        {
            get => _contactName;
            set
            {
                _contactName = value;
                _errorsViewModel.ClearErrors(nameof(ContactName));
                if (string.IsNullOrEmpty(ContactName))
                {
                    _errorsViewModel.AddError(nameof(ContactName), "Company Name cannot be empty");
                }
                OnPropertyChanged(ref _contactName!, value);
            }
        }

        
        public string? Email
        {
            get => _email;
            set => OnPropertyChanged(ref _email!, value);
        }

        
        public string? Phone
        {
            get => _phone;
            set => OnPropertyChanged(ref _phone!, value);
        }

        
        public string? Address
        {
            get => _address;
            set => OnPropertyChanged(ref _address!, value);
        }
       
        #endregion

        public AddCustomerViewModel(INavigator navigator, IDbContextFactory<AppDbContext> dbFactory )
        {
            _navigator = navigator;
            _dbContext = dbFactory;
            _errorsViewModel = new ErrorsViewModel();
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _errorsViewModel.ErrorsChanged += OnErrorsChanged;
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }

        private void OnErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }
        private void Save()
        {
            if (CompanyName is not null && ContactName is not null &&
                Phone is not null && Email is not null && Address is not null)
                {
                    // check to see if the customer already exists in the DB/
                    using var db = _dbContext!.CreateDbContext();
                    var c = db.Customers.Select(x => x.CompanyName == CompanyName).FirstOrDefault();

                    if (c is { })
                    {
                        //we save the customer to the db
                        var cust = new Customer()
                        {
                            Identifier = Guid.NewGuid(),
                            CompanyName = CompanyName!,
                            ContactName = ContactName!,
                            Email = Email!,
                            Phone = Phone!,
                            Address = Address!

                        };

                        db.Customers.Add(cust);
                        db.SaveChanges();
                        ClearInputs();
                    } 
                }
        }

        private void ClearInputs()
        {
            CompanyName = string.Empty;
            ContactName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
        }

        private void Cancel()
        {
            ICommand navigateCustomersCommand = new NavigateCommand<CustomersViewModel>(_navigator, () => new CustomersViewModel(_navigator, _dbContext));
            navigateCustomersCommand.Execute(this);
        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }
    }
}
