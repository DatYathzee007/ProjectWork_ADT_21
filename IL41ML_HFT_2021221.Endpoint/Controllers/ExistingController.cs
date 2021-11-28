using IL41ML_HFT_2021221.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExistingController : Controller
    {
        IExistingData existLogic;
        public ExistingController(IExistingData existLogic)
        {
            this.existLogic = existLogic;
        }
        [HttpGet("[action]/table={table}&id={id}")] // get: existing/IsExisting
        public bool IsExisting(int id, string table)
        {
            return existLogic.IsExisting(id, table);
        }
    }
}
