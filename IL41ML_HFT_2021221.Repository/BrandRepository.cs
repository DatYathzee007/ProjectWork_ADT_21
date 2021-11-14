using IL41ML_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Repository
{
    public class BrandRepository : ARepository<Brand>, IBrandRepository
    {
        private DbContext ctx;
        public BrandRepository(DbContext ctx) : base(ctx)
        {
            this.ctx = ctx ?? throw new System.ArgumentNullException(nameof(ctx));
        }
        public void ChangeCEO(int id, string newCEO)
        {
            var brand = this.GetOne(id);
            if (brand == null)
            {
                throw new InvalidOperationException("Brand not found");
            }

            brand.CEO = newCEO;
            this.ctx.SaveChanges();
        }

        public override Brand GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id == id);
        }

        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }
    }
}
