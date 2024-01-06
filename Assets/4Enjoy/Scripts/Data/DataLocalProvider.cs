using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DataLocalProvider : IDataProvider
{
    private readonly IPersistentData _persistentData;

    public DataLocalProvider(IPersistentData persistentData) => _persistentData = persistentData;

    private string SavePath => Application.persistentDataPath;

    private string FullPath => Path.Combine(SavePath, $"{ConstantsExtension.FILE_NAME}{ConstantsExtension.SAVE_FILE_EXTENSION}");

    public bool TryLoad()
    {
        if (IsDataAlreadyExist() == false)
            return false;

        _persistentData.GameData = JsonConvert.DeserializeObject<GameData>(File.ReadAllText(FullPath));
        return true;
    }

    public void Save()
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(_persistentData.GameData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }

    private bool IsDataAlreadyExist() => File.Exists(FullPath);
}
