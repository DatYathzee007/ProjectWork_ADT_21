using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic
{
    internal interface IManagerLogic
    {
        void InsertBrand(string name, string country, string ceo, string source, DateTime foundation);
        void InsertShop(int brandid, int serviceid, string name, string country, string city, string phone, string address);
        void InsertService(int brandid, string name, string country, string city, string address, string web, string phone);
        void ChangeBrandCEO(int id, string name);
        void ChangeModelPrice(int id, int price);
        void ChangeServiceWeb(int id, string web);
        void ChangeServiceName(int id, string name);
        void ChangeServicePhone(int id, string phone);
        void ChangeShopName(int id, string name);
        void ChangeShopPhone(int id, string phone);
        void RemoveEntity(string data, int id);
    }

}
