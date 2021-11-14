using IL41ML_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IL41ML_HFT_2021221.Repository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        void ChangeCEO(int id, string newCEO);
    }
}
