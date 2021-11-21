using IL41ML_HFT_2021221.Models;
using IL41ML_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic
{
    public class ManagerLogic : IManagerLogic
    {
        private IBrandRepository brandRepo;
        private IModelRepository modelRepo;
        private IServiceRepository serviceRepo;
        private IShopRepository shopRepo;

        public ManagerLogic(IBrandRepository brandRepo, IModelRepository modelRepo, IServiceRepository serviceRepo, IShopRepository shopRepo)
        {
            this.brandRepo = brandRepo;
            this.modelRepo = modelRepo;
            this.serviceRepo = serviceRepo;
            this.shopRepo = shopRepo;
        }

        public void ChangeBrandCEO(int id, string name)
        {
            this.brandRepo.ChangeCEO(id, name);
        }

        public void ChangeModelPrice(int id, int price)
        {
            this.modelRepo.ChangeModelPrice(id, price);
        }

        public void ChangeServiceName(int id, string name)
        {
            this.serviceRepo.ChangeName(id, name);
        }

        public void ChangeServicePhone(int id, string phone)
        {
            this.serviceRepo.ChangePhoneNumber(id, phone);
        }
        public void ChangeServiceWeb(int id, string web)
        {
            this.serviceRepo.ChangeWeb(id, web);
        }

        public void ChangeShopName(int id, string name)
        {
            this.shopRepo.ChangeName(id, name);
        }

        public void ChangeShopPhone(int id, string phone)
        {
            this.shopRepo.ChangePhoneNumber(id, phone);
        }

        public void InsertBrand(string name, string country, string ceo, string source, DateTime foundation)
        {
            if (name == null || country == null || ceo == null || source == null || foundation == /*null*/ new DateTime())
            {
                throw new ArgumentNullException(nameof(name));
            }
            else
            {
                Brand newBrand = new Brand()
                {
                    Name = name,
                    Country = country,
                    CEO = ceo,
                    Source = source,
                    Foundation = foundation,
                };
                this.brandRepo.Insert(newBrand);
            }
        }

        public void InsertService(int brandid, string name, string country, string city, string address, string web, string phone)
        {
            if (this.brandRepo.GetAll().Count() < brandid || brandid <= 0 || name == null || country == null || city == null || address == null || web == null || phone == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            else
            {
                Service newService = new Service()
                {
                    BrandId = brandid,
                    ServiceName = name,
                    Country = country,
                    City = city,
                    Address = address,
                    WebPage = web,
                    PhoneNr = phone,
                };
                this.serviceRepo.Insert(newService);
            }
        }

        public void InsertShop(int brandid, int serviceid, string name, string country, string city, string phone, string address)
        {
            if (this.brandRepo.GetAll().Count() < brandid || brandid <= 0 || this.serviceRepo.GetAll().Count() < serviceid || serviceid <= 0 || name == null || country == null || city == null || address == null || phone == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            else
            {
                Shop newShop = new Shop()
                {
                    BrandId = brandid,
                    ServiceId = serviceid,
                    Name = name,
                    Country = country,
                    City = city,
                    Phone = phone,
                    Address = address,
                };
                this.shopRepo.Insert(newShop);
            }
        }

        public void RemoveEntity(string data, int id)
        {
            if (data != null)
            {
                data = data.ToUpperInvariant();
            }
            else
            {
                throw new ArgumentNullException(nameof(data));
            }

            switch (data)
            {
                case "BRAND":
                    this.brandRepo.Remove(id);
                    break;
                case "MODEL":
                    this.modelRepo.Remove(id);
                    break;
                case "SERVICE":
                    this.serviceRepo.Remove(id);
                    break;
                case "SHOP":
                    this.shopRepo.Remove(id);
                    break;
                default:
                    throw new ArgumentException("Invalid brand", nameof(data));
            }
        }
    }
}
