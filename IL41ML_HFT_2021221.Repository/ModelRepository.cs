using IL41ML_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IL41ML_HFT_2021221.Repository
{
    public class ModelRepository : ARepository<Model>, IModelRepository
    {
        private DbContext ctx;
        public ModelRepository(DbContext ctx)
            : base(ctx)
        {
            this.ctx = ctx ?? throw new System.ArgumentNullException(nameof(ctx));
        }
        public void ChangeModelPrice(int id, int newPrice)
        {
            var model = this.GetOne(id);
            if (model == null)
            {
                throw new InvalidOperationException("Model not found");
            }

            model.Price = newPrice;
            this.ctx.SaveChanges();
        }
        public override Model GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }
        public void Update(Model entity)
        {
            var oldModel = this.GetOne(entity.Id);
            if (oldModel is null)
            {
                throw new InvalidOperationException($"Model is notexists with id: {entity.Id}");
            }
            foreach (var prop in oldModel.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(oldModel, prop.GetValue(entity));
                }
            }
            this.ctx.SaveChanges();
        }
    }
}
