using IL41ML_HFT_2021221.Logic.DataType;
using IL41ML_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic
{
    public interface IStockLogic
    {
        IList<Brand> ListBrands();
        IList<Model> ListModels();
        Brand ListBrandByID(int id);
        Model ListModelByID(int id);
        void InsertModel(Model input);
        void InsertModel(int brandid, string name, string modelName, int size, string color, int price);
        IList<Model> ListModelsByBrand(string brand);
        IEnumerable<NameAndDouble> ListBrandAverages();
        Task<IEnumerable<NameAndDouble>> ListBrandAveragesAsync();
        NameAndDouble ListMostExpensiveBrand();
        Task<NameAndDouble> ListMostExpensiveBrandAsync();
        NameAndDouble ListLeastExpensiveBrand();
        Task<NameAndDouble> ListLeastExpensiveBrandAsync();
        Model MostExpensiveModel();
        double AveragePriceOfModels();
        Model ListCheapestModel();
        IList<NameAndDouble> GetModelAverage();
        Task<IList<NameAndDouble>> GetModelAverageAsync();
    }
}
