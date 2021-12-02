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

        public void ChangeBrandCEO(int id, string ceo)
        {
            this.brandRepo.ChangeCEO(id, ceo);
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

        public void InsertBrand(Brand input)
        {
            if (input.Name == null || input.Country == null || input.CEO == null || input.Source == null || input.Foundation == /*null*/ new DateTime())
            {
                throw new ArgumentNullException(nameof(input.Name));
            }
            else
            {
                this.brandRepo.Insert(input);
            }
        }

        public void InsertService(Service input)
        {
            if (this.brandRepo.GetAll().Count() < input.BrandId || input.BrandId <= 0 || input.ServiceName == null || input.Country == null || input.City == null || input.Address == null || input.WebPage == null || input.PhoneNr == null)
            {
                throw new ArgumentNullException(nameof(input.ServiceName));
            }
            else
            {
                this.serviceRepo.Insert(input);
            }
        }

        public void InsertShop(Shop input)
        {
            if (this.brandRepo.GetAll().Count() < input.BrandId || input.BrandId <= 0 || this.serviceRepo.GetAll().Count() < input.ServiceId || input.ServiceId <= 0 || input.Name == null || input.Country == null || input.City == null || input.Address == null || input.Phone == null)
            {
                throw new ArgumentNullException(nameof(input.Name));
            }
            else
            {
                this.shopRepo.Insert(input);
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
