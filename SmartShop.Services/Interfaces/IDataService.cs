using SmartShop.Data.Configuration;

namespace SmartShop.Services.Interfaces
{
    internal interface IDataService
    {
        public ConfigJson GetConnectionString();
    }
}
