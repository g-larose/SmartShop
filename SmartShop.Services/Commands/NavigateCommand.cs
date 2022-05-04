using SmartShop.Core.Commands;
using SmartShop.Services.Interfaces;
using SmartShop.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services.Commands
{
    internal class NavigateCommand<TViewModel> : CommandBase
      where TViewModel : ViewModelBase
    {
        private readonly INavigate _navigator;
        private readonly Func<TViewModel> _createViewModel;
        public NavigateCommand(INavigate navigator, Func<TViewModel> createViewModel)
        {
            _navigator = navigator;
            _createViewModel = createViewModel;
        }
        public override void Execute(object? parameter)
        {
            _navigator.CurrentViewModel = _createViewModel();
        }
    }
}
