using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Smart_Shop.Interfaces;

namespace Smart_Shop.ViewModels
{
    public partial class AppViewModel : ObservableObject
    {
        private readonly IUtility _utilityService;
        [ObservableProperty]
        private string? _buildVer;

        public AppViewModel(IUtility utilityService)
        {
            _utilityService = utilityService;
            BuildVer = _utilityService.GenerateBuildVersion();
        }
    }
}
