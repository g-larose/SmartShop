using Smart_Shop.Commands;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Validation;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace Smart_Shop.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase, IDataErrorInfo, INotifyDataErrorInfo
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
        private bool _hasErrors;
        private ObservableCollection<string> _errors;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;


        #endregion

        #region PROPERTIES

        public ObservableCollection<string> Errors
        {
            get => _errors;
            set => OnPropertyChanged(ref _errors, value);
        }
        
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

        public bool HasErrors
        {
            get => _hasErrors;
            set => OnPropertyChanged(ref _hasErrors, value);
        }

        #endregion

        #region VALIDATION

        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = _custValidator.Validate(this).Errors.FirstOrDefault(p => p.PropertyName == columnName);
                if (firstOrDefault != null)
                {
                    return _custValidator != null ? firstOrDefault.ErrorMessage : "";
                }
               
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

       

        #endregion


        public AddCustomerViewModel(INavigator navigator, AppDbContextFactory dbContext)
        {
            _navigator = navigator;
            _dbContext = dbContext;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;  
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            if (CompanyName is not null || ContactName is not null ||
                Phone is not null || Email is not null || Address is not null)
            {
                //we save the customer to the db
            }
        }

        private void Cancel()
        {
            ICommand navigateHomeCommand = new NavigateCommand<HomeViewViewModel>(_navigator, () => new HomeViewViewModel(_navigator, _dbContext));
            navigateHomeCommand.Execute(this);
        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private bool CanExecute(object args)
        {
            var result = _custValidator.Validate(this);
            return result.IsValid;
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
