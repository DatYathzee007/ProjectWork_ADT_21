using IL41ML_HFT_2021221.Models;

namespace IL41ML_HFT_2021221.Repository
{
    public interface IModelRepository : IRepository<Model>
    {
        void ChangeModelPrice(int id, int newPrice);
        void Update(Model entity);
    }
}
