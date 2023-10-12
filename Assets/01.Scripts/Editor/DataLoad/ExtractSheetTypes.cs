using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using SheetImporter;

public static class ExtractSheetTypes
{
    public static Dictionary<string, DataType> GetSheetTypes(string sheet)
    {
        Dictionary<string, DataType> dict = new();
        string[] rows = sheet.Split('\n'); // ------

        if (rows.Length < 2)
        {
            Debug.Log("시트에 제목과 데이터가 없습니다.");
            return null;
        }

        string[] names = rows[0].Split('\t');
        string[] values = rows[1].Split('\t');

        for (int i = 0; i < names.Length; i++)
        {
            dict.Add(names[i], GetData(values[i]));
        }

        return dict;
    }

    private static DataType GetData(string value)
    {
        if (bool.TryParse(value, out bool val0))
            return DataType.Bool;

        if (int.TryParse(value, out int val2))
            return DataType.Int;

        if (float.TryParse(value, out float val))
            return DataType.Float;

        return DataType.String;
    }
}
