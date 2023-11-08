using System.IO;
using UnityEngine;

public class FileHandler
{
    private readonly string _path;

    public FileHandler(string path)
    {
        _path = path;
    }

    public MyPlayerData Load()
    {
        EnsureFileExist();
        File.ReadAllText(_path);
        var contents = File.ReadAllText(_path);
        return JsonUtility.FromJson<MyPlayerData>(contents);
    }

    public void Save(MyPlayerData data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, json);
    }

    private void EnsureFileExist()
    {
        File.WriteAllText(_path, string.Empty);
    }
}
