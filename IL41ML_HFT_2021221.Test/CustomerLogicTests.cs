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
    public class CostumerLogicTests
    {
        private CustomerLogic clogic;
        private Mock<IModelRepository> mockedMRepo;
        private Mock<IBrandRepository> mockedBRepo;
        private Mock<IShopRepository> mockedShRepo;
        private Mock<IServiceRepository> mockedSRepo;

        [SetUp]
        public void Setup()
        {
            this.mockedMRepo = new Mock<IModelRepository>(MockBehavior.Loose);
            this.mockedBRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            this.mockedShRepo = new Mock<IShopRepository>(MockBehavior.Loose);
            this.mockedSRepo = new Mock<IServiceRepository>(MockBehavior.Loose);
            this.clogic = new CustomerLogic(this.mockedBRepo.Object, this.mockedMRepo.Object, this.mockedSRepo.Object, this.mockedShRepo.Object);
        }

        [Test]
        public void TestListOneShop()
        {
            // arrange phase
            List<Shop> shops = new List<Shop>()
            {
                new Shop() { Id = 1, Name = "Shop 1" },
                new Shop() { Id = 2, Name = "Shop 2" },
                new Shop() { Id = 3, Name = "Shop 3" },
            };
            Shop expectedShop = new Shop() { Id = 3, Name = "Shop 3" };
            this.mockedShRepo.Setup(repo => repo.GetAll()).Returns(shops.AsQueryable());

            // act phase
            var result = this.clogic.ListShopByID(3);

            // Assert phase
            this.mockedShRepo.Verify(repo => repo.GetOne(3), Times.Once);
            this.mockedShRepo.Verify(repo => repo.GetOne(48), Times.Never);
            this.mockedShRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
        }

        [Test] // CRUD test => GetAll()
        public void TestListModels()
        {
            // arrange phase
            List<Model> models = new List<Model>() // only need the parameters that are required for the operation, ( GetAll() )
            {
                new Model() { BrandId = 1, Name = "Iphone", ModelName = "12 XSMAXMiniPro" },
                new Model() { BrandId = 2, Name = "Galaxy", ModelName = "S20+" },
                new Model() { BrandId = 3, Name = "P30", ModelName = "Pro" },
                new Model() { BrandId = 4, Name = "RedMi", ModelName = "Note 9" },
                new Model() { BrandId = 1, Name = "Iphone", ModelName = "12 Mini" },
            };
            List<Model> expectedModels = new List<Model>() { models[0], models[1], models[2], models[3], models[4] };
            this.mockedMRepo.Setup(repo => repo.GetAll()).Returns(models.AsQueryable());

            // act phase
            var result = this.clogic.ListModels();

            // Asssert phase
            this.mockedMRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.mockedMRepo.Verify(repo => repo.GetOne(30), Times.Never);
            this.mockedMRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        [Test] // CRUD test => GetAll() by brand
        public void TestListModelsByBrand()
        {
            // arrange phase
            List<Brand> brands = new List<Brand>()
            {
                new Brand() { Id = 1, Name = "Apple" },
                new Brand() { Id = 2, Name = "Samsung" },
                new Brand() { Id = 3, Name = "Huawei" },
                new Brand() { Id = 4, Name = "Xiaomi" },
            };

            List<Model> models = new List<Model>() // only need the parameters that are required for the operation, ( GetAll() )
            {
                new Model() { BrandId = 1, Name = "Iphone", ModelName = "12 XSMAXMiniPro" },
                new Model() { BrandId = 2, Name = "Galaxy", ModelName = "S20+" },
                new Model() { BrandId = 3, Name = "P30", ModelName = "Pro" },
                new Model() { BrandId = 4, Name = "RedMi", ModelName = "Note 9" },
                new Model() { BrandId = 1, Name = "Iphone", ModelName = "12 Mini" },
            };

            List<Model> expectedModels = new List<Model>() { models[0], models[4] };
            this.mockedBRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            this.mockedMRepo.Setup(repo => repo.GetAll()).Returns(models.AsQueryable());

            // act phase
            var result = this.clogic.ListModelsByBrand("Apple");

            // Asssert phase
            this.mockedMRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.mockedMRepo.Verify(repo => repo.GetOne(30), Times.Never);
            this.mockedMRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        [Test] // CRUD test => GetAll() by brand
        public void TestListServiceByBrand()
        {
            // arrange phase
            List<Brand> brands = new List<Brand>()
            {
                new Brand() { Id = 1, Name = "Apple" },
                new Brand() { Id = 2, Name = "Samsung" },
                new Brand() { Id = 3, Name = "Huawei" },
                new Brand() { Id = 4, Name = "Xiaomi" },
            };

            List<Service> services = new List<Service>() // only need the parameters that are required for the operation, ( GetAll() )
            {
                new Service() { BrandId = 1, ServiceName = "Service 1" },
                new Service() { BrandId = 2, ServiceName = "Service 2" },
                new Service() { BrandId = 3, ServiceName = "Service 3" },
                new Service() { BrandId = 2, ServiceName = "Service 4" },
                new Service() { BrandId = 1, ServiceName = "Service 5" },
            };

            List<Service> expectedModels = new List<Service>() { services[0], services[4] };
            this.mockedBRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            this.mockedSRepo.Setup(repo => repo.GetAll()).Returns(services.AsQueryable());

            // act phase
            var result = this.clogic.ListServiceByBrand("Apple");

            // Asssert phase
            this.mockedSRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.mockedSRepo.Verify(repo => repo.GetOne(30), Times.Never);
            this.mockedSRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }
    }
}
