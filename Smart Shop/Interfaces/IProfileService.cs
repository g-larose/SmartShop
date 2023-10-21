using Smart_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Smart_Shop.Interfaces
{
    public interface IProfileService
    {
        void WriteProfileImageToDb(Customer cust);
        byte ReadProfileImageFromDb(Customer cust);
    }
}
