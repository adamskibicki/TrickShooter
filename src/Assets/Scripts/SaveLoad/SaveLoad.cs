using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static List<Profile> savedProfiles = new List<Profile> { null, null, null };

    public static void Save(int currentPofileIndex)
    {
        savedProfiles[currentPofileIndex] = Profile.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, savedProfiles);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            savedProfiles = (List<Profile>)bf.Deserialize(file);
            file.Close();
        }
    }
}
