using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Persistance : MonoBehaviour
{  

    public static void SaveData(BackUP data,string fileName)
    {
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName + ".json";
        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public static void SaveData(string json, string fileName)
    {
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName + ".json";
        Debug.Log("Saving Data at " + savePath);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public static BackUP LoadData(string fileName)
    {
        try
        {
            using StreamReader reader = new StreamReader(Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName + ".json");
            string json = reader.ReadToEnd();

            if (json != null)
            {
                return JsonUtility.FromJson<BackUP>(json);
            }
            return null;

            //Character data = JsonUtility.FromJson<Character>(json);
            //Debug.Log(data.ToString());
        }
        catch(FileNotFoundException fnfe)
        {
            BackUP newuser = new BackUP();
            SaveData(newuser, "Local");
            return newuser;
        }
    }

}
