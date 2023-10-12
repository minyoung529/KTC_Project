using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SheetList
{
    private VisualTreeAsset spreadDataBtnPrefab;

    private List<SheetInformation> spreadInfos = new();
    private ScrollViewUI<Button> scrollViewUI = new();

    #region PROPERTY
    public Action<SheetInformation> OnSelectSpread { get; set; }
    private int SelectedIdx => scrollViewUI.SelectedIdx;
    #endregion

    public void Initialize(List<SheetInformation> infos, ScrollView itemViewList)
    {
        spreadDataBtnPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/02.UIBuilder/SpreadLoad/SpreadUIButton.uxml");
        scrollViewUI.Initialize(itemViewList);
        scrollViewUI.OnSelectIdx += OnSelectedIdx;

        spreadInfos = infos;

        if (infos != null)
        {
            // 데이터 토대로 생성
            for(int i = 0; i < infos.Count; i++)
            {
                AddSpreadData();
            }
        }

        Update();
    }

    private void AddSpreadData()
    {
        scrollViewUI.Create(spreadDataBtnPrefab);
    }

    public void MakeNewSpreadData()
    {
        SheetInformation newInfo = new SheetInformation();
        int idx = spreadInfos.Count;

        newInfo.sheetName = $"New Spread Sheet ({idx})";
        newInfo.index = idx;
        spreadInfos.Add(newInfo);
        AddSpreadData();
        Update();
    }

    public void OnSelectedIdx()
    {
        if (SelectedIdx < 0) return;
        OnSelectSpread?.Invoke(spreadInfos[SelectedIdx]);
    }

    public void RemoveSpreadData()
    {
        if (SelectedIdx < 0) return;

        scrollViewUI.Remove(SelectedIdx);
        spreadInfos.RemoveAt(SelectedIdx);

        Update();
        scrollViewUI.Select(-1);
    }

    private void Update()
    {
        for (int i = 0; i < spreadInfos.Count; i++)
            spreadInfos[i].index = i;

        UpdateButtons();
    }

    public void OnNamechange(ChangeEvent<string> evt)
    {
        if (SelectedIdx < 0) return;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        IReadOnlyList<Button> buttons = scrollViewUI.Items;

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].text = $"{i + 1}. {spreadInfos[i].sheetName}";
        }
    }
}