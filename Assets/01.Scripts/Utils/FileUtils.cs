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

    public static string GetFileText(string fileName)
    {
        string path = SavePath + fileName;

        if (File.Exists(path))
        {
            return File.ReadAllText(fileName);
        }
        else
        {
            return "";
        }
    }
}
