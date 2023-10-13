using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MovementSO")]
public class MovementSO : ScriptableObject
{
    public string name;
    public float speed;
    public string guid;

    private void OnValidate()
    {
        //AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();
        //guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this));

        //if (guid == "")
        //{
        //    guid = "ㅠㅠ";
        //}
    }
    private void OnEnable()
    {
        //string filePath = $"Assets/03.SOs/MovementSO/{name}.asset";
        //GUID id = AssetDatabase.GUIDFromAssetPath(filePath);

        EditorCoroutineUtility.StartCoroutine(s(), this);
    }

    private IEnumerator s()
    {
        guid = "Loading...";

        while (AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this)) == "")
        {
            yield return null;
        }

        guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this));

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}