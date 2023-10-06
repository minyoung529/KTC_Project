using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpreadManagingWindow : EditorWindow
{
    [MenuItem("Tools/ManageSpread")]
    public static void ShowWindow()
    {
        GetWindow<SpreadManagingWindow>("Spread Manager");
    }

    private void OnGUI()
    {

    }
}
