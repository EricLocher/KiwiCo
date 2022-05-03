using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static void Save(Save save)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/fot.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(save);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static GameData Load()
    {
        string path = Application.persistentDataPath + "/fot.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save not found in: " + path);
            return null;
        }
    }
}