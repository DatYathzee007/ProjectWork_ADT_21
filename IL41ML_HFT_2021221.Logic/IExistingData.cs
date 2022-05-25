namespace IL41ML_HFT_2021221.Logic
{
    public interface IExistingData
    {
        bool IsExisting(int id, string table);
        bool IsExisting(string name, string table);
    }
}
