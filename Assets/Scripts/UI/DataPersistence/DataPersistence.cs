using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// 数据持久化
/// </summary>
public class DataPersistence : MonoBehaviour
{
    protected static string dataFileName = nameof(DataPersistence)+".data";
    protected static string GetDataPath => Path.Combine(Environment.CurrentDirectory, dataFileName);

    public static void GameDataPersistence<T>(T data) where T:class
    {
        MemoryStream ms = new MemoryStream();
        ms.Position = 0;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(ms, data);
        byte[] buffer = ms.GetBuffer();
        File.Delete(GetDataPath);
        File.WriteAllBytes(GetDataPath, buffer);
        ms.Close();
    }

    public static T GetPersistenceGameData<T>() where T : class
    {
        if (!File.Exists(GetDataPath)) return default;
        byte[] buffer = File.ReadAllBytes(GetDataPath);
        MemoryStream ms = new MemoryStream(buffer);
        ms.Position = 0;
        BinaryFormatter bf = new BinaryFormatter();
        T dataClass = (T)bf.Deserialize(ms);
        ms.Close();
        return dataClass;
    }

}
