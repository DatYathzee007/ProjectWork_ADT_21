namespace IL41ML_HFT_2021221.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using IL41ML_HFT_2021221.Models;
    using OfficeOpenXml;

    /// <summary>
    /// A class that called via <see cref="CustomDbContext"/>,
    /// main task is to import data from *.xlsx file to List entities, that will be loaded into the database.
    /// </summary>
    public class XlsxLoader
    {
        private static List<Model> myList = new List<Model>();
        private static List<Service> myList2 = new List<Service>();
        private static List<Shop> myList3 = new List<Shop>();

        /// <summary>
        /// Initializes a new instance of the <see cref="XlsxLoader"/> class.
        /// </summary>
        /// <param name="connString">The string that contains the path of the Model data source file.</param>
        /// <param name="connString2">The string that contains the path of the Service data source file.</param>
        /// <param name="connString3">The string that contains the path of the Shop data source file.</param>
        public XlsxLoader(string connString, string connString2, string connString3)
        {
            XlsxGetDataModel(connString);
            XlsxGetDataService(connString2);
            XlsxGetDataShop(connString3);
            ReturnDataModel();
            ReturnDataService();
            ReturnDataShop();
        }

        /// <summary>
        /// Passes the loaded <see cref="myList"/>.
        /// </summary>
        /// <returns><see cref="myList"/> with data.</returns>
        public static List<Model> ReturnDataModel()
        {
            return myList;
        }

        /// <summary>
        /// Passes the loaded <see cref="myList2"/>.
        /// </summary>
        /// <returns><see cref="myList2"/> with data.</returns>
        public static List<Service> ReturnDataService()
        {
            return myList2;
        }

        /// <summary>
        /// Passes the loaded <see cref="myList3"/>.
        /// </summary>
        /// <returns><see cref="myList3"/> with data.</returns>
        public static List<Shop> ReturnDataShop()
        {
            return myList3;
        }

        /// <summary>
        /// Method that uses <see cref="ExcelPackage"/> to load data from *.xlsx into <see cref="myList"/>.
        /// </summary>
        /// <param name="connString">Contains the Absolute path to the file.</param>
        private static void XlsxGetDataModel(string connString)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(connString)))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First();
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;

                int idCounter = 1;
                string brandidstring = string.Empty;
                string nameSB = string.Empty;
                string modelNameSB;
                int sizeSB = 0;
                string colorSB = string.Empty;
                string priceSB = string.Empty;
                int price = 0;
                string[] sizes = { "32GB", "64GB", "128GB", "256GB", "512GB", "1TB" };

                for (int rowNum = 1; rowNum <= totalRows; rowNum++)
                {
                    sizeSB = 0;
                    var sb = new StringBuilder();
                    var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].
                    Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                    sb.AppendLine(string.Join(",", row));
                    var rowString = sb.ToString();
                    int firstComma = rowString.IndexOf(",", System.StringComparison.OrdinalIgnoreCase);
                    var firstSpaceIndex = rowString.IndexOf(" ", System.StringComparison.OrdinalIgnoreCase);
                    int leftChar = rowString.Length - firstComma;
                    priceSB = rowString.Substring(firstComma, leftChar);
                    price = int.Parse(priceSB.Replace(",", string.Empty, System.StringComparison.OrdinalIgnoreCase), System.Globalization.NumberFormatInfo.InvariantInfo);
                    brandidstring = rowString.Substring(0, firstSpaceIndex);
                    int cnter = rowString.Length - priceSB.Length;
                    rowString = rowString.Substring(0, cnter);
                    firstSpaceIndex = firstSpaceIndex + 1;
                    rowString = rowString.Substring(firstSpaceIndex, rowString.Length - firstSpaceIndex);
                    nameSB = rowString.Substring(0, rowString.IndexOf(" ", System.StringComparison.OrdinalIgnoreCase));
                    var secondSpaceIndex = rowString.IndexOf(" ", System.StringComparison.OrdinalIgnoreCase) + 1;
                    rowString = rowString.Substring(secondSpaceIndex, rowString.Length - secondSpaceIndex);
                    foreach (string item in sizes)
                    {
                        if (rowString.Contains(item, System.StringComparison.OrdinalIgnoreCase))
                        {
                            rowString = rowString.Replace(item, string.Empty, System.StringComparison.OrdinalIgnoreCase);
                            sizeSB = int.Parse(item.Replace("GB", string.Empty, System.StringComparison.OrdinalIgnoreCase), System.Globalization.NumberFormatInfo.InvariantInfo);
                            break;
                        }
                    }

                    string[] colors = { "Gold-Pink Sand", "Gray-Black", "Silver-White", "Gray-Anthracite Black", "Silver-Pure Platinum Black", "Blue-Blue", "Silver-Black", "Red-Red", "Sky Blue", "Aurora Blue", "Breathing Crystal", "Twilight", "Peacock Blue", "Misty Lavender", "Mystic Blue", "Pink", "Leather Brown", "Violet", "Mint", "Aura Glow", "Bronze", "Rose Gold", "Gold", "Gray", "Silver", "Black", "Red", "White", "Green", "Purple", "Yellow", "Coral", "Blue" };
                    foreach (string item in colors)
                    {
                        if (rowString.Contains(item, System.StringComparison.OrdinalIgnoreCase))
                        {
                            rowString = rowString.Replace(item, string.Empty, System.StringComparison.OrdinalIgnoreCase);
                            colorSB = item.ToString();
                            break;
                        }
                        else
                        {
                            colorSB = string.Empty;
                        }
                    }

                    rowString = rowString.Replace("  ", " ", System.StringComparison.OrdinalIgnoreCase);
                    modelNameSB = rowString;
                    switch (brandidstring.Replace(" ", string.Empty, System.StringComparison.OrdinalIgnoreCase).ToUpper(System.Globalization.CultureInfo.InvariantCulture))
                    {
                        case "APPLE":
                            myList.Add(new Model() { Id = rowNum, BrandId = 1, Name = nameSB, ModelName = modelNameSB, Size = sizeSB, Color = colorSB, Price = price });
                            break;
                        case "SAMSUNG":
                            myList.Add(new Model() { Id = rowNum, BrandId = 2, Name = nameSB, ModelName = modelNameSB, Size = sizeSB, Color = colorSB, Price = price });
                            break;
                        case "HUAWEI":
                            myList.Add(new Model() { Id = rowNum, BrandId = 3, Name = nameSB, ModelName = modelNameSB, Size = sizeSB, Color = colorSB, Price = price });
                            break;
                        case "XIAOMI":
                            myList.Add(new Model() { Id = rowNum, BrandId = 4, Name = nameSB, ModelName = modelNameSB, Size = sizeSB, Color = colorSB, Price = price });
                            break;
                        default:
                            throw new KeyNotFoundException(nameof(brandidstring));
                    }

                    idCounter++;
                }
            }
        }

        /// <summary>
        /// Method that uses <see cref="ExcelPackage"/> to load data from *.xlsx into <see cref="myList2"/>.
        /// </summary>
        /// <param name="connString2">Contains the Absolute path to the file.</param>
        private static void XlsxGetDataService(string connString2)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(connString2)))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First();
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;

                int brandId = 0;
                string serviceName = string.Empty;
                string country = string.Empty;
                string city = string.Empty;
                string address = string.Empty;
                string webPage = string.Empty;
                string phoneNumber = string.Empty;

                for (int rowNum = 1; rowNum <= totalRows; rowNum++)
                {
                    for (int colNum = 1; colNum <= totalColumns; colNum++)
                    {
                        var cell = myWorksheet.Cells[rowNum, colNum];
                        switch (colNum)
                        {
                            case 1:
                                switch (cell.Value.ToString())
                                {
                                    case "Apple":
                                        brandId = 1;
                                        break;
                                    case "Samsung":
                                        brandId = 2;
                                        break;
                                    case "Huawei":
                                        brandId = 3;
                                        break;
                                    case "Xiaomi":
                                        brandId = 4;
                                        break;
                                    default:
                                        throw new KeyNotFoundException(cell.Value.ToString() + " not matching!!!");
                                }

                                break;
                            case 2:
                                serviceName = cell.Value.ToString();
                                break;
                            case 3:
                                country = cell.Value.ToString();
                                break;
                            case 4:
                                city = cell.Value.ToString();
                                break;
                            case 5:
                                address = cell.Value.ToString();
                                break;
                            case 6:
                                if (cell.Value == null)
                                {
                                    webPage = "No URL found.";
                                }
                                else
                                {
                                    webPage = cell.Value.ToString();
                                }

                                break;
                            case 7:
                                phoneNumber = cell.Value.ToString();
                                break;
                            default:
                                throw new InvalidDataException(nameof(colNum) + "is invalid!!!");
                        }
                    }

                    myList2.Add(new Service() { Id = rowNum, BrandId = brandId, ServiceName = serviceName, Country = country, City = city, Address = address, WebPage = webPage, PhoneNr = phoneNumber });
                }
            }
        }

        /// <summary>
        /// Method that uses <see cref="ExcelPackage"/> to load data from *.xlsx into <see cref="myList3"/>.
        /// </summary>
        /// <param name="connString3">Contains the Absolute path to the file.</param>
        private static void XlsxGetDataShop(string connString3)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(connString3)))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First();
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;
                int brandId = 0;
                int serviceId = 0;
                string name = string.Empty;
                string country = string.Empty;
                string city = string.Empty;
                string phoneNumber = string.Empty;
                string address = string.Empty;

                for (int rowNum = 1; rowNum < totalRows; rowNum++)
                {
                    for (int colNum = 1; colNum < totalColumns + 1; colNum++)
                    {
                        var cell = myWorksheet.Cells[rowNum, colNum];
                        switch (colNum)
                        {
                            case 1:
                                switch (cell.Value.ToString())
                                {
                                    case "Apple":
                                        brandId = 1;
                                        break;
                                    case "Samsung":
                                        brandId = 2;
                                        break;
                                    case "Huawei":
                                        brandId = 3;
                                        break;
                                    case "Xiaomi":
                                        brandId = 4;
                                        break;
                                    default:
                                        throw new KeyNotFoundException(cell.Value.ToString() + " not matching!!!");
                                }

                                break;
                            case 2:
                                serviceId = int.Parse(cell.Value.ToString(), System.Globalization.NumberFormatInfo.InvariantInfo);
                                break;
                            case 3:
                                name = cell.Value.ToString();
                                break;
                            case 4:
                                country = cell.Value.ToString();
                                break;
                            case 5:
                                city = cell.Value.ToString();
                                break;
                            case 6:
                                phoneNumber = cell.Value.ToString();
                                break;
                            case 7:
                                address = cell.Value.ToString();
                                break;
                            default:
                                throw new KeyNotFoundException(nameof(colNum) + " not matching!!!");
                        }
                    }

                    myList3.Add(new Shop() { Id = rowNum, BrandId = brandId, ServiceId = serviceId, Name = name, Country = country, City = city, Phone = phoneNumber, Address = address });
                }
            }
        }
    }
}
