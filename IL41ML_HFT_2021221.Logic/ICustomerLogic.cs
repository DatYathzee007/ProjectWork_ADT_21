using IL41ML_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic
{
    public interface ICustomerLogic
    {
        IList<Model> ListModels();

        IList<Service> ListServices();

        IList<Shop> ListShops();

        Model ListModelByID(int id);

        Service ListServiceByID(int id);

        Shop ListShopByID(int id);

        IList<Model> ListModelsByBrand(string brand);

        IList<Service> ListServiceByBrand(string brand);

        IList<Shop> ListShopsByBrand(string brand);

        IEnumerable<string> ListAllEntityByBrand(string brand);
        IList<string> ListAll();

        IList<string> ListModelsByPriceRange(int lowerBound, int upperBound);

        IEnumerable<string> ListShopsAndServicesBySpecificModel(int id);

        IEnumerable<string> ListShopsAndServiceINSpecificLocByBrand(string brand, string shoplocation);

        Task<IEnumerable<string>> ListShopsAndServiceINSpecificLocByBrandAsync(string brand, string shoplocation);

        // UPDATE CRUD
        void UpdateModel(Model model);
        void UpdateService(Service service);
        void UpdateService(Shop shop);
        // CREATE CRUD
        void CreateModel(Model model);
        void CreateService(Service service);
        void CreateService(Shop shop);
        // DELETE CRUD
        void DeleteModel(Model model);
        void DeleteService(Service service);
        void DeleteShop(Shop shop);
    }
}
