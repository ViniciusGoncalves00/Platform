public interface IDataPersistence
{
    void LoadData(MyPlayerData data);
    void SaveData(ref MyPlayerData data);
}