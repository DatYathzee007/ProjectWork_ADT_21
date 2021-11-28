﻿using IL41ML_HFT_2021221.Data;
using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Logic.DataType;
using IL41ML_HFT_2021221.Models;
using IL41ML_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Client
{
    class FactoryProgram
    {
        private static FactoryProgram fp;
        private RestService rserv;

        /*
        private ExistingData exist;
        private CustomDbContext ctx;
        private BrandRepository brepo;
        private ModelRepository mrepo;
        private ServiceRepository srepo;
        private ShopRepository shrepo;
        */
        public FactoryProgram()
        {
            /*
            this.ctx = new CustomDbContext();
            this.brepo = new BrandRepository(ctx);
            this.mrepo = new ModelRepository(ctx);
            this.srepo = new ServiceRepository(ctx);
            this.shrepo = new ShopRepository(ctx);
            this.exist = new ExistingData(this.brepo, this.mrepo, this.shrepo, this.srepo);
            */

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
        private static void OneItemToConsole<T>(T input)
        {
            if (input != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\tBEGIN: ");
                Console.ResetColor();
                Console.WriteLine(input.ToString());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine('\t' + " END.\t(Press a key)");
                Console.ResetColor();
                Console.ReadKey();
            }
            else
            {
                throw new ArgumentNullException(nameof(input));
            }
        }
        private static void MessageNotExisting(int id, string table)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Entity is not Existing in table: {0} with ID: {1}", table, id);
            Console.ResetColor();
        }

        // StockLogic methods
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

                    if (true/*this.rserv.GetSingle<bool>($"existing/IsExisting/table=brand&id={id}")/*this.exist.IsExisting(id, "brand")*/)
                    {
                        OneItemToConsole(this.rserv.Get<Brand>(id, "stock/ListBrandByID"));
                        succes = false;
                    }
                    else
                    {
                        MessageNotExisting(id, "Brand");
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
                    if (this.rserv.GetSingle<bool>("existing/IsExisting"))
                    {
                        Console.WriteLine(this.rserv.Get<Model>(id, "stock/ListModelByID"));
                        Console.ReadKey();
                        succes = false;
                    }
                    else
                    {
                        MessageNotExisting(id, "Model");
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
                if (this.rserv.GetSingle<bool>("existing/IsExisting"))
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
                    MessageNotExisting(brandid, "Brand");
                }
            }
            else
            {
                Console.WriteLine("!!!Input must be integer!!! Press a key...");
                Console.ReadKey();
            }
        }

        public void ListModelsByBrand2() //?
        {
            Console.WriteLine("Enter brand id: ");
            int id = int.Parse(Console.ReadLine());
            foreach (var item in this.rserv.Get<Brand>("stock/ListBrands"))
            {
                if (item.Id == id)
                {
                    this.rserv.Get<Model>(id, "stock/ListModelsByBrand");
                }
                else { MessageNotExisting(id, "Brand"); }
            }
        }

        public void ListBrandAverages()
        {
            this.rserv.Get<Brand>("stock/stock/ListBrandAverages").ToConsole("Listing Brand Averages: ");
        }
        /*
        public void ListBrandAveragesAsync()
        {
            this.rserv.Get<Brand>("stock/stock/ListBrandAveragesAsync").ToConsole("Listing Brand Averages: ");
        }
        */
        public void ListMostExpensiveBrand()
        {
            Console.WriteLine(this.rserv.GetSingle<NameAndDouble>("stock/ListMostExpensiveBrand").ToString() + " -- Most expensive Brand by Average.\n Press Key...");
            Console.ReadKey();
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
            Console.WriteLine(this.rserv.GetSingle<NameAndDouble>("stock/ListLeastExpensiveBrand").ToString() + " -- Least expensive Brand by Average.\n Press Key...");
            Console.ReadKey();
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
            Console.WriteLine(this.rserv.GetSingle<Model>("stock/MostExpensiveModel").ToString() + " -- Highest price Model.\n Press Key...");
            Console.ReadKey();
        }

        public void AveragePriceOfModels()
        {
            Console.WriteLine(this.rserv.GetSingle<double>("stock/AveragePriceOfModels") + "HUF -- Average Model price.\n Press Key...");
            Console.ReadKey();
        }

        public void ListCheapestModel()
        {
            Console.WriteLine(this.rserv.GetSingle<Model>("stock/ListCheapestModel").ToString() + " -- Cheapest price Model.\n Press Key...");
            Console.ReadKey();
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
        // End of StockLogic methods
    }
}
