using Smart_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.Messaging
{
    public class WeakMesaageReference<TViewModel> where TViewModel : ViewModelBase
    {
        public string? Message { get; set; }
        public List<string>? MessageQue { get; set; }
    }
}
