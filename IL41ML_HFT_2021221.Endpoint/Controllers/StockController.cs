using IL41ML_HFT_2021221.Endpoint.Services;
using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Logic.DataType;
using IL41ML_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IL41ML_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        IStockLogic stockLogic;
        IHubContext<SignalRHub> hub;
        public StockController(IStockLogic stockLogic, IHubContext<SignalRHub> hub)
        {
            this.stockLogic = stockLogic;
            this.hub = hub;
        }

        [HttpGet("[action]")] //GET: stock/AveragePriceOfModels
        public double AveragePriceOfModels()
        {
            return stockLogic.AveragePriceOfModels();
        }

        [HttpGet("[action]")] // GET: stock/GetModelAverage
        public IList<NameAndDouble> GetModelAverage()
        {
            return stockLogic.GetModelAverage();
        }
        /*
        [HttpGet("[action]")] // GET: stock/GetModelAverageAsync
        public Task<IList<NameAndDouble>> GetModelAverageAsync()
        {
            return stockLogic.GetModelAverageAsync();
        }
        */
        [HttpPost]
        public void InsertModel([FromBody] Model value)
        {
            //stockLogic.InsertModel(value.BrandId, value.Name, value.ModelName, value.Size, value.Color, value.Price);
            stockLogic.InsertModel(value);
        }

        [HttpGet("[action]")] // GET: stock/ListBrandAverages
        public IEnumerable<NameAndDouble> ListBrandAverages()
        {
            return stockLogic.ListBrandAverages();
        }

        /*[HttpGet("[action]")] // GET: stock/ListBrandAveragesAsync
        public Task<IEnumerable<NameAndDouble>> ListBrandAveragesAsync()
        {
            return stockLogic.ListBrandAveragesAsync();
        }*/

        [HttpGet("[action]/{id}")] // GET: stock/ListBrandByID/"id"
        public Brand ListBrandByID(int id)
        {
            return stockLogic.ListBrandByID(id);
        }

        [HttpGet("[action]")] // GET: stock/ListBrands
        public IList<Brand> ListBrands()
        {
            return stockLogic.ListBrands();
        }

        [HttpGet("[action]")] // GET: stock/ListCheapestModel
        public Model ListCheapestModel()
        {
            return stockLogic.ListCheapestModel();
        }

        [HttpGet("[action]")] // GET: stock/ListLeastExpensiveBrand
        public NameAndDouble ListLeastExpensiveBrand()
        {
            return stockLogic.ListLeastExpensiveBrand();
        }
        /*
        [HttpGet("[action]")] // GET: stock/ListLeastExpensiveBrandAsync
        public Task<NameAndDouble> ListLeastExpensiveBrandAsync()
        {
            return stockLogic.ListLeastExpensiveBrandAsync();
        }
        */
        [HttpGet("[action]/{id}")] // GET: stock/ListModelByID/"id"
        public Model ListModelByID(int id)
        {
            return stockLogic.ListModelByID(id);
        }

        [HttpGet("[action]")] // GET: stock/ListModels
        public IList<Model> ListModels()
        {
            return stockLogic.ListModels();
        }

        [HttpGet("[action]")] // GET: stock/ListModelsByBrand?brand={name}
        public IList<Model> ListModelsByBrand(string brand)
        {
            return stockLogic.ListModelsByBrand(brand);
        }

        [HttpGet("[action]")] // GET: stock/ListMostExpensiveBrand
        public NameAndDouble ListMostExpensiveBrand()
        {
            return stockLogic.ListMostExpensiveBrand();
        }
        /*
        [HttpGet("[action]")] // GET: stock/ListMostExpensiveBrandAsync
        public Task<NameAndDouble> ListMostExpensiveBrandAsync()
        {
            return stockLogic.ListMostExpensiveBrandAsync();
        }
        */
        [HttpGet("[action]")] // GET: stock/MostExpensiveModel
        public Model MostExpensiveModel()
        {
            return stockLogic.MostExpensiveModel();
        }
    }
}
