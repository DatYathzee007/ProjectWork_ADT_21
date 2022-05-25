using IL41ML_HFT_2021221.Endpoint.Services;
using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IL41ML_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerLogic customerLogic;
        IHubContext<SignalRHub> hub;
        public CustomerController(ICustomerLogic customerLogic, IHubContext<SignalRHub> hub)
        {
            this.customerLogic = customerLogic;
            this.hub = hub;
        }

        [HttpGet("[action]")] // GET: customer/ListAll"
        public IList<string> ListAll() { return customerLogic.ListAll(); }
        [HttpGet("[action]")] // GET: customer/ListModels "[action]"
        public IList<Model> ListModels() { return customerLogic.ListModels(); }
        [HttpGet("[action]")] // GET: customer/ListServices
        public IList<Service> ListServices() { return customerLogic.ListServices(); }
        [HttpGet("[action]")] // GET: customer/ListShops
        public IList<Shop> ListShops() { return customerLogic.ListShops(); }
        [HttpGet("[action]/{id}")] // GET: customer/ListModelByID/{id}
        public Model ListModelByID(int id) { return customerLogic.ListModelByID(id); }
        [HttpGet("[action]/{id}")] // GET: customer/ListServiceByID/{id}
        public Service ListServiceByID(int id) { return customerLogic.ListServiceByID(id); }
        [HttpGet("[action]/{id}")] // GET: customer/ListShopByID/{id}
        public Shop ListShopByID(int id) { return customerLogic.ListShopByID(id); }
        [HttpGet("[action]")] // GET: customer/ListModelsByBrand?brand={name}
        public IList<Model> ListModelsByBrand(string brand) { return customerLogic.ListModelsByBrand(brand); }
        [HttpGet("[action]")] // GET: customer/ListServiceByBrand?brand={name}
        public IList<Service> ListServiceByBrand(string brand) { return customerLogic.ListServiceByBrand(brand); }
        [HttpGet("[action]")] // GET: customer/ListShopsByBrand?brand={name}
        public IList<Shop> ListShopsByBrand(string brand) { return customerLogic.ListShopsByBrand(brand); }
        [HttpGet("[action]")] // GET: customer/ListAllEntityByBrand?brand={name}
        public IEnumerable<string> ListAllEntityByBrand(string brand) { return customerLogic.ListAllEntityByBrand(brand); }
        [HttpGet("[action]")] // GET: customer/ListModelsByPriceRange?lb={lowerbound}&ub={upperbound}
        public IList<string> ListModelsByPriceRange(int lb, int ub) { return customerLogic.ListModelsByPriceRange(lb, ub); }
        [HttpGet("[action]/{id}")] // GET: customer/ListShopsAndServicesBySpecificModel/{id}
        public IEnumerable<string> ListShopsAndServicesBySpecificModel(int id) { return customerLogic.ListShopsAndServicesBySpecificModel(id); }
        [HttpGet("[action]")] // GET: customer/ListShopsAndServiceINSpecificLocByBrand?brand={name}&shoplocation={location}
        public IEnumerable<string> ListShopsAndServiceINSpecificLocByBrand(string brand, string shoplocation) { return customerLogic.ListShopsAndServiceINSpecificLocByBrand(brand, shoplocation); }
    }
}
