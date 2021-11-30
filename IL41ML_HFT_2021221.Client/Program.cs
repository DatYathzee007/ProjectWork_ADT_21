using IL41ML_HFT_2021221.Models;
using System;
using System.Threading;
using ConsoleTools;
namespace IL41ML_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Global Phone Application - IL41ML - HFT - 2021221";

            Console.WriteLine("Program Started.");
            Console.WriteLine("Waiting for EndPoint to start...");
            char[] load = "                    ".ToCharArray();
            Console.Write("\r[{0}]", new string(load));
            for (int y = 0; y < load.Length; y++)
            {
                Thread.Sleep(400);
                load[y] = '\u25A0';
                Console.Write("\r[{0}]", new string(load));
            }
            // endpoint needs more time to start, we must wait for that
            FactoryProgram fp = FactoryProgram.Init();
            //RestService rserv = new RestService("http://localhost:20347"); // from launchSettings.json
            Console.WriteLine("\nEndpoint started.");
            Thread.Sleep(500);
            Console.Clear();

            var submenu1 = new ConsoleMenu(args, 1)
            .Add("List all Brands", fp.ListBrands)
            .Add("List all Models", fp.ListModels2)
            .Add("List Brand by ID", fp.ListBrandByID)
            .Add("List Model by ID", fp.ListModelByID2)
            .Add("Insert new Model", fp.InsertModel)
            .Add("List Models by Brand", fp.ListModelsByBrand2)
            .Add("List Brand Averages", fp.ListBrandAverages)
            //.Add("List Brand Averages (Async)", fp.ListBrandAveragesAsync)
            .Add("Show the Most Expensive Brand", fp.ListMostExpensiveBrand)
            //.Add("Show the Most Expensive Brand (Async)", fp.ListMostExpensiveBrandAsync)
            .Add("Show the Least Expensive Brand", fp.ListLeastExpensiveBrand)
            //.Add("Show the Least Expensive Brand (Async)", fp.ListLeastExpensiveBrandAsync)
            .Add("Show the Most Expensive Model", fp.MostExpensiveModel)
            .Add("Show the Average price of Models", fp.AveragePriceOfModels)
            .Add("Show the cheapest Model", fp.ListCheapestModel)
            .Add("List Model Averages", fp.ListModelAverages)
            //.Add("List Model Averages (Async)", fp.ListModelAveragesAsync)
            .Add("Sub_Close", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Stock Logic";
                config.EnableWriteTitle = true;
                config.SelectedItemBackgroundColor = ConsoleColor.Red;
            });
            /*
            var submenu2 = new ConsoleMenu(args, 1)
                .Add("List all entity by Brand", fp.ListAllEntityByBrand)
                .Add("List Model by ID", fp.ListModelByID)
                .Add("List all Models", fp.ListModels)
                .Add("List Models by Brand", fp.ListModelsByBrand)
                .Add("List Models between a price range", fp.ListModelsByPriceRange)
                .Add("List Services by Brand", fp.ListServiceByBrand)
                .Add("List Service by ID", fp.ListServiceByID)
                .Add("List all Services", fp.ListServices)
                .Add("List Shop by ID", fp.ListShopByID)
                .Add("List all Shops", fp.ListShops)
                .Add("List Shops and Services accoring to a Model", fp.ListShopsAndServicesBySpecificModel)
                .Add("List Shops by Brand", fp.ListShopsByBrand)
                .Add("List Shops and Services by Localation and Brand", fp.ListShopsAndServiceINSpecificLocByBrand)
                .Add("List Shops and Services by Localation and Brand (Async)", fp.ListShopsAndServiceINSpecificLocByBrandAsync)
                .Add("Sub_Close", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.Title = "Customer Logic";
                    config.EnableWriteTitle = true;
                    config.SelectedItemBackgroundColor = ConsoleColor.Red;
                });

            var submenu3 = new ConsoleMenu(args, 1)
                .Add("Insert new Brand", fp.InsertBrand)
                .Add("Insert new Shop", fp.InsertShop)
                .Add("Insert new Service", fp.InsertService)
                .Add("Modify a Brands CEO", fp.ChangeBrandCEO)
                .Add("Modify a Models Price", fp.ChangeModelPrice)
                .Add("Modify a Services Web Address", fp.ChangeServiceWeb)
                .Add("Modify a Services Name", fp.ChangeServiceName)
                .Add("Modify a Services Phone Number", fp.ChangeServicePhone)
                .Add("Modify a Shops Name", fp.ChangeShopName)
                .Add("Modify a Shops Phone Number", fp.ChangeShopPhone)
                .Add("Remove Entity by table and by ID", fp.RemoveEntityFP)
                .Add("Sub_Close", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.Title = "Manager Logic";
                    config.EnableWriteTitle = true;
                    config.SelectedItemBackgroundColor = ConsoleColor.Red;
                });
            */
            var menu = new ConsoleMenu(args, level: 0)
              .Add("Stock operations", () => submenu1.Show())
              //.Add("Customer operations", () => submenu2.Show())
              //.Add("Manager operations", () => submenu3.Show())
              .Add("Close", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = false;
                  config.Title = "Global Phone IL41ML - Main Menu";
                  config.EnableWriteTitle = true;
                  config.SelectedItemBackgroundColor = ConsoleColor.Red;
              });

            menu.Show();



            /*var test = rserv.Get<Brand>("stock/ListBrands");
            foreach (var item in test)
            {
                Console.WriteLine(item.ToString());
            }
            var test2 = rserv.Get<Brand>(1,"stock/ListBrandByID");
            Console.WriteLine(test2.ToString());*/
            //var test3 = rserv.Get<Model>("stock/ListModels");
            //test3.ForEach(item => Console.WriteLine(item)); // LISTING MODELS

            //Model testModel = new Model() { BrandId = 1, ModelName = "TESTPhone", Color = "Red", Name = "maxProGiga", Price = 9999, Size = 64 };
            //rserv.Post<Model>(testModel, "stock"); // ADDING MODEL
            //Console.WriteLine("-----------------------------------------------------------------------------------");
            //rserv.Post<Model>(new Model() { BrandId = 1, Name = "KUKiPhone"}, "stock/InsertModel");
            //rserv.Get<Model>("stock/ListModels").ForEach(item => Console.WriteLine(item)); // LISTING MODELS

            //rserv.Delete(1, "manager");
            //var test = rserv.Get<Brand>("stock/ListBrands");
            //foreach (var item in test)
            //{
            //    Console.WriteLine(item.ToString());
            //}


        }
    }
}
