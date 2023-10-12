using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SheetLoadButton
{
    private Button loadButton;
    private Label loadProgressText;
    private DownloadSheetTSV downloadSheet = new();

    public Action<string> OnSuccessLoad { get; set; }
    public Action OnFailLoad { get; set; }

    public void Initialize(VisualElement visualElement)
    {
        loadButton = visualElement.Q<Button>("LoadButton");
        loadProgressText = visualElement.Q<Label>("LoadProgressText");

        loadButton.clicked += OnLoadButtonClicked;

        OnSuccessLoad = OnLoadSuccess;
        OnFailLoad = OnLoadFail;
    }

    private void OnLoadButtonClicked()
    {
        if (SheetManagingWindow.CurInfo == null)
            return;

        loadProgressText.visible = true;
        loadProgressText.style.color = Color.white;
        loadProgressText.text = "Loading...";
        downloadSheet.Download(SheetManagingWindow.CurInfo, OnSuccessLoad, OnFailLoad);
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

    public void OnSelectedSpread(SheetInformation info)
    {
        loadProgressText.visible = false;
    }
}
