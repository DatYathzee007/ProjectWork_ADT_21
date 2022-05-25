using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IL41ML_HFT_2021221.Repository
{
    public abstract class ARepository<T> : IRepository<T> where T : class
    {
        private DbContext ctx;
        public ARepository(DbContext ctx)
        {
            this.ctx = ctx ?? throw new System.ArgumentNullException(nameof(ctx));
        }
        public IQueryable<T> GetAll()
        {
            return this.ctx.Set<T>();
        }

        public abstract T GetOne(int id);

        public void Insert(T entity)
        {
            this.ctx.Set<T>().Add(entity);
            this.ctx.SaveChanges();
        }

        public void Remove(T entity)
        {
            this.ctx.Set<T>().Remove(entity);
            this.ctx.SaveChanges();
        }

        public abstract void Remove(int id);


    }
}
