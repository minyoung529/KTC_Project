using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

public class SheetList
{
    private VisualTreeAsset spreadDataBtnPrefab;
    private ScrollView itemViewList;

    private List<SheetInformation> spreadInfos = new();
    private List<SheetButton> buttons = new();

    public Action<SheetInformation> OnSelectSpread { get; set; }

    int curIdx = -1;

    public void Initialize(List<SheetInformation> infos, ScrollView itemViewList)
    {
        this.itemViewList = itemViewList;
        spreadDataBtnPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/02.UIBuilder/SpreadLoad/SpreadUIButton.uxml");

        spreadInfos = infos;

        if (infos == null)
        {

        }
        else
        {
            // 데이터 토대로 생성
            foreach (SheetInformation info in infos)
            {
                AddSpreadData(info);
            }
        }
    }

    private void AddSpreadData(SheetInformation info)
    {
        SheetButton spreadBtn = new SheetButton();
        TemplateContainer container = spreadDataBtnPrefab.CloneTree();

        buttons.Add(spreadBtn);
        spreadBtn.Initialize(info, container);

        spreadBtn.Button.clicked += () => SetCurIndex(spreadBtn);
        itemViewList.Add(container);
    }

    public void MakeNewSpreadData()
    {
        SheetInformation newInfo = new SheetInformation();
        int idx = spreadInfos.Count;

        newInfo.sheetName = $"New Spread Sheet ({idx})";
        newInfo.index = idx;
        spreadInfos.Add(newInfo);

        AddSpreadData(newInfo);
    }

    public void SetCurIndex(SheetButton spreadBtn)
    {
        curIdx = spreadBtn.Index;
        OnSelectSpread?.Invoke(spreadInfos[curIdx]);
    }

    public void SetCurIndex(int idx)
    {
        curIdx = idx;
        OnSelectSpread?.Invoke(spreadInfos[curIdx]);
    }

    public void RemoveSpreadData()
    {
        if (curIdx < 0) return;

        buttons[curIdx].Container.Remove(buttons[curIdx].Button);

        spreadInfos.RemoveAt(curIdx);
        buttons.RemoveAt(curIdx);

        Update();

        curIdx = -1;
    }

    private void Update()
    {
        for (int i = 0; i < spreadInfos.Count; i++)
        {
            spreadInfos[i].index = i;
            buttons[i].SetText(spreadInfos[i].sheetName);
        }
    }

    public void OnNamechange(ChangeEvent<string> evt)
    {
        if (curIdx < 0) return;
        buttons[curIdx].SetText(evt.newValue);
    }
}