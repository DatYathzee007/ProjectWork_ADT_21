using IL41ML_HFT_2021221.Data;
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
            } else { HelperMethods.MessageNotExisting(name, "Brand"); }
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
        // End of StockLogic methods


    }
}
