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
        public RestCollection<string> TempResult { get; set; }
        private RestService rserv;
        [ICommand]
        private void List0() //GET: customer/ListAllEntityByBrand? brand = { name }
        {
            TempResult = new RestCollection<string>("http://localhost:20347/", "customer/", $"ListAllEntityByBrand?brand={selectedBrand.Name}", "hub");
            //TempResult.Filter($"ListAllEntityByBrand?brand={selectedBrand.Name}");
            //var TempList = TempResult.Filter($"ListAllEntityByBrand?brand={selectedBrand.Name}");
            Results.Clear();
            foreach (var item in TempResult)
            {
                Results.Add(item);
            }
        }
        [ICommand]
        private void List1() // GET: customer/ListModelByID/{id}
        {
            Results.Clear();
            if (IsDigitsOnly(selectedId) && rserv.GetSingle<bool>($"existing/IsExisting?table=model&id={selectedId}"))
            {
                var TempItem = Models.FilterOne($"ListModelByID/{selectedId}");
                Results.Add(TempItem);
            }
        }
        [ICommand]
        private void List2()
        {
            Results.Clear();
            foreach (var model in Models)
            {
                Results.Add(new {model.Id, BrandName = model.Brand.Name, model.Name, model.ModelName, model.Size, model.Color, Price = model.Price + " Ft"});
            }
        }
        [ICommand]
        private void List3() // GET: Customer/ListModelsByBrand?brand=Samsung
        {
            var TempList = Models.Filter($"ListModelsByBrand?brand={selectedBrand.Name}");
            Results.Clear();
            foreach (var model in TempList)
            {
                Results.Add(model);
            }
        }
        [ICommand]
        private void List4()
        {

        }
        [ICommand]
        private void List5()
        {

        }
        [ICommand]
        private void List6()
        {

        }
        [ICommand]
        private void List7()
        {
            Results.Clear();
            foreach (var service in Services)
            {
                Results.Add(new {service.Id, service.ServiceName });
            }
        }
        [ICommand]
        private void List8()
        {

        }
        [ICommand]
        private void List9()
        {
            Results.Clear();
            foreach (var shop in Shops)
            {
                Results.Add(shop);
            }
        }
        [ICommand]
        private void List10()
        {

        }
        [ICommand]
        private void List11()
        {

        }
        [ICommand]
        private void List12() 
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
            
            //List2Command = new RelayCommand(() => {
            //    Results.Clear();
            //    foreach (var model in Models)
            //    {
            //        Results.Add(model/*new { model.Id, BrandName = model.Brand.Name, model.Name, model.ModelName, model.Size, model.Color, Price = model.Price + " Ft" }*/);
            //    }
            //});
            //List7Command = new RelayCommand(() => {
            //    Results.Clear();
                
            //    foreach (var shop in Shops)
            //    {
            //        Results.Add(shop);
            //    }
            //});

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

        //private void Results_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
