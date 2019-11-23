using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
   

    public static void SaveScore(ScoreData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/binaries.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ScoreData LoadData()
    {
        string path = Application.persistentDataPath + "/binaries.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            stream.Close();
            return data;
        }
        else //on crée le binary avec les données de bases définies par le constructeur par défaut de ScoreData
        {
            Debug.LogError("Save file not found");
            ScoreData data = new ScoreData();

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, data);
            stream.Close();

            return data;
        }
    }
}
