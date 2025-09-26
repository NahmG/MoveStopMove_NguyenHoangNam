using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class SaveDataJSON : MonoBehaviour
{
    public ShopData shopData;

    public void SaveData()
    {
        string json = JsonUtility.ToJson(shopData);
        Debug.Log(json);

        using StreamWriter writer = new(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json");
        writer.Write(json);
    }

    public void LoadData()
    {
        string json = string.Empty;

        using (StreamReader reader = new(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            json = reader.ReadToEnd();
        }

        ShopData data = JsonUtility.FromJson<ShopData>(json);
        shopData.SetData(data.allItems);
    }
}
