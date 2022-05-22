using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IL41ML_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace IL41ML_HFT_2021221.WpfApp_v2
{
    public partial class CrudViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private string inputBrand;
        [ObservableProperty]
        private string inputModel;
        [ObservableProperty]
        private string inputService;
        [ObservableProperty]
        private string inputShop;
        [ObservableProperty]
        private Brand selectedBrand;
        [ObservableProperty]
        private Model selectedModel;
        [ObservableProperty]
        private Service selectedService;
        [ObservableProperty]
        private Shop selectedShop;

        public RestCollection<Brand> Brands { get; set; }
        public RestCollection<Model> Models { get; set; }
        public RestCollection<Service> Services { get; set; }
        public RestCollection<Shop> Shops { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        // BRAND CRUD commands
        [ICommand]
        private void GetOneBrand()
        {
            Brands.GetOne($"ListBrandByID/{inputBrand}", int.Parse(inputBrand));
        }
        [ICommand]
        private void GetAllBrand()
        {
            this.Brands.GetAll();
        }
        [ICommand]
        private void UpdateBrand()
        {
            if (selectedBrand != null)
            {
               
            }
        }
        [ICommand]
        private void CreateBrand()
        {

            if (selectedBrand != null)
            {
                Brand newBrand = new() {Name = selectedBrand.Name, CEO = selectedBrand.CEO, Country = selectedBrand.Country, Foundation = selectedBrand.Foundation, Source = selectedBrand.Source };
                Brands.Add(newBrand);
            }
        } 
        [ICommand]
        private void DeleteBrand()
        {
            if (selectedBrand != null)
            {
                Brands.Delete(selectedBrand.Id);
            }
            //this.rserv.Delete(id, $"manager/{entityName}");
        }
        // MODEL CRUD commands

        // SERVICE CRUD commands

        // SHOP CRUD commands
        public CrudViewModel()
        {
            Thread.Sleep(5000);
            string baseUrl = "http://localhost:20347/";
            if (!IsInDesignMode)
            {
                Brands = new(baseUrl, "stock/", "ListBrands");
                Models = new(baseUrl, "customer/", "ListModels");
                Services = new(baseUrl, "customer/", "ListServices");
                Shops = new(baseUrl, "customer/", "ListShops");
            }
            selectedBrand = new();

        }
    }
}
