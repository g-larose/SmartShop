using Smart_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.Interfaces
{
    public interface INavigator
    {
        public event Action CurrentViewModelChanged;
        public ViewModelBase? CurrentViewModel { get; set; }
    }
}
