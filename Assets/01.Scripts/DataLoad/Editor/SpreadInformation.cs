using System.Collections.Generic;
using System.Net;
using System.Runtime.Remoting.Messaging;

[System.Serializable]
public class SpreadInformation
{
    public string sheetName;
    public string sheetAddress;
    public string sheetRange;
    public string sheetGid;
    public int index;

    // 직렬화용
    public List<string> variableNames;
    public List<DataType> types;

    public string GetAddress()
    {
        string link = $"https://docs.google.com/spreadsheets/d/{sheetAddress}/export?format=tsv";

        if (sheetRange.Length > 0)
            link = $"{link}&range={sheetRange}";

        if (sheetGid.Length > 0)
            link = $"{link}&gid={sheetGid}";

        return link;
    }

    public Dictionary<string, DataType> GetDirectionary()
    {
        Dictionary<string, DataType> dict = new Dictionary<string, DataType>();

        for(int i = 0; i < types.Count; i++)
        {
            dict.Add(variableNames[i], types[i]);   
        }

        return dict;
    }
}

[System.Serializable]
public class SerlizedList<T>
{
    public SerlizedList(List<T> list)
    {
        this.list = list;
    }

    public List<T> list;
}