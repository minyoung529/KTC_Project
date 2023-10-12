using SheetImporter;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CreateSOButton
{
    private Button createButton;
    private string soPath;

    public void Initialize(Button button)
    {
        soPath = $"Assets/03.SOs";
        createButton = button;
        createButton.clicked += CreateAllSOs;
    }

    private void CreateAllSOs()
    {
        SheetInformation sheetInfo = SheetManagingWindow.CurInfo;
        string directory = $"{Application.dataPath}/03.SOs/{sheetInfo.sheetName}";
        string[] rows = sheetInfo.sheet.Split('\n');
        
        if (!File.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // 변수 이름 부분(Top) 제외
        for (int i = 1; i < rows.Length; i++)
        {
            string[] datas = rows[i].Split('\t');
            CreateSO(datas);
        }

        string data = rows[1].Split('\t')[0];
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath($"Assets/03.SOs/{sheetInfo.sheetName}/{data}.asset");
    }

    private void CreateSO(string[] values)
    {
        SheetInformation info = SheetManagingWindow.CurInfo;

        ScriptableObject newObj = ScriptableObject.CreateInstance($"{info.sheetName}SO");
        List<FieldInfo> fieldInfos = new();

        foreach (string str in info.variableNames)
        {
            // add fields
            string fieldName = str.FirstCharacterToLower();
            fieldInfos.Add(newObj.GetType().GetField(fieldName.Trim()));
        }

        for (int i = 0; i < info.types.Count; i++)
        {
            // set value on fields
            fieldInfos[i].SetValue(newObj, GetObjectData(values[i], info.types[i]));
        }

        //newObj.name = ;
        AssetDatabase.CreateAsset(newObj, $"{soPath}/{info.sheetName}/{values[0]}.asset");
        AssetDatabase.SaveAssets();
        Selection.activeObject = newObj;
    }

    private object GetObjectData(string data, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Int:
                return int.Parse(data);

            case DataType.Float:
                return float.Parse(data);

            case DataType.String:
                return data;

            case DataType.Bool:
                return bool.Parse(data);

            default:
                break;
        }

        return "";
    }
}
