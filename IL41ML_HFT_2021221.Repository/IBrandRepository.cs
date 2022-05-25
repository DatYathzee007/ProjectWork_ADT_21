using IL41ML_HFT_2021221.Models;


namespace IL41ML_HFT_2021221.Repository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        void ChangeCEO(int id, string newCEO);
        void Update(Brand entity);
    }
}
