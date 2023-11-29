using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Smart_Shop.Commands;
using Smart_Shop.Data;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using Smart_Shop.Services;

namespace Smart_Shop.ViewModels
{
    public partial class AppViewModel : ViewModelBase
    {
        private readonly IUtility _utilityService;
        private INavigator _navigator;
        private readonly IDbContextFactory<AppDbContext> _dbContext;

        public Stack<ViewModelBase> PreviousVMList = new();
        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;

        private ViewModelBase? _previousVM;
        public ViewModelBase? PreviousVM
        {
            get => _previousVM;
            set => OnPropertyChanged(ref _previousVM, value);
        }

        private bool _isBackEnabled;
        public bool IsBackEnabled
        {
            get => _isBackEnabled;
            set => OnPropertyChanged(ref _isBackEnabled, value);
        }


        private string? _buildVer;

        public string BuildVer
        {
            get => _buildVer!;
            set => OnPropertyChanged(ref _buildVer, value);
        }

        //Navigation Commands
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateSettingsCommand { get; }
        public ICommand NavigateCustomersCommand { get; }
        public ICommand NavigateAddCustomerCommand { get; }
        public ICommand NavigateNewInvoiceCommand { get; }
        public ICommand NavigateInvoicesCommand { get; }
        public ICommand BackCommand { get; }

        

        public AppViewModel(INavigator navigator, IDbContextFactory<AppDbContext> dbContext)
        {
            _dbContext = dbContext;
            _navigator = navigator;
            _utilityService = new UtilityService();
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
            BuildVer = _utilityService.GenerateBuildVersion();
            NavigateHomeCommand = new NavigateCommand<HomeViewViewModel>(_navigator, () => new HomeViewViewModel(_navigator, _dbContext));
            NavigateSettingsCommand = new NavigateCommand<SettingsViewModel>(_navigator, () => new SettingsViewModel());
            NavigateCustomersCommand = new NavigateCommand<CustomersViewModel>(_navigator, () => new CustomersViewModel(_navigator, _dbContext));
            NavigateNewInvoiceCommand = new NavigateCommand<NewInvoiceViewModel>(_navigator, () => new NewInvoiceViewModel(_navigator, _dbContext));
            NavigateAddCustomerCommand = new NavigateCommand<AddCustomerViewModel>(_navigator, () => new AddCustomerViewModel(_navigator, _dbContext));
            NavigateInvoicesCommand = new NavigateCommand<InvoicesListViewModel>(_navigator, () => new InvoicesListViewModel(_navigator, dbContext));
            BackCommand = new RelayCommand(NavigateBack);
        }

        private void NavigateBack()
        {
            if (PreviousVMList.Count() > 1)
            {
                var vm = PreviousVMList.ElementAt(1); 
                if (vm is HomeViewViewModel)
                {
                    NavigateHomeCommand.Execute(this);
                    PreviousVMList.Pop();
                    PreviousVMList.Pop();
                }
                else if (vm is CustomersViewModel)
                {
                    NavigateCustomersCommand.Execute(this);
                    PreviousVMList.Pop();
                    PreviousVMList.Pop();
                }
                else if (vm is SettingsViewModel)
                {
                    NavigateSettingsCommand.Execute(this);
                    PreviousVMList.Pop();
                    PreviousVMList.Pop();
                }
                else if (vm is AddCustomerViewModel)
                {
                    NavigateAddCustomerCommand.Execute(this);
                    PreviousVMList.Pop();
                    PreviousVMList.Pop();
                }
                else if (vm is EditCustomerViewModel)
                {
                    NavigateAddCustomerCommand.Execute(this);
                    PreviousVMList.Pop();
                    PreviousVMList.Pop();
                }
                else if (vm is InvoicesListViewModel)
                {
                    NavigateInvoicesCommand.Execute(this);
                    PreviousVMList.Pop();
                    PreviousVMList.Pop();
                }

            }
            IsBackEnabled = PreviousVMList.Count() > 1;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));

            if (!PreviousVMList.Contains(CurrentViewModel))
                PreviousVMList.Push(CurrentViewModel!);
            IsBackEnabled = PreviousVMList.Count() > 1;

        }
    }
}
