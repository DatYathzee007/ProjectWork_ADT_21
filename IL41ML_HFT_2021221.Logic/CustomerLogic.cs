using IL41ML_HFT_2021221.Models;
using IL41ML_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic
{
    public class CustomerLogic : ICustomerLogic
    {
        private IBrandRepository brandRepo;
        private IModelRepository modelRepo;
        private IShopRepository shopRepo;
        private IServiceRepository serviceRepo;

        public CustomerLogic(IBrandRepository brandRepo, IModelRepository modelRepo, IServiceRepository serviceRepo, IShopRepository shopRepo)
        {
            this.brandRepo = brandRepo;
            this.modelRepo = modelRepo;
            this.serviceRepo = serviceRepo;
            this.shopRepo = shopRepo;
        }

        /*
        public IEnumerable<string> OLDListShopsAndServiceINSpecificLocByBrand(string brandname, string shoplocation)
        {
            return this.brandRepo.GetAll()
               .Where(x => x.Name == brandname)
               .Join(this.shopRepo.GetAll(), brand => brand.Id, shop => shop.BrandId, (brand, shop) => new { brand, shop })
               .Where(x => x.shop.Address.Contains(shoplocation))
               .Join(this.serviceRepo.GetAll(), brasho => brasho.shop.ServiceId, service => service.Id, (brasho, service) => new { brasho, service })
               .Where(x => x.service.Address.Contains(shoplocation))
               .Select(x => x.brasho.brand.Name + ", Shop Name: " + x.brasho.shop.Name + " Shop Address: " + x.brasho.shop.Address + " Service Name: " + x.service.ServiceName + " Service Address: " + x.service.Address)
               .ToList();
        }
        */

        public IEnumerable<string> ListShopsAndServiceINSpecificLocByBrand(string brandname, string shoplocation)
        {
            var result = from brand in this.brandRepo.GetAll()
                         where brand.Name == brandname
                         join shop in this.shopRepo.GetAll() on brand.Id equals shop.BrandId
                         where shop.Address.Contains(shoplocation)
                         join service in this.serviceRepo.GetAll() on shop.ServiceId equals service.Id
                         where service.Address.Contains(shoplocation)
                         select $"BrandName: {brand.Name}| ShopName: {shop.Name}| ShopAddress: {shop.Address}| ServiceName: {service.ServiceName}| ServiceAddress: {service.Address}";
            return result;
        }

        public Task<IEnumerable<string>> ListShopsAndServiceINSpecificLocByBrandAsync(string brand, string shoplocation)
        {
            return Task.Run(() => this.ListShopsAndServiceINSpecificLocByBrand(brand, shoplocation));
        }

        /*
        public IEnumerable<string> OLDListAllEntityByBrand(string brand)
        {
            IEnumerable<string> result = this.brandRepo.GetAll()
               .Where(x => x.Name == brand)
               .Select(x => "Brands: " + x.ToString())
               .ToList();
            IEnumerable<string> result2 = this.modelRepo.GetAll()
                .Where(x => x.BrandId.Equals(this.brandRepo.GetAll().Single(x => x.Name == brand).Id))
                .Select(x => "Model: " + x.ToString())
                .ToList();
            IEnumerable<string> result3 = this.serviceRepo.GetAll()
                .Where(x => x.BrandId.Equals(this.brandRepo.GetAll().Single(x => x.Name == brand).Id))
                .Select(x => "Service: " + x.ToString())
                .ToList();
            IEnumerable<string> result4 = this.shopRepo.GetAll()
                .Where(x => x.BrandId.Equals(this.brandRepo.GetAll().Single(x => x.Name == brand).Id))
                .Select(x => "Shop: " + x.ToString())
                .ToList();
            return result.Union(result2).Union(result3).Union(result4);
        }
        */

        public IEnumerable<string> ListAllEntityByBrand(string brandName)
        {
            int selectedBrandID = this.brandRepo.GetAll().Single(x => x.Name == brandName).Id;
            IEnumerable<string> result = from brand in this.brandRepo.GetAll()
                                         where brand.Id == selectedBrandID
                                         select $"Brand: {brand}";
            IEnumerable<string> result2 = from model in this.modelRepo.GetAll()
                                          where model.BrandId == selectedBrandID
                                          select $"Model: {model}";
            IEnumerable<string> result3 = from service in this.serviceRepo.GetAll()
                                          where service.BrandId == selectedBrandID
                                          select $"Service: {service}";
            IEnumerable<string> result4 = from shop in this.shopRepo.GetAll()
                                          where shop.BrandId == selectedBrandID
                                          select $"Shop: {shop}";
            return result.Concat(result2).Concat(result3).Concat(result4);
        }

        public Model ListModelByID(int id)
        {
            return this.modelRepo.GetOne(id);
        }

        public IList<Model> ListModels()
        {
            return this.modelRepo.GetAll().ToList();
        }

        /*
        public IList<Model> OLDListModelsByBrand(string brand)
        {
            return this.modelRepo.GetAll()
                .Where(x => x.BrandId.Equals(this.brandRepo.GetAll().Single(x => x.Name == brand).Id))
                .ToList();
        }
        */

        public IList<Model> ListModelsByBrand(string brandName)
        {
            var q = from model in this.modelRepo.GetAll()
                    where model.BrandId == this.brandRepo.GetAll().Single(branditem => branditem.Name == brandName).Id
                    select model;
            return q.ToList();
        }

        
        public IList<string> ListModelsByPriceRange(int lowerBound, int upperBound)
        {
            var q = this.modelRepo.GetAll()
            .Where(x => x.Price > lowerBound && x.Price < upperBound)
            .Select(x => new { x.Id, Name = x.Brand.Name + " " + x.Name + " " + x.ModelName, x.Price, x.Size, PriceValueRatio = x.Price / (x.Size == 0 ? 1 : x.Size) })
            .OrderBy(x => x.PriceValueRatio)
            .ThenBy(x => x.Price)
            .Select(x => $"ID: {x.Id}\t Name: {x.Name}\t Size: {x.Size}  \tPrice: {x.Price} \tRatio: {x.PriceValueRatio}")
            .ToList();
            ((List<string>)q).Add("[Ratio is Price over StorageSize. Simply a cost of 1GB storages.]");
            return q;
        }
        
        /*
        public IList<string> OLDListModelsByPriceRange(int lowerBound, int upperBound)
        {
            var q1 = this.modelRepo.GetAll().Where(x => x.Price > lowerBound && x.Price < upperBound)//.Where(x => x.Price > lowerBound && x.Price < upperBound) //without it where it works fine. 
                    .Select(x => $"ID: {x.Id}, Name: {x.Name} {x.ModelName}, Price:{x.Price}").ToList();

            var q = from model in this.modelRepo.GetAll()
                    where model.Price > lowerBound && model.Price < upperBound
                    select new { model.Id, Name = model.Brand.Name + " " + model.Name + " " + model.ModelName, model.Price, model.Size, PriceValueRatio = model.Price / (model.Size == 0 ? 1 : model.Size) }
                    into newList
                    orderby newList.PriceValueRatio
                    orderby newList.Price
                    select $"ID: {newList.Id}\t Name: {newList.Name}\t Size: {newList.Size}  \t Price: {newList.Price} \t Ratio: {newList.PriceValueRatio}";
            
            return q.ToList();
        }
        */
        /*
        public IList<Service> OLDListServiceByBrand(string brand)
        {
            return this.serviceRepo.GetAll()
                .Where(x => x.BrandId.Equals(this.brandRepo.GetAll().Single(x => x.Name == brand).Id))
                .ToList();
        }
        */

        public IList<Service> ListServiceByBrand(string brand)
        {
            var q = from service in this.serviceRepo.GetAll()
                    where service.BrandId == this.brandRepo.GetAll().Single(brandItem => brandItem.Name == brand).Id
                    select service;
            return q.ToList();
        }

        public Service ListServiceByID(int id)
        {
            return this.serviceRepo.GetOne(id);
        }

        public IList<Service> ListServices()
        {
            return this.serviceRepo.GetAll().ToList();
        }

        public Shop ListShopByID(int id)
        {
            return this.shopRepo.GetOne(id);
        }

        public IList<Shop> ListShops()
        {
            return this.shopRepo.GetAll().ToList();
        }

        /*
        public IEnumerable<string> OLDListShopsAndServicesBySpecificModel(int id)
        {
            IEnumerable<string> result = this.shopRepo.GetAll()
                .Where(x => x.BrandId.Equals(this.modelRepo.GetOne(id).BrandId))
                .Select(x => "Shop: " + x.ToString())
                .ToList();
            IEnumerable<string> result2 = this.serviceRepo.GetAll()
                .Where(x => x.BrandId.Equals(this.modelRepo.GetOne(id).BrandId))
                .Select(x => "Service: " + x.ToString())
                .ToList();

            return result.Union(result2);
        }
        */

        public IEnumerable<string> ListShopsAndServicesBySpecificModel(int id)
        {
            int selectedBrandID = this.modelRepo.GetOne(id).BrandId;
            IEnumerable<string> q1 = from shop in this.shopRepo.GetAll()
                                     where shop.BrandId == selectedBrandID
                                     select $"Shop: {shop}";
            IEnumerable<string> q2 = from service in this.serviceRepo.GetAll()
                                     where service.BrandId == selectedBrandID
                                     select $"Service: {service}";
            return q1.Concat(q2);
        }

        /*
        public IList<Shop> OLDListShopsByBrand(string brand)
        {
            return this.shopRepo.GetAll()
                .Where(x => x.BrandId.Equals(this.brandRepo.GetAll().Single(x => x.Name == brand).Id))
                .ToList();
        }
        */

        public IList<Shop> ListShopsByBrand(string brand)
        {
            var q1 = from shop in this.shopRepo.GetAll()
                     where shop.BrandId == this.brandRepo.GetAll().Single(x => x.Name == brand).Id
                     select shop;
            return q1.ToList();
        }
    }
}
