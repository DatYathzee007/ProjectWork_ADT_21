using IL41ML_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic
{
    public interface IManagerLogic
    {
        object GetItem(string table,int id);
        void InsertBrand(Brand input);
        void InsertModel(Model input);
        void InsertShop(Shop input);
        void InsertService(Service input);
        void ChangeBrandCEO(int id, string ceo);
        void ChangeModelPrice(int id, int price);
        void ChangeServiceWeb(int id, string web);
        void ChangeServiceName(int id, string name);
        void ChangeServicePhone(int id, string phone);
        void ChangeShopName(int id, string name);
        void ChangeShopPhone(int id, string phone);
        void RemoveEntity(string data, int id);
        void UpdateBrand(Brand input);
        void UpdateModel(Model input);
        void UpdateService(Service input);
        void UpdateShop(Shop input);
    }

}
