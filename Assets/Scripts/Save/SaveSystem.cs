using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(string currentLevel)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(currentLevel);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveMusicData(float musicVolume)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/musicData.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        MusicData musicData = new MusicData(musicVolume);
        
        formatter.Serialize(stream, musicData);
        stream.Close();
    }

    public static MusicData LoadMusicData()
    {
        string path = Application.persistentDataPath + "/musicData.bin";
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        
        MusicData musicData = formatter.Deserialize(stream) as MusicData;
        stream.Close();

        return musicData;
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.bin";
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();

        return data;
    }
}
