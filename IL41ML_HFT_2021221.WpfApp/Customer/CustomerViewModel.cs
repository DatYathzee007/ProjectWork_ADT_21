using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IL41ML_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IL41ML_HFT_2021221.WpfApp
{
    public partial class CustomerViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private Brand selectedBrand;
        [ObservableProperty]
        private string selectedId;
        [ObservableProperty]
        private string selectedMin;
        [ObservableProperty]
        private string selectedMax;
        public ObservableCollection<object> Results { get; set; }
        public RestCollection<Model> Models { get; set; }

        public RestCollection<Brand> Brands { get; set; }
        public RestCollection<Service> Services { get; set; }
        public RestCollection<Shop> Shops { get; set; }
        public RestCollection<object> TempResult { get; set; }
        //public List<T> TempResult { get; set; }

        [ICommand]
        private void List0() //#0 List all entity by Brand: 
        {
            //*******GET: customer/ListAllEntityByBrand? brand = { name }**********

            var TempResult = new RestCollection<object>("http://localhost:20347/", "customer/", $"ListAllEntityByBrand?brand={selectedBrand.Name}");
            //TempResult.Filter($"ListAllEntityByBrand?brand={selectedBrand.Name}");
            //TempResult.FilterEnum($"ListAllEntityByBrand?brand={selectedBrand.Name}");
            Results.Clear();
            foreach (var item in TempResult)
            {
                Results.Add(item);
            }
        }
        [ICommand]
        private void List1() //#1 List model by id:
        {
            //********GET: customer/ListModelByID/{id}*********

            Results.Clear();
            var TempItem = Models.FilterOne($"ListModelByID/{selectedId}");
            Results.Add(TempItem);
            //if (IsDigitsOnly(selectedId) && rserv.GetSingle<bool>($"existing/IsExisting?table=model&id={selectedId}"))
            //{
            //    var TempItem = Models.FilterOne($"ListModelByID/{selectedId}");
            //    Results.Add(TempItem);
            //}
        }
        [ICommand]
        private void List2() //#2 List all models:
        {
            Results.Clear();
            foreach (var model in Models)
            {
                Results.Add(new { model.Id, BrandName = model.Brand.Name, model.Name, model.ModelName, model.Size, model.Color, Price = model.Price + " Ft" });
            }
        }
        [ICommand]
        private void List3() //#3 List models by brand:
        {
            Models.Filter($"ListModelsByBrand?brand={selectedBrand.Name}");
        }
        [ICommand]
        private void List4() //#4 List models between a price range:
        {
            //********GET: customer/ListModelsByPriceRange?lb={lowerbound}&ub={upperbound}********

            //var TempList = Models.Filter($"ListModelsByPriceRange?lb={selectedMin}&ub={selectedMax}");
            //Results.Clear();
            //foreach (var model in TempList)
            //{
            //    Results.Add(model);
            //}
        }
        [ICommand]
        private void List5() //#5 List Services by brand:
        {
            //***********GET: customer/ListShopsByBrand?brand={name}************

            //var TempList = Services.Filter($"ListServiceByBrand?brand={selectedBrand.Name}");
            //Results.Clear();
            //foreach (var model in TempList)
            //{
            //    Results.Add(model);
            //}
        }
        [ICommand]
        private void List6() //#6 List Services by id:
        {
            //********GET: customer/ListServiceByID/{id}*********
            Results.Clear();
            var TempItem = Services.FilterOne($"ListServiceByID/{selectedId}");
            Results.Add(TempItem);
        }
        [ICommand]
        private void List7() //#7 List all Services:
        {
            Results.Clear();
            foreach (var service in Services)
            {
                Results.Add(new { service.Id, service.ServiceName });
            }
        }
        [ICommand]
        private void List8() //#8 List shop by id:
        {
            //********GET: customer/ListShopByID/{id}*********
            Results.Clear();
            var TempItem = Shops.FilterOne($"ListShopByID/{selectedId}");
            Results.Add(TempItem);
        }
        [ICommand]
        private void List9() //#9 List all shops:
        {
            Results.Clear();
            foreach (var shop in Shops)
            {
                Results.Add(shop);
            }
        }
        [ICommand]
        private void List10() //#10 List shops and services accoring to a model id:
        {

        }
        [ICommand]
        private void List11() //#11 List shops by brand:
        {
            //***********GET: customer/ListShopsByBrand?brand={name}************

            //var TempList = Shops.Filter($"ListShopsByBrand?brand={selectedBrand.Name}");
            //Results.Clear();
            //foreach (var model in TempList)
            //{
            //    Results.Add(model);
            //}
        }
        [ICommand]
        private void List12() //#12 List shops and services by localation and brand:
        {
            Results.Clear();
            //GET: customer/ListModelsByBrand?brand={name}

        }
        public CustomerViewModel()
        {
            Results = new ObservableCollection<object>();

            Models = new RestCollection<Model>("http://localhost:20347/", "customer/", "ListModels", "hub"); //http://localhost:20347/customer/Listmodels

            Shops = new RestCollection<Shop>("http://localhost:20347/", "customer/", "ListShops", "hub");

            Services = new RestCollection<Service>("http://localhost:20347/", "customer/", "ListServices", "hub");

            Brands = new RestCollection<Brand>("http://localhost:20347/", "stock/", "ListBrands", "hub");

            //TempResult = new RestCollection<string>("http://localhost:20347/", "customer/");

        }
        public bool IsDigitsOnly(string str)
        {
            if (str != null)
            {
                foreach (char c in str)
                {
                    if (c < '0' || c > '9')
                        return false;
                }

                return true;
            }
            return false;
        }
    }
}
