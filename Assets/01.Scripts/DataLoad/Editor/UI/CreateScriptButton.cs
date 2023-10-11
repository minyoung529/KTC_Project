using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateScriptButton
{
    private Button createButton;
    private string scriptPath;

    public void Initialize(Button button)
    {
        scriptPath = $"{Application.dataPath}/01.Scripts/SO";
        createButton = button;
        createButton.clicked += CreateSOScript;
    }

    private void CreateSOScript()
    {
        SheetInformation info = SheetManagingWindow.CurInfo;
        string source = SOScripterGenerator.GetSOSourceCode(info.sheetName, info.GetDirectionary());

        FileUtils.CreateFile(scriptPath, $"{info.sheetName}SO.cs", source);
        Debug.Log($"Create {info.sheetName}SO.cs!");
    }
}
