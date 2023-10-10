using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpreadLoadButton
{
    private Button loadButton;
    private Label loadProgressText;
    private DownloadSheetTSV downloadSheet = new();

    public void Initialize(VisualElement visualElement)
    {
        loadButton = visualElement.Q<Button>("LoadButton");
        loadProgressText = visualElement.Q<Label>("LoadProgressText");

        loadButton.clicked += OnLoadButtonClicked;
    }

    private void OnLoadButtonClicked()
    {
        if (SpreadManagingWindow.CurInfo == null)
            return;

        loadProgressText.visible = true;
        loadProgressText.style.color = Color.white;
        loadProgressText.text = "Loading...";
        downloadSheet.Download(SpreadManagingWindow.CurInfo, OnLoadSuccess, OnLoadFail);
    }

    private void OnLoadSuccess(string value)
    {
        loadProgressText.style.color = Color.green;
        loadProgressText.text = "Success!";
    }

    private void OnLoadFail()
    {
        loadProgressText.style.color = Color.red;
        loadProgressText.text = "Load Fail...";
    }

    public void OnSelectedSpread(SpreadInformation info)
    {
        loadProgressText.visible = false;
    }
}
