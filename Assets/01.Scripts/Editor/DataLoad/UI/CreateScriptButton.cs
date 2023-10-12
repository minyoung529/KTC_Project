using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;

public class CreateScriptButton
{
    private Button createButton;
    private string scriptPath;
    private string latelyMadeScriptName = "";

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
        string title = $"{info.sheetName}SO.cs";
        latelyMadeScriptName = title;

        Debug.Log($"Create {title}!");
        FileUtils.CreateFile(scriptPath, title, source);

        AssetDatabase.Refresh();
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath($"Assets/01.Scripts/SO/{latelyMadeScriptName}");

        CompilationPipeline.compilationFinished += OnCompileEnd;

    }

    private void OnCompileEnd(object value)
    {
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath($"Assets/01.Scripts/SO/{latelyMadeScriptName}");
        CompilationPipeline.compilationFinished -= OnCompileEnd;
    }
}
