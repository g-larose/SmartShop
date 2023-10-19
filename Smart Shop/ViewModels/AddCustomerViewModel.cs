using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Smart_Shop.Commands;
using Smart_Shop.Data;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Smart_Shop.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly INavigator _navigator;
        private readonly AppDbContextFactory _dbContext;
        private readonly CustomerValidator _custValidator = new CustomerValidator();

        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }


        #region FIELDS

        private string _companyName;
        private string _contactName;
        private string _email;
        private string _phone;
        private string _address;

        #endregion

        #region PROPERTIES

        
        public string CompanyName
        {
            get => _companyName;
            set => OnPropertyChanged(ref _companyName, value);
        }

       
        public string ContactName
        {
            get => _contactName;
            set => OnPropertyChanged(ref _contactName, value);
        }

        
        public string Email
        {
            get => _email;
            set => OnPropertyChanged(ref _email, value);
        }

        
        public string Phone
        {
            get => _phone;
            set => OnPropertyChanged(ref _phone, value);
        }

        
        public string Address
        {
            get => _address;
            set => OnPropertyChanged(ref _address, value);
        }

        #endregion

        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = _custValidator.Validate(this).Errors.FirstOrDefault(p => p.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _custValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }

        public string Error
        {
            get
            {
                if (_custValidator != null)
                {
                    var results = _custValidator.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                        return errors;
                    }
                }
                return string.Empty;
            }
        }


        public AddCustomerViewModel(INavigator navigator, AppDbContextFactory dbContext)
        {
            _navigator = navigator;
            _dbContext = dbContext;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            CancelCommand = new Commands.RelayCommand(Cancel);
            SaveCommand = new Commands.RelayCommand(Save);
        }

        private void Save()
        {
            throw new NotImplementedException();
        }

        private void Cancel()
        {
            ICommand navigateHomeCommand = new NavigateCommand<HomeViewViewModel>(_navigator, () => new HomeViewViewModel(_navigator, _dbContext));
            navigateHomeCommand.Execute(this);
        }

        private void ValidatInputs()
        {
            
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
