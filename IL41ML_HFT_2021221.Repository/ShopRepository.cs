using IL41ML_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IL41ML_HFT_2021221.Repository
{
    public class ShopRepository : ARepository<Shop>, IShopRepository
    {
        private DbContext ctx;
        public ShopRepository(DbContext ctx)
            : base(ctx)
        {
            this.ctx = ctx ?? throw new System.ArgumentNullException(nameof(ctx));
        }
        public void ChangeName(int id, string newName)
        {
            var shop = this.GetOne(id);
            if (shop == null)
            {
                throw new InvalidOperationException("Shop not found");
            }

            shop.Name = newName;
            this.ctx.SaveChanges();
        }
        public void ChangePhoneNumber(int id, string newPhoneNumber)
        {
            var shop = this.GetOne(id);
            if (shop == null)
            {
                throw new InvalidOperationException("Shop not found");
            }

            shop.Phone = newPhoneNumber;
            this.ctx.SaveChanges();
        }
        public override Shop GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }

        public void Update(Shop entity)
        {
            var oldShop = this.GetOne(entity.Id);
            if (oldShop is null)
            {
                throw new InvalidOperationException($"Model is notexists with id: {entity.Id}");
            }
            foreach (var prop in oldShop.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(oldShop, prop.GetValue(entity));
                }
            }
            this.ctx.SaveChanges();
        }
    }
}
