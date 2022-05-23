﻿using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Models;
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

        [HttpDelete("{table}/{id}")]
        public void Delete(string table, int id)
        {
            this.managerLogic.RemoveEntity(table, id);
        }
        [HttpPost("[action]")]  // GET: manager/InsertBrand
        public void InsertBrand([FromBody] Brand value)
        {
            managerLogic.InsertBrand(value);
        }
        [HttpPost("[action]")] // GET: manager/InsertModel
        public void InsertModel([FromBody] Model value)
        {
            managerLogic.InsertModel(value);
        }
            [HttpPost("[action]")] // GET: manager/InsertShop
        public void InsertShop([FromBody] Shop value)
        {
            managerLogic.InsertShop(value);
        }
        [HttpPost("[action]")] // GET: manager/InsertService
        public void InsertService([FromBody] Service value)
        {
            managerLogic.InsertService(value);
        }
        [HttpPut("[action]")] // PUT: manager/UpdateBrand
        public void UpdateBrand([FromBody] Brand value)
        {
            managerLogic.UpdateBrand(value);
        }
        [HttpPut("[action]")] // PUT: manager/UpdateModel
        public void UpdateModel([FromBody] Model value)
        {
            managerLogic.UpdateModel(value);
        }
        [HttpPut("[action]")] // PUT: manager/UpdateService
        public void UpdateService([FromBody] Service value)
        {
            managerLogic.UpdateService(value);
        }
        [HttpPut("[action]")] // PUT: manager/UpdateShop
        public void UpdateShop([FromBody] Shop value)
        {
            managerLogic.UpdateShop(value);
        }
        [HttpPut("[action]")]
        public void ChangeBrandCEO([FromBody] Brand value)
        {
            managerLogic.ChangeBrandCEO(value.Id, value.CEO);
        }
        [HttpPut("[action]")]
        public void ChangeModelPrice([FromBody] Model value)
        {
            managerLogic.ChangeModelPrice(value.Id, value.Price);
        }
        [HttpPut("[action]")]
        public void ChangeServiceWeb([FromBody] Service value)
        {
            managerLogic.ChangeServiceWeb(value.Id, value.WebPage);
        }
        [HttpPut("[action]")]
        public void ChangeServiceName([FromBody] Service value)
        {
            managerLogic.ChangeServiceName(value.Id, value.ServiceName);
        }
        [HttpPut("[action]")]
        public void ChangeServicePhone([FromBody] Service value)
        {
            managerLogic.ChangeServicePhone(value.Id, value.PhoneNr);
        }
        [HttpPut("[action]")]
        public void ChangeShopName([FromBody] Shop value)
        {
            managerLogic.ChangeShopName(value.Id, value.Name);
        }
        [HttpPut("[action]")]
        public void ChangeShopPhone([FromBody] Shop value)
        {
            managerLogic.ChangeShopPhone(value.Id, value.Phone);
        }
    }
}
