using System.Linq;

namespace IL41ML_HFT_2021221.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetOne(int id);
        IQueryable<T> GetAll();
        void Insert(T entity);
        void Remove(T entity);
        void Remove(int id);

    }
}
