using IL41ML_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic
{
    public class ExistingData : IExistingData
    {
        private IBrandRepository brandrepo;
        private IModelRepository modelrepo;
        private IShopRepository shoprepo;
        private IServiceRepository servicerepo;
        public ExistingData(IBrandRepository brandrepo, IModelRepository modelrepo, IShopRepository shoprepo, IServiceRepository servicerepo)
        {
            this.brandrepo = brandrepo;
            this.modelrepo = modelrepo;
            this.shoprepo = shoprepo;
            this.servicerepo = servicerepo;
        }
        public bool IsExisting(int id, string table)
        {
            switch (table)
            {
                case "brand":
                    var list = this.brandrepo.GetAll().Select(x => x.Id).ToList();
                    foreach (var item in list)
                    {
                        if (item == id)
                        {
                            return true;
                        }
                    }

                    break;
                case "model":
                    var list2 = this.modelrepo.GetAll().Select(x => x.Id).ToList();
                    foreach (var item in list2)
                    {
                        if (item == id)
                        {
                            return true;
                        }
                    }

                    break;
                case "service":
                    var list3 = this.servicerepo.GetAll().Select(x => x.Id).ToList();
                    foreach (var item in list3)
                    {
                        if (item == id)
                        {
                            return true;
                        }
                    }

                    break;
                case "shop":
                    var list4 = this.shoprepo.GetAll().Select(x => x.Id).ToList();
                    foreach (var item in list4)
                    {
                        if (item == id)
                        {
                            return true;
                        }
                    }

                    break;
                default:
                    return false;
            }

            return false;
        }
    }
}
