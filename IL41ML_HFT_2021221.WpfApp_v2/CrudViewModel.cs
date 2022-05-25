using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IL41ML_HFT_2021221.Models;
using System.ComponentModel;
using System.Threading;
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

        //private RestCollection<string> result;
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
            inputBrand = inputBrand.Replace(" ", "");
            if (IsDigitsOnly(inputBrand))
            {
                Brands.GetOne($"ListBrandByID/{inputBrand}", int.Parse(inputBrand));
            }
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
                var brand = selectedBrand;
                Brands.Update(brand);
            }

        }
        [ICommand]
        private void CreateBrand()
        {

            if (selectedBrand != null)
            {
                Brand newBrand = new() { Name = selectedBrand.Name, CEO = selectedBrand.CEO, Country = selectedBrand.Country, Foundation = selectedBrand.Foundation, Source = selectedBrand.Source };
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
        }
        // MODEL CRUD commands
        [ICommand]
        private void GetOneModel()
        {
            inputModel = inputModel.Replace(" ", "");
            if (IsDigitsOnly(inputModel))
            {
                Models.GetOne($"ListModelByID/{inputModel}", int.Parse(inputModel));
            }
        }
        [ICommand]
        private void GetAllModel()
        {
            this.Models.GetAll();
        }
        [ICommand]
        private void UpdateModel()
        {

            if (selectedModel != null)
            {
                var model = selectedModel;
                model.BrandId = selectedModel.Brand.Id;
                Models.Update(model);
            }

        }
        [ICommand]
        private void CreateModel()
        {

            if (selectedModel != null)
            {
                Model newModel = new()
                {
                    BrandId = selectedModel.Brand.Id,
                    Name = selectedModel.Name,
                    ModelName = selectedModel.ModelName,
                    Size = selectedModel.Size,
                    Color = selectedModel.Color,
                    Price = selectedModel.Price
                };
                Models.Add(newModel);
            }
        }
        [ICommand]
        private void DeleteModel()
        {
            if (selectedModel != null)
            {
                Models.Delete(selectedModel.Id);
            }
        }
        // SERVICE CRUD commands
        [ICommand]
        private void GetOneService()
        {
            inputService = inputService.Replace(" ", "");
            if (IsDigitsOnly(inputService))
            {
                Services.GetOne($"ListServiceByID/{inputService}", int.Parse(inputService));
            }
        }
        [ICommand]
        private void GetAllService()
        {
            this.Services.GetAll();
        }
        [ICommand]
        private void UpdateService()
        {

            if (selectedService != null)
            {
                var service = selectedService;
                service.BrandId = selectedService.Brand.Id;
                Services.Update(service);
            }

        }
        [ICommand]
        private void CreateService()
        {

            if (selectedService != null)
            {
                Service newService = new()
                {
                    BrandId = selectedService.Brand.Id,
                    ServiceName = selectedService.ServiceName,
                    Country = selectedService.Country,
                    City = selectedService.City,
                    Address = selectedService.Address,
                    WebPage = selectedService.WebPage,
                    PhoneNr = selectedService.PhoneNr
                };
                Services.Add(newService);
            }
        }
        [ICommand]
        private void DeleteService()
        {
            if (selectedService != null)
            {
                Services.Delete(selectedService.Id);
            }
        }
        // SHOP CRUD commands
        [ICommand]
        private void GetOneShop()
        {
            inputShop = inputShop.Replace(" ", "");
            if (IsDigitsOnly(inputShop))
            {
                Shops.GetOne($"ListShopByID/{inputShop}", int.Parse(inputShop));
            }
        }
        [ICommand]
        private void GetAllShop()
        {
            this.Shops.GetAll();
        }
        [ICommand]
        private void UpdateShop()
        {

            if (selectedShop != null)
            {
                var shop = selectedShop;
                shop.BrandId = selectedShop.Brand.Id;
                shop.ServiceId = selectedShop.Service.Id;
                Shops.Update(shop);
            }

        }
        [ICommand]
        private void CreateShop()
        {

            if (selectedShop != null)
            {
                Shop newShop = new()
                {
                    BrandId = selectedShop.Brand.Id,
                    ServiceId = selectedShop.Service.Id,
                    Name = selectedShop.Name,
                    Country = selectedShop.Country,
                    City = selectedShop.City,
                    Phone = selectedShop.Phone,
                    Address = selectedShop.Address

                };
                Shops.Add(newShop);
            }
        }
        [ICommand]
        private void DeleteShop()
        {
            if (selectedShop != null)
            {
                Shops.Delete(selectedShop.Id);
            }
        }
        //NON CRUD FUNCs
        [ICommand]
        private void GetNonCRUDone()
        {
            // GET: customer/ListAllEntityByBrand?brand={name}
            //string name = "Apple";
            var result = new RestCollection<string>("http://localhost:20347/", "customer/", $"ListAllEntityByBrand?brand=Apple");
            new NonCrudWindow(result).ShowDialog();

        }
        [ICommand]
        private void GetNonCRUDtwo()
        {
            //stock/ListBrandAverages
            //var result2 = new RestCollection<NameAndDouble>("http://localhost:20347/", "stock/", $"ListBrandAverages");

            var result = new RestCollection<string>("http://localhost:20347/", "customer/", $"ListAll");
            new NonCrudWindow(result).ShowDialog();

        }
        [ICommand]
        private void GetNonCRUDthree()
        {
            // GET: customer/ListShopsAndServicesBySpecificModel/{id}
            int id = 1;
            var result = new RestCollection<string>("http://localhost:20347/", "customer/", $"ListShopsAndServicesBySpecificModel/{id}");
            new NonCrudWindow(result).ShowDialog();

        }
        public CrudViewModel()
        {
            Thread.Sleep(5000);
            string baseUrl = "http://localhost:20347/";
            if (!IsInDesignMode)
            {
                Brands = new(baseUrl, "stock/", "ListBrands", "hub");
                Models = new(baseUrl, "customer/", "ListModels");
                Services = new(baseUrl, "customer/", "ListServices");
                Shops = new(baseUrl, "customer/", "ListShops");
            }
            selectedBrand = new();
            selectedModel = new();
            selectedService = new();
            selectedShop = new();
            //result = new RestCollection<string>("http://localhost:20347/", "customer/", $"ListAllEntityByBrand?brand=Apple");

        }
        public bool IsDigitsOnly(string str)
        {
            if (str != null && str != "")
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
