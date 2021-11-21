using IL41ML_HFT_2021221.Logic.DataType;
using IL41ML_HFT_2021221.Models;
using IL41ML_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic
{
    public class StockLogic : IStockLogic
    {
        private IBrandRepository brandRepo;
        private IModelRepository modelRepo;
        public StockLogic(IBrandRepository brandRepo, IModelRepository modelRepo)
        {
            this.brandRepo = brandRepo;
            this.modelRepo = modelRepo;
        }
        public void InsertModel(int brandid, string name, string modelName, int size, string color, int price)
        {
            Model newModel = new Model()
            {
                BrandId = this.brandRepo.GetAll().Single(brands => brands.Id == brandid).Id,
                Name = name,
                ModelName = modelName,
                Size = size,
                Color = color,
                Price = price,
            };
            this.modelRepo.Insert(newModel);
        }
        public Brand ListBrandByID(int id)
        {
            return this.brandRepo.GetOne(id);
        }
        public IList<Brand> ListBrands()
        {
            return this.brandRepo.GetAll().ToList();
        }
        public Model ListModelByID(int id)
        {
            return this.modelRepo.GetOne(id);
        }
        public IList<Model> ListModels()
        {
            return this.modelRepo.GetAll().ToList();
        }
        public IList<Model> ListModelsByBrand(string brand)
        {
            return this.modelRepo.GetAll()
                .Where(x => x.BrandId
                .Equals(this.brandRepo.GetAll().Single(x => x.Name == brand).Id))
                .ToList();
        }
        public Model MostExpensiveModel()
        {
            return this.modelRepo.GetAll().OrderByDescending(x => x.Price).First();
        }
        public double AveragePriceOfModels()
        {
            double avg = this.modelRepo.GetAll().Average(x => x.Price);
            return Math.Round(avg);
        }
        public Model ListCheapestModel()
        {
            return this.modelRepo.GetAll().OrderByDescending(x => x.Price).Last();
        }

        /*
        public IEnumerable<NameAndDouble> OLDListBrandAverages()
        {
            return this.modelRepo.GetAll()
                .GroupBy(x => x.BrandId)
                .Select(g => new { BrandId = g.Key, Average = g.Average(s => s.Price) })
                .Join(this.brandRepo.GetAll(), g => g.BrandId, brand => brand.Id, (group, brand) => new NameAndDouble { Name = brand.Name, Average = group.Average })
                .ToList();
        }
        */

        public IEnumerable<NameAndDouble> ListBrandAverages()
        {
            var q = from model in this.modelRepo.GetAll()
                    group model by model.BrandId into g
                    select new { BrandId = g.Key, Average = g.Average(s => s.Price) }
                    into brandavg
                    join brand in this.brandRepo.GetAll() on brandavg.BrandId equals brand.Id
                    select new NameAndDouble { Name = brand.Name, Average = brandavg.Average }
                    into result
                    orderby result.Average
                    select result;
            return q.ToList();
        }

        public Task<IEnumerable<NameAndDouble>> ListBrandAveragesAsync()
        {
            return Task.Run(this.ListBrandAverages);
        }

        /*
        public NameAndDouble OLDListMostExpensiveBrand()
        {
            return this.modelRepo.GetAll()
                .GroupBy(x => x.BrandId)
                .Select(g => new { BrandId = g.Key, Average = g.Average(s => s.Price) })
                .Join(this.brandRepo.GetAll(), g => g.BrandId, brand => brand.Id, (group, brand) => new NameAndDouble { Name = brand.Name, Average = group.Average })
                .OrderByDescending(g => g.Average)
                .First();
        }
        */
        public NameAndDouble ListMostExpensiveBrand()
        {
            /*
            var q = from model in this.modelRepo.GetAll()
                    group model by model.BrandId into g
                    select new { BrandId = g.Key, Average = g.Average(s => s.Price) }
                    into brandavg
                    join brand in this.brandRepo.GetAll() on brandavg.BrandId equals brand.Id
                    select new NameAndDouble { Name = brand.Name, Average = brandavg.Average }
                    into result
                    orderby result.Average
                    select result;
            return q.Last();
            */
            return this.ListBrandAverages().Last();
        }
        public Task<NameAndDouble> ListMostExpensiveBrandAsync()
        {
            return Task.Run(this.ListMostExpensiveBrand);
        }

        /*
        public NameAndDouble OLDListLeastExpensiveBrand()
        {
            return this.modelRepo.GetAll()
                .GroupBy(x => x.BrandId)
                .Select(g => new { BrandId = g.Key, Average = g.Average(s => s.Price) })
                .Join(this.brandRepo.GetAll(), g => g.BrandId, brand => brand.Id, (group, brand) => new NameAndDouble { Name = brand.Name, Average = group.Average })
                .OrderByDescending(g => g.Average)
                .Last();
        }
        */

        public NameAndDouble ListLeastExpensiveBrand()
        {
            /*
            var q = from model in this.modelRepo.GetAll()
                    group model by model.BrandId into g
                    select new { BrandId = g.Key, Average = g.Average(s => s.Price) }
                    into brandavg
                    join brand in this.brandRepo.GetAll() on brandavg.BrandId equals brand.Id
                    select new NameAndDouble { Name = brand.Name, Average = brandavg.Average }
                    into result
                    orderby result.Average
                    select result;
            return q.First();
            */
            return this.ListBrandAverages().First();
        }

        public Task<NameAndDouble> ListLeastExpensiveBrandAsync()
        {
            return Task.Run(this.ListLeastExpensiveBrand);
        }

        /*
        public IList<NameAndDouble> OLDGetModelAverage()
        {
            return this.modelRepo.GetAll()
                .GroupBy(x => x.Name)
                .Select(g => new NameAndDouble { Name = g.Key, Average = g.Average(s => s.Price) })
                .OrderByDescending(g => g.Average)
                .ToList();
        }
        */
        public IList<NameAndDouble> GetModelAverage()
        {
            var q = from model in this.modelRepo.GetAll()
                    group model by model.Name into g
                    select new { Name = g.Key, Average = g.Average(s => s.Price) }
                    into modelavg
                    orderby modelavg.Average descending
                    select new NameAndDouble { Name = modelavg.Name, Average = modelavg.Average };
            return q.ToList();
        }

        public Task<IList<NameAndDouble>> GetModelAverageAsync()
        {
            return Task.Run(this.GetModelAverage);
        }
    }
}
