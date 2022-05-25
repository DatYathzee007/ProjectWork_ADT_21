using IL41ML_HFT_2021221.Models;

namespace IL41ML_HFT_2021221.Repository
{
    public interface IShopRepository : IRepository<Shop>
    {
        void ChangePhoneNumber(int id, string newPhoneNumber);
        void ChangeName(int id, string newName);
        void Update(Shop entity);
    }
}
