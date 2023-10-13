using System.Collections.Generic;
using System.Diagnostics;
using SheetImporter;

[System.Serializable]
public class SheetInformation
{
    public string sheetName;
    public string sheetAddress;
    public string sheetRange;
    public string sheetGid;
    public int index;

    // 직렬화용
    public List<string> variableNames;
    public List<DataType> types;

    public string sheet;

    public string GetAddress()
    {
        string link = $"https://docs.google.com/spreadsheets/d/{sheetAddress}/export?format=tsv";

        if (!string.IsNullOrEmpty(sheetRange) && sheetRange.Length > 0)
            link = $"{link}&range={sheetRange}";

        if (!string.IsNullOrEmpty(sheetGid) && sheetGid.Length > 0)
            link = $"{link}&gid={sheetGid}";

        return link;
    }

    public Dictionary<string, DataType> GetDirectionary()
    {
        Dictionary<string, DataType> dict = new Dictionary<string, DataType>();

        for (int i = 0; i < types.Count; i++)
        {
            dict.Add(variableNames[i], types[i]);
        }

        return dict;
    }
}
