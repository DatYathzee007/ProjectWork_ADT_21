using IL41ML_HFT_2021221.Data;
using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Logic.DataType;
using IL41ML_HFT_2021221.Models;
using IL41ML_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Client
{
    class FactoryProgram
    {
        private static FactoryProgram fp;
        private RestService rserv;

        public FactoryProgram()
        {
            this.rserv = new RestService("http://localhost:20347");
        }
        public static FactoryProgram Init()
        {
            if (fp == null)
            {
                fp = new FactoryProgram();
            }

            return fp;
        }

        #region StockLogic methods
        public void ListBrands()
        {
            this.rserv.Get<Brand>("stock/ListBrands").ToConsole("Listing Brands: ");
        }

        public void ListModels2()
        {
            this.rserv.Get<Model>("stock/ListModels").ToConsole("Listing Models: ");
        }

        public void ListBrandByID()
        {
            bool succes = true;
            while (succes)
            {
                Console.WriteLine("Input ID for Brand:");
                if (int.TryParse(Console.ReadLine(), out int id))
                {

                    if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=brand&id={id}"))
                    {
                        HelperMethods.OneItemToConsole(this.rserv.Get<Brand>(id, "stock/ListBrandByID"), $"Brand with ID: {id}");
                        succes = false;
                    }
                    else
                    {
                        HelperMethods.MessageNotExisting(id, "Brand");
                    }
                }
                else
                {
                    Console.WriteLine("!!!Input must be integer!!!");
                }
            }
        }

        public void ListModelByID2()
        {
            bool succes = true;
            while (succes)
            {
                Console.WriteLine("Input ID for Model:");
                int id;
                succes = int.TryParse(Console.ReadLine(), out id);
                if (succes)
                {
                    if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=model&id={id}"))
                    {
                        HelperMethods.OneItemToConsole(this.rserv.Get<Model>(id, "stock/ListModelByID"), $"Model with ID: {id}");
                        succes = false;
                    }
                    else
                    {
                        HelperMethods.MessageNotExisting(id, "Model");
                    }
                }
                else
                {
                    Console.WriteLine("!!!Input must be integer!!!");
                    Console.ReadKey();
                }
            }
        }

        public void InsertModel()
        {
            string name, modelname, color;
            int brandid, size, price;
            bool succes;
            Console.WriteLine("Enter Brand ID: ");
            succes = int.TryParse(Console.ReadLine(), out brandid);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=brand&id={brandid}"))
                {
                    Console.WriteLine("Enter name: ");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter model Name: ");
                    modelname = Console.ReadLine();
                    Console.WriteLine("Enter size: ");
                    succes = int.TryParse(Console.ReadLine(), out size);
                    if (succes)
                    {
                        Console.WriteLine("Enter color: ");
                        color = Console.ReadLine();
                        Console.WriteLine("Enter price: ");
                        succes = int.TryParse(Console.ReadLine(), out price);
                        if (succes)
                        {
                            this.rserv.Post<Model>(new Model() { BrandId = brandid, ModelName = modelname, Color = color, Name = name, Price = price, Size = size }, "stock");
                            Console.WriteLine("Insertion Done! Press a key...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("!!!Input must be integer!!! Press a key...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("!!!Input must be integer!!! Press a key...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    HelperMethods.MessageNotExisting(brandid, "Brand");
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void ListModelsByBrand2()
        {
            Console.WriteLine("Enter Brand name: ");
            string name = Console.ReadLine();
            if (this.rserv.GetSingle<bool>($"existing/IsExistingString?name={name}&table=brand"))
            {
                this.rserv.Get<Model>($"stock/ListModelsByBrand?brand={name}").ToConsole($"Listing models by brand: {name}");
            }
            else { HelperMethods.MessageNotExisting(name, "Brand"); }
        }

        public void ListBrandAverages()
        {
            this.rserv.Get<NameAndDouble>("stock/ListBrandAverages").ToConsole("Listing Brand Averages: ");
        }
        /*
        public void ListBrandAveragesAsync()
        {
            this.rserv.Get<Brand>("stock/stock/ListBrandAveragesAsync").ToConsole("Listing Brand Averages: ");
        }
        */
        public void ListMostExpensiveBrand()
        {
            HelperMethods.OneItemToConsole(this.rserv.GetSingle<NameAndDouble>("stock/ListMostExpensiveBrand"), $"Most expensive brand by average: ");
        }
        /*
        public void ListMostExpensiveBrandAsync()
        {
            Console.WriteLine(this.rserv.Get<Brand>("stock/ListMostExpensiveBrandAsync").ToString() + " -- Most expensive Brand by Average.\n Press Key...");
            Console.ReadKey();
        }
        */
        public void ListLeastExpensiveBrand()
        {
            HelperMethods.OneItemToConsole(this.rserv.GetSingle<NameAndDouble>("stock/ListLeastExpensiveBrand"), $"Least expensive brand by average: ");
        }
        /*
        public void ListLeastExpensiveBrandAsync()
        {
            Console.WriteLine(this.rserv.Get<Brand>("stock/ListLeastExpensiveBrandAsync").ToString() + " -- Least expensive Brand by Average.\n Press Key...");
            Console.ReadKey();
        }
        */
        public void MostExpensiveModel()
        {
            HelperMethods.OneItemToConsole(this.rserv.GetSingle<Model>("stock/MostExpensiveModel"), $"Most expensive model: ");
        }

        public void AveragePriceOfModels()
        {
            HelperMethods.OneItemToConsole(this.rserv.GetSingle<double>("stock/AveragePriceOfModels"), $"Average price of models in HUF:");
        }

        public void ListCheapestModel()
        {
            HelperMethods.OneItemToConsole(this.rserv.GetSingle<Model>("stock/ListCheapestModel"), $"Least expensive model:");
        }

        public void ListModelAverages()
        {
            this.rserv.Get<NameAndDouble>("stock/GetModelAverage").ToConsole("Listing the Model Averages: ");
        }
        /*
        public void ListModelAveragesAsync()
        {
            this.rserv.Get<Model>("stock/GetModelAverageAsync").ToConsole("Listing the Model Averages: ");
        }
        */
        #endregion

        #region ManagerLogic methods

        public void InsertBrand()
        {
            string name, country, ceo, source;
            DateTime foundation;
            Console.WriteLine("Enter Brand name:");
            name = Console.ReadLine();
            Console.WriteLine("Enter Country:");
            country = Console.ReadLine();
            Console.WriteLine("Enter CEO of Brand:");
            ceo = Console.ReadLine();
            Console.WriteLine("Enter source localation of Brand:");
            source = Console.ReadLine();
            Console.WriteLine("Enter foundation date in a format YYYY-MM-DD :");
            foundation = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            this.rserv.Post<Brand>(new Brand() { Name = name, Country = country, CEO = ceo, Source = source, Foundation = foundation }, "manager/insertBrand");
            //this.mlgc.InsertBrand(name, country, ceo, source, foundation);
            Console.WriteLine("Insertion Done! Press a key...");
            Console.ReadKey();
        }

        public void InsertShop()
        {
            int brandid, serviceid;
            string name, country, city, phone, address;
            bool succes = true;
            while (succes)
            {
                Console.WriteLine("Enter BrandID:");
                succes = int.TryParse(Console.ReadLine(), out brandid);
                if (succes)
                {
                    succes = this.rserv.GetSingle<bool>($"existing/IsExisting?table=brand&id={brandid}");
                    if (succes)
                    {
                        Console.WriteLine("Enter ServiceID:");
                        succes = int.TryParse(Console.ReadLine(), out serviceid);
                        if (succes)
                        {
                            succes = this.rserv.GetSingle<bool>($"existing/IsExisting?table=service&id={serviceid}");
                            if (succes)
                            {
                                Console.WriteLine("Enter name: ");
                                name = Console.ReadLine();
                                Console.WriteLine("Enter country: ");
                                country = Console.ReadLine();
                                Console.WriteLine("Enter city: ");
                                city = Console.ReadLine();
                                Console.WriteLine("Enter phone number: ");
                                phone = Console.ReadLine();
                                Console.WriteLine("Enter address: ");
                                address = Console.ReadLine();
                                this.rserv.Post<Shop>(new Shop() { BrandId = brandid, ServiceId = serviceid, Name = name, Country = country, City = city, Phone = phone, Address = address }, "manager/insertShop");
                                Console.WriteLine("Insertion Done! Press a key...");
                                Console.ReadKey();
                                succes = false;
                            }
                            else
                            {
                                HelperMethods.MessageNotExisting(serviceid, "Service");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("!!!Input must be integer!!! Press a key...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        HelperMethods.MessageNotExisting(brandid, "Brand");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("!!!Input must be integer!!! Press a key...");
                    Console.ReadKey();
                }
            }
        }

        public void InsertService()
        {
            int brandid;
            string name, country, city, address, web, phone;
            bool succes = true;
            while (succes)
            {
                Console.WriteLine("Enter BrandID:");
                succes = int.TryParse(Console.ReadLine(), out brandid);
                if (succes)
                {
                    succes = this.rserv.GetSingle<bool>($"existing/IsExisting?table=brand&id={brandid}");
                    if (succes)
                    {
                        Console.WriteLine("Enter name: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter country: ");
                        country = Console.ReadLine();
                        Console.WriteLine("Enter city: ");
                        city = Console.ReadLine();
                        Console.WriteLine("Enter address: ");
                        address = Console.ReadLine();
                        Console.WriteLine("Enter WebAddres (URL)");
                        web = Console.ReadLine();
                        Console.WriteLine("Enter phone number: ");
                        phone = Console.ReadLine();
                        this.rserv.Post<Service>(new Service() { BrandId = brandid, ServiceName = name, Country = country, City = city, PhoneNr = phone, Address = address, WebPage = web }, "manager/insertService");
                        Console.WriteLine("Insertion Done! Press a key...");
                        Console.ReadKey();
                        succes = false;
                    }
                    else
                    {
                        HelperMethods.MessageNotExisting(brandid, "Brand");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("!!!Input must be integer!!! Press a key...");
                    Console.ReadKey();
                }
            }
        }
        public void ChangeBrandCEO()
        {
            int id;
            string name;
            bool succes;
            Console.WriteLine("Enter ID of Brand:");
            succes = int.TryParse(Console.ReadLine(), out id);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=brand&id={id}"))
                {
                    Console.WriteLine("Enter name of New CEO:");
                    name = Console.ReadLine();
                    this.rserv.Put<Brand>(new Brand { Id = id, CEO = name }, "manager/ChangeBrandCEO");
                    Console.WriteLine("Change Done! Press a key...");
                    Console.ReadKey();
                }
                else
                {
                    //id.MessageNotExisting("brand");
                    HelperMethods.MessageNotExisting(id, "Brand");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void ChangeModelPrice()
        {
            int id, price;
            bool succes;
            Console.WriteLine("Enter ID of Model:");
            succes = int.TryParse(Console.ReadLine(), out id);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=model&id={id}"))
                {
                    Console.WriteLine("Enter new price:");
                    succes = int.TryParse(Console.ReadLine(), out price);
                    if (succes)
                    {
                        this.rserv.Put(new Model { Id = id, Price = price }, $"manager/ChangeModelPrice");
                        //this.mlgc.ChangeModelPrice(id, price);
                        Console.WriteLine("Change Done! Press a key...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("!!!Input must be integer!!! Press a key...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    HelperMethods.MessageNotExisting(id, "Model");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void ChangeServiceWeb()
        {
            int id;
            string web;
            bool succes;
            Console.WriteLine("Enter ID of Service:");
            succes = int.TryParse(Console.ReadLine(), out id);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=service&id={id}"))
                {
                    Console.WriteLine("Enter new URL:");
                    web = Console.ReadLine();
                    this.rserv.Put(new Service { Id = id, WebPage = web }, $"manager/ChangeServiceWeb");
                    //this.mlgc.ChangeServiceWeb(id, web);
                    Console.WriteLine("Change Done! Press a key...");
                    Console.ReadKey();
                }
                else
                {

                    HelperMethods.MessageNotExisting(id, "Service");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void ChangeServiceName()
        {
            int id;
            string name;
            bool succes;
            Console.WriteLine("Enter ID of Service:");
            succes = int.TryParse(Console.ReadLine(), out id);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=service&id={id}"))
                {
                    Console.WriteLine("Enter new name:");
                    name = Console.ReadLine();
                    this.rserv.Put(new Service { Id = id, ServiceName = name }, $"manager/ChangeServiceName");
                    //this.mlgc.ChangeServiceName(id, name);
                    Console.WriteLine("Change Done! Press a key...");
                    Console.ReadKey();
                }
                else
                {
                    HelperMethods.MessageNotExisting(id, "Service");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void ChangeServicePhone()
        {
            int id;
            string phone;
            bool succes;
            Console.WriteLine("Enter ID of Service:");
            succes = int.TryParse(Console.ReadLine(), out id);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=service&id={id}"))
                {
                    Console.WriteLine("Enter new phone number:");
                    phone = Console.ReadLine();
                    this.rserv.Put(new Service { Id = id, PhoneNr = phone }, $"manager/ChangeServicePhone");
                    //this.mlgc.ChangeServicePhone(id, phone);
                    Console.WriteLine("Change Done! Press a key...");
                    Console.ReadKey();
                }
                else
                {
                    HelperMethods.MessageNotExisting(id, "Service");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void ChangeShopName()
        {
            int id;
            string name;
            bool succes;
            Console.WriteLine("Enter ID of Shop:");
            succes = int.TryParse(Console.ReadLine(), out id);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=shop&id={id}"))
                {
                    Console.WriteLine("Enter new name:");
                    name = Console.ReadLine();
                    this.rserv.Put(new Shop { Id = id, Name = name }, $"manager/ChangeShopName");
                    //this.mlgc.ChangeShopName(id, name);
                    Console.WriteLine("Change Done! Press a key...");
                    Console.ReadKey();
                }
                else
                {
                    HelperMethods.MessageNotExisting(id, "Shop");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void ChangeShopPhone()
        {
            int id;
            string phone;
            bool succes;
            Console.WriteLine("Enter ID of Shop:");
            succes = int.TryParse(Console.ReadLine(), out id);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=service&id={id}"))
                {
                    Console.WriteLine("Enter new phone number:");
                    phone = Console.ReadLine();
                    this.rserv.Put(new Shop { Id = id, Phone = phone }, $"manager/ChangeShopPhone");
                    //this.mlgc.ChangeShopName(id, phone);
                    Console.WriteLine("Change Done! Press a key...");
                    Console.ReadKey();
                }
                else
                {
                    HelperMethods.MessageNotExisting(id, "Shop");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void RemoveEntityFP()
        {
            Console.WriteLine("Select table: Brand = 1 , Model = 2, Shop = 3, Service = 4");
            string tableDecider;
            tableDecider = Console.ReadLine();
            int id;
            bool succes;
            string entityName = "brand";
            switch (tableDecider)
            {
                case "1":
                    entityName = "brand";
                    break;
                case "2":
                    entityName = "model";
                    break;
                case "3":
                    entityName = "shop";
                    break;
                case "4":
                    entityName = "service";
                    break;
                default:
                    throw new ArgumentException("Not valid tableDecider int.");
            }

            Console.WriteLine("Enter ID of " + entityName + " to be Removed: ");
            succes = int.TryParse(Console.ReadLine(), out id);
            if (succes)
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?table={entityName}&id={id}"))
                {
                    this.rserv.Delete(id, $"manager/{entityName}");
                    //this.mlgc.RemoveEntity(entityName, id);
                    Console.WriteLine("Remove Done! Press a key...");
                    Console.ReadKey();
                }
                else
                {
                    HelperMethods.MessageNotExisting(id, entityName);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        #endregion

        #region Customerlogic methods
        public void ListAllEntityByBrand()
        {
            Console.WriteLine("Enter Brand name: ");
            string name = Console.ReadLine();
            if (this.rserv.GetSingle<bool>($"existing/IsExistingString?name={name}&table=brand"))
            {
                this.rserv.Get<string>($"customer/ListAllEntityByBrand?brand={name}").ToConsole($"Listing Entities by brand: {name}");
            }
            else { HelperMethods.MessageNotExisting(name, "Brand"); }
        }
        public void ListModelByID()
        {
            bool succes = true;
            while (succes)
            {
                Console.WriteLine("Input ID for Model:");
                int id;
                succes = int.TryParse(Console.ReadLine(), out id);
                if (succes)
                {
                    if (this.rserv.GetSingle<bool>($"existing/IsExisting?id={id}&table=model"))
                    {
                        HelperMethods.OneItemToConsole(this.rserv.Get<Model>(id, "customer/ListModelByID"), $"Model with ID: {id}");
                        succes = false;
                    }
                    else
                    {
                        HelperMethods.MessageNotExisting(id, "Model");
                    }
                }
                else
                {
                    Console.WriteLine("!!!Input must be integer!!!");
                    Console.ReadKey();
                }
            }
        }
        public void ListModels()
        {
            this.rserv.Get<Model>("customer/ListModels").ToConsole("Listing Models: ");
        }
        public void ListModelsByBrand()
        {
            Console.WriteLine("Enter Brand name: ");
            string name = Console.ReadLine();
            if (this.rserv.GetSingle<bool>($"existing/IsExistingString?name={name}&table=brand"))
            {
                this.rserv.Get<Model>($"customer/ListModelsByBrand?brand={name}").ToConsole($"Listing models by brand: {name}");
            }
            else { HelperMethods.MessageNotExisting(name, "Brand"); }
        }
        public void ListModelsByPriceRange()
        {
            bool succes;
            int lowerBound, upperBound;
            Console.WriteLine("Enter LowerBound:");
            succes = int.TryParse(Console.ReadLine(), out lowerBound);
            if (succes)
            {
                Console.WriteLine("Enter UpperBound: ");
                succes = int.TryParse(Console.ReadLine(), out upperBound);
                if (succes)
                {
                    this.rserv.Get<string>($"customer/ListModelsByPriceRange?lb={lowerBound}&ub={upperBound}").ToConsole($"Listing Models between {lowerBound} HUF and {upperBound} HUF: ");
                }
                else
                {
                    Console.WriteLine("!!!Input must be integer!!!");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!!");
                Console.ReadKey();
            }
        }
        public void ListServiceByBrand()
        {
            Console.WriteLine("Enter Brand name: ");
            string name = Console.ReadLine();
            if (this.rserv.GetSingle<bool>($"existing/IsExistingString?name={name}&table=brand"))
            {
                this.rserv.Get<Service>($"customer/ListServiceByBrand?brand={name}").ToConsole($"Listing services by brand: {name}");
            }
            else { HelperMethods.MessageNotExisting(name, "Brand"); }
        }
        public void ListServiceByID()
        {
            bool succes = true;
            while (succes)
            {
                Console.WriteLine("Input ID for Service:");
                int id;
                succes = int.TryParse(Console.ReadLine(), out id);
                if (succes)
                {
                    if (this.rserv.GetSingle<bool>($"existing/IsExisting?table=service&id={id}"))
                    {
                        HelperMethods.OneItemToConsole(this.rserv.Get<Service>(id, "customer/ListServiceByID"), $"Model with ID: {id}");
                        succes = false;
                    }
                    else
                    {
                        HelperMethods.MessageNotExisting(id, "Service");
                    }
                }
                else
                {
                    Console.WriteLine("!!!Input must be integer!!!");
                    Console.ReadKey();
                }
            }
        }
        public void ListServices()
        {
            this.rserv.Get<Service>("customer/ListServices").ToConsole("Listing Services: ");
        }
        public void ListShopByID()
        {
            bool succes = true;
            while (succes)
            {
                Console.WriteLine("Input ID for Shop:");
                int id;
                succes = int.TryParse(Console.ReadLine(), out id);
                if (succes)
                {
                    if (this.rserv.GetSingle<bool>($"existing/IsExisting?id={id}&table=shop"))
                    {
                        HelperMethods.OneItemToConsole(this.rserv.Get<Shop>(id, "customer/ListShopByID"), $"Shop with ID: {id}");
                        Console.ReadKey();
                        succes = false;
                    }
                    else
                    {
                        HelperMethods.MessageNotExisting(id, "Shop");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("!!!Input must be integer!!!");
                }
            }
        }
        public void ListShops()
        {
            this.rserv.Get<Shop>("customer/ListShops").ToConsole("Listing Shops: ");

        }
        public void ListShopsAndServicesBySpecificModel()
        {
            this.rserv.Get<Model>("customer/ListModels").ToConsole("Listing Models: ");
            Console.WriteLine("Enter ID of Model: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (this.rserv.GetSingle<bool>($"existing/IsExisting?id={id}&table=model"))
                {
                    this.rserv.Get<string>($"customer/ListShopsAndServicesBySpecificModel/{id}").ToConsole("Listing Shops and Services:");
                }
                else
                {
                    HelperMethods.MessageNotExisting(id, "Model");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }
        public void ListShopsByBrand()
        {
            Console.WriteLine("Enter Brand name: ");
            string name = Console.ReadLine();
            if (this.rserv.GetSingle<bool>($"existing/IsExistingString?name={name}&table=brand"))
            {
                this.rserv.Get<Shop>($"customer/ListShopsByBrand?brand={name}").ToConsole($"Listing shops by brand: {name}");
            }
            else { HelperMethods.MessageNotExisting(name, "Brand"); }
        }
        public void ListShopsAndServiceINSpecificLocByBrand()
        {
            string name;
            string shoplocation;
            ConsoleKey asd;
            do
            {
                Console.WriteLine("\nEnter Brand name (SAMSUNG): ");
                name = Console.ReadLine();
                if (this.rserv.GetSingle<bool>($"existing/IsExistingString?name={name}&table=brand"))
                {
                    Console.WriteLine("\nEnter Location (Szentmihalyi ut): ");
                    shoplocation = Console.ReadLine();
                    Console.WriteLine($"CONFIRM choice, Brand:{name}, Address: {shoplocation}, \nPress Y/N");
                    asd = Console.ReadKey().Key;
                    if (asd == ConsoleKey.Y)
                    {
                        this.rserv.Get<string>($"customer/ListShopsAndServiceINSpecificLocByBrand?brand={name}&shoplocation={shoplocation}").ToConsole($"Listing shops and services by brand: {name} and localation: {shoplocation}");
                    }
                }
                else
                {
                    HelperMethods.MessageNotExisting(name, "Brand");
                    Console.WriteLine("Start all over? Y/N");
                    asd = Console.ReadKey().Key;
                    if (asd == ConsoleKey.Y)
                    {
                        asd = ConsoleKey.N;
                    }
                    else asd = ConsoleKey.Y;
                }
            } while (asd == ConsoleKey.N);

        }

        #endregion


    }
}
