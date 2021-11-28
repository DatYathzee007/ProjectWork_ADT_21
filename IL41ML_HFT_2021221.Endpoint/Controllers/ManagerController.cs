using IL41ML_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IL41ML_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        IManagerLogic managerLogic;
        public ManagerController(IManagerLogic managerLogic)
        {
            this.managerLogic = managerLogic;
        }
        // GET: api/<ManagerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ManagerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManagerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ManagerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManagerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.managerLogic.RemoveEntity("Brand", id);
        }

        public void InsertBrand(string name, string country, string ceo, string source, DateTime foundation)
        {
            throw new NotImplementedException();
        }

        public void InsertShop(int brandid, int serviceid, string name, string country, string city, string phone, string address)
        {
            throw new NotImplementedException();
        }

        public void InsertService(int brandid, string name, string country, string city, string address, string web, string phone)
        {
            throw new NotImplementedException();
        }

        public void ChangeBrandCEO(int id, string name)
        {
            throw new NotImplementedException();
        }

        public void ChangeModelPrice(int id, int price)
        {
            throw new NotImplementedException();
        }

        public void ChangeServiceWeb(int id, string web)
        {
            throw new NotImplementedException();
        }

        public void ChangeServiceName(int id, string name)
        {
            throw new NotImplementedException();
        }

        public void ChangeServicePhone(int id, string phone)
        {
            throw new NotImplementedException();
        }

        public void ChangeShopName(int id, string name)
        {
            throw new NotImplementedException();
        }

        public void ChangeShopPhone(int id, string phone)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntity(string data, int id)
        {
            throw new NotImplementedException();
        }
    }
}
