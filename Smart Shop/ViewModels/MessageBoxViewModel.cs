using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.ViewModels
{
    public class MessageBoxViewModel : ViewModelBase
    {
		private string? _statusCode;
		public string StatusCode
		{
			get => _statusCode!;
			set => OnPropertyChanged(ref _statusCode, value);
		}

		private string? _message;
		public string Message
		{
			get => _message!;
			set => OnPropertyChanged(ref _message, value);
		}

        public MessageBoxViewModel(string? statusCode, string? message)
        {
            _statusCode = statusCode;
            _message = message;
        }
    }
}
