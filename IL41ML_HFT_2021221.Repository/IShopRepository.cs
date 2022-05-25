using IL41ML_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Repository
{
    public interface IShopRepository : IRepository<Shop>
    {
        void ChangePhoneNumber(int id, string newPhoneNumber);
        void ChangeName(int id, string newName);
        void Update(Shop entity);
    }
}
