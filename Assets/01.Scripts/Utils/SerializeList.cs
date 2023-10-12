
using System.Collections.Generic;

[System.Serializable]
public class SerializedList<T>
{
    public SerializedList(List<T> list)
    {
        this.list = list;
    }

    public List<T> list;
}