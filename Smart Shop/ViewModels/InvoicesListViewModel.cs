using Microsoft.EntityFrameworkCore;
using Smart_Shop.Data;
using Smart_Shop.Factories;
using Smart_Shop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.ViewModels
{
    public class InvoicesListViewModel : ViewModelBase
    {
        private readonly INavigator? _navigator;
        private readonly IDbContextFactory<AppDbContext>? _dbFactory;

        public InvoicesListViewModel(INavigator? navigator, IDbContextFactory<AppDbContext>? dbFactory)
        {
            _navigator = navigator;
            _dbFactory = dbFactory;
        }
    }
}
