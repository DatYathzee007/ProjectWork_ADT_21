using IL41ML_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Repository
{
    public interface IServiceRepository : IRepository<Service>
    {
        void ChangeName(int id, string newName);
        void ChangeWeb(int id, string newWeb);
        void ChangePhoneNumber(int id, string newPhoneNumber);
        void Update(Service entity);
    }
}
