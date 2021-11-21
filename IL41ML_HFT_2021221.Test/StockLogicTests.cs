using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Logic.DataType;
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
    public class StockLogicTests
    {
        private StockLogic logic;
        private Mock<IModelRepository> modelRepo;
        private Mock<IBrandRepository> brandRepo;
        private List<NameAndDouble> expectedAverage;
        private List<NameAndDouble> expectedModelAverage;

        [SetUp]
        public void Setup()
        {
            this.modelRepo = new Mock<IModelRepository>();
            this.brandRepo = new Mock<IBrandRepository>();

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
            this.expectedAverage = new List<NameAndDouble>()
            {
                new NameAndDouble() { Name = "Apple", Average = 20000 },
                new NameAndDouble() { Name = "Samsung", Average = 15000 },
            };
            this.expectedModelAverage = new List<NameAndDouble>()
            {
                new NameAndDouble() { Name = "Iphone", Average = 20000 },
                new NameAndDouble() { Name = "Galaxy", Average = 15000 },
            };
            this.modelRepo.Setup(repo => repo.GetAll()).Returns(models.AsQueryable());
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            this.logic = new StockLogic(this.brandRepo.Object, this.modelRepo.Object);
        }

        [Test]
        public void TestListBrands()
        {
            List<Brand> expectedBrands = new List<Brand>()
            {
                new Brand() { Id = 1, Name = "Apple" },
                new Brand() { Id = 2, Name = "Samsung" },
            };
            var result = this.logic.ListBrands();

            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.brandRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void TestInsertModel()
        {
            List<Brand> brands = new List<Brand>()
            {
                new Brand() { Id = 1, Name = "Apple" },
                new Brand() { Id = 2, Name = "Samsung" },
            };
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            Model newModel = new Model();

            this.modelRepo.Setup(repo => repo.Insert(It.IsAny<Model>())).Callback<Model>(x => newModel = x);

            this.logic.InsertModel(1, "Iphone", "3G", 8, "Black", 999);

            Model expectedModel = new Model() { Id = 1, BrandId = 1, Name = "Iphone", ModelName = "3G", Size = 8, Color = "Black", Price = 999 };

            /*
            Assert.That(expectedModel.BrandId == newModel.BrandId);
            Assert.That(expectedModel.Name == newModel.Name);
            Assert.That(expectedModel.ModelName == newModel.ModelName);
            Assert.That(expectedModel.Size == newModel.Size);
            Assert.That(expectedModel.Color == newModel.Color);
            Assert.That(expectedModel.Price == newModel.Price);
            */

            this.modelRepo.Verify(repo => repo.Insert(It.IsAny<Model>()), Times.Once);
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.brandRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void TestListMostExpensiveBrand()
        {
            var actualAverages = this.logic.ListMostExpensiveBrand();
            NameAndDouble expectedAVGMost = new NameAndDouble() { Name = "Apple", Average = 20000 };
            Assert.That(actualAverages.Name, Is.EqualTo(expectedAVGMost.Name));
            Assert.That(actualAverages.Average, Is.EqualTo(expectedAVGMost.Average));
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.modelRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Test]
        public void TestListBrandAverages()
        {
            var actualAverages = this.logic.ListBrandAverages();

            Assert.That(actualAverages, Is.EquivalentTo(this.expectedAverage));
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.modelRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Test]
        public void TestGetModelAverage()
        {
            var actualAverages = this.logic.GetModelAverage();

            Assert.That(actualAverages, Is.EquivalentTo(this.expectedModelAverage));
            this.modelRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /* MOCK CREATOR----------------------------
        private StockLogic CreateLogicWithMocks()
        {
            this.modelRepo = new Mock<IModelRepository>();
            this.brandRepo = new Mock<IBrandRepository>();

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
            this.expectedAverage = new List<NameAndDouble>()
            {
                new NameAndDouble() { Name = "Apple", Average = 20000 },
                new NameAndDouble() { Name = "Samsung", Average = 15000 },
            };
            this.expectedModelAverage = new List<NameAndDouble>()
            {
                new NameAndDouble() { Name = "Iphone", Average = 20000 },
                new NameAndDouble() { Name = "Galaxy", Average = 15000 },
            };
            this.modelRepo.Setup(repo => repo.GetAll()).Returns(models.AsQueryable());
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            return new StockLogic(this.brandRepo.Object, this.modelRepo.Object);
        }
        */
    }
}
