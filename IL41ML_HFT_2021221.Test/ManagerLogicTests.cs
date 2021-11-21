using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Models;
using IL41ML_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Test
{
    [TestFixture]
    public class ManagerLogicTests
    {
        private ManagerLogic logic;
        private Mock<IBrandRepository> mBrandRepo;
        private Mock<IModelRepository> mModelRepo;
        private Mock<IServiceRepository> mServiceRepo;
        private Mock<IShopRepository> mShopRepo;

        [SetUp]
        public void Setup()
        {
            this.mModelRepo = new Mock<IModelRepository>(MockBehavior.Loose);
            this.mBrandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            this.mShopRepo = new Mock<IShopRepository>(MockBehavior.Loose);
            this.mServiceRepo = new Mock<IServiceRepository>(MockBehavior.Loose);
            this.logic = new ManagerLogic(this.mBrandRepo.Object, this.mModelRepo.Object, this.mServiceRepo.Object, this.mShopRepo.Object);
        }

        [Test]
        public void TestRemoveEntity()
        {
            Brand apple = new Brand() { Id = 1, Name = "Apple" };
            Brand samsung = new Brand() { Id = 2, Name = "Samsung" };
            List<Brand> brands = new List<Brand> { apple, samsung };
            List<Model> models = new List<Model>()
            {
                new Model() { Id = 1, Brand = apple, BrandId = apple.Id, Name = "Iphone", ModelName = "12", Price = 30000, Size = 64 },
                new Model() { Id = 2, Brand = apple, BrandId = apple.Id, Name = "Iphone", ModelName = "X", Price = 10000, Size = 64 },
                new Model() { Id = 3, Brand = samsung, BrandId = samsung.Id, Name = "Galaxy", ModelName = "s10", Price = 10000, Size = 128 },
                new Model() { Id = 4, Brand = samsung, BrandId = samsung.Id, Name = "Galaxy", ModelName = "s20", Price = 20000, Size = 128 },
            };
            List<Service> services = new List<Service>()
            {
                new Service() { Id = 1, Brand = apple, BrandId = apple.Id, ServiceName = "Apple Service 1" },
                new Service() { Id = 2, Brand = apple, BrandId = apple.Id, ServiceName = "Apple Service 2" },
                new Service() { Id = 3, Brand = samsung, BrandId = samsung.Id, ServiceName = "Samsung Service 1" },
                new Service() { Id = 4, Brand = samsung, BrandId = samsung.Id, ServiceName = "Samsung Service 2" },
            };
            List<Shop> shops = new List<Shop>()
            {
                new Shop() { Id = 1, Brand = apple, BrandId = apple.Id, ServiceId = services[0].Id, Name = "Apple Shop 1" },
                new Shop() { Id = 2, Brand = apple, BrandId = apple.Id, ServiceId = services[1].Id, Name = "Apple Shop 2" },
                new Shop() { Id = 3, Brand = samsung, BrandId = samsung.Id, ServiceId = services[2].Id, Name = "Samsung Shop 1" },
                new Shop() { Id = 1, Brand = samsung, BrandId = samsung.Id, ServiceId = services[3].Id, Name = "Samsung Shop 2" },
            };

            this.mBrandRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            this.mModelRepo.Setup(repo => repo.GetAll()).Returns(models.AsQueryable());
            this.mServiceRepo.Setup(repo => repo.GetAll()).Returns(services.AsQueryable());
            this.mShopRepo.Setup(repo => repo.GetAll()).Returns(shops.AsQueryable());
            int newBrandID = 0;
            this.mBrandRepo.Setup(repo => repo.Remove(It.IsAny<int>())).Callback<int>(x => newBrandID = x);
            this.logic.RemoveEntity("brand", 2);
            Brand expectedBrand = new Brand() { Id = 2, Name = "Samsung" };

            this.mBrandRepo.Verify(repo => repo.Remove(expectedBrand.Id), Times.Once);
            this.mBrandRepo.Verify(repo => repo.Remove(It.IsAny<int>()), Times.Exactly(1));
        }

        [Test]
        public void TestChangeServiceName()
        {
            List<Service> services = new List<Service>()
            {
                new Service() { Id = 1, ServiceName = "Apple Service 1" },
                new Service() { Id = 2, ServiceName = "Apple Service 2" },
                new Service() { Id = 3, ServiceName = "Samsung Service 1" },
                new Service() { Id = 4, ServiceName = "Samsung Service 2" },
            };
            string serviceName = string.Empty;
            int serviceID = 0;
            this.mServiceRepo.Setup(repo => repo.GetAll()).Returns(services.AsQueryable());
            this.mServiceRepo.Setup(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>())).Callback<int, string>((id, st) =>
            {
                serviceID = id;
                serviceName = st;
            });
            this.logic.ChangeServiceName(4, "New Apple Service");
            Service expectedService = new Service() { Id = 4, ServiceName = "New Apple Service" };

            this.mServiceRepo.Verify(repo => repo.ChangeName(expectedService.Id, expectedService.ServiceName), Times.Once);
        }
    }
}
