using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileUtils
{
    private static string SavePath
    {
        get
        {
#if UNITY_EDITOR
            return $"{Application.dataPath}/saves/";
#else
            return $"{Application.persistentDataPath}/saves/";
#endif
        }
    }

    public static T GetJsonFile<T>(string directoryPath, string name) where T : class
    {
        return LoadJsonFile<T>(directoryPath, name);
    }

    private static T LoadJsonFile<T>(string directoryPath, string name) where T : class
    {
        string directory = directoryPath;
        string filePath = $"{directoryPath}/{name}";
        string json;

        if (File.Exists(filePath))
        {
            json = File.ReadAllText(filePath);
            T value = JsonUtility.FromJson<T>(json);
            return value;
        }
        else
        {
            if (!File.Exists(directory))
                Directory.CreateDirectory(directory);

            SaveJsonFile<T>(filePath);
            LoadJsonFile<T>(directoryPath, name);
        }

        return null;
    }

    private static void SaveJsonFile<T>(string path) where T : class
    {
        T value = default;
        string json = JsonUtility.ToJson(value, true);

        File.WriteAllText(path, json, System.Text.Encoding.UTF8);
    }

    public static void SaveJsonFile<T>(string path, T value) where T : class
    {
        string json = JsonUtility.ToJson(value, true);
        File.WriteAllText(path, json, System.Text.Encoding.UTF8);
    }

    public static void CreateFile(string directory, string name, string value)
    {
        string path = $"{directory}/{name}";

        if(File.Exists(path))
        {
            // µ¤¾î¾²±â
            File.Delete(path);
        }

        if (!File.Exists(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllText(path, value, System.Text.Encoding.UTF8);
    }
}
