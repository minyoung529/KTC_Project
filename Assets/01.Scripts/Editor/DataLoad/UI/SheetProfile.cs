using SheetImporter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SheetProfile
{
    #region PREFAB
    private VisualTreeAsset spreadTypeField;
    #endregion

    #region TEXT FIELD
    private TextField nameField;
    private TextField addressField;
    private TextField gidField;
    private TextField rangeField;
    #endregion

    #region TYPE LIST
    private ScrollViewUI<EnumField> typeScrollViewUI = new();
    #endregion

    private Label sheetLinkText;
    private EventCallback<ChangeEvent<string>> nameChangeEvent;
    private EventCallback<ChangeEvent<string>> addressChangeEvent;

    private SheetInformation curInfo = null;

    public void Initizlie(VisualElement rootVisualElement)
    {
        spreadTypeField = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/02.UIBuilder/SpreadLoad/SpreadType.uxml");

        nameField = rootVisualElement.Q<TextField>("NameField");
        addressField = rootVisualElement.Q<TextField>("AddressField");
        gidField = rootVisualElement.Q<TextField>("GidField");
        rangeField = rootVisualElement.Q<TextField>("RangeField");

        typeScrollViewUI.Initialize(rootVisualElement.Q<ScrollView>("TypeScrollView"));
        sheetLinkText = rootVisualElement.Q<Label>("SheetLinkLabel");

        nameChangeEvent += SetNameField;
        addressChangeEvent += OnAddressChanged;

        nameField.RegisterCallback(nameChangeEvent);

        addressField.RegisterCallback(addressChangeEvent);
        gidField.RegisterCallback(addressChangeEvent);
        rangeField.RegisterCallback(addressChangeEvent);
    }

    private void SetNameField(ChangeEvent<string> value)
    {
        if (curInfo == null) return;
        curInfo.sheetName = value.newValue;
    }

    public void OnSelectSpread(SheetInformation info)
    {
        curInfo = info;
        if (curInfo == null) return;

        nameField.SetValueWithoutNotify(curInfo.sheetName);
        addressField.SetValueWithoutNotify(curInfo.sheetAddress);
        gidField.SetValueWithoutNotify(curInfo.sheetGid);
        rangeField.SetValueWithoutNotify(curInfo.sheetRange);

        SetSheetLinkText(curInfo);
        UpdateList(curInfo);
    }

    public void OnNameChanged(EventCallback<ChangeEvent<string>> evt)
    {
        nameField.RegisterCallback(evt);
    }

    public void OnAddressChanged(ChangeEvent<string> value)
    {
        curInfo.sheetRange = rangeField.text;
        curInfo.sheetGid = gidField.text;
        curInfo.sheetAddress = addressField.text;

        SetSheetLinkText(curInfo);
    }

    private void SetSheetLinkText(SheetInformation info)
    {
        sheetLinkText.text = $"Sheet Link: {info.GetAddress()}";
    }

    public void UpdateList(SheetInformation info)
    {
        int diff = info.variableNames.Count - typeScrollViewUI.Items.Count;

        // Enum Field�� �� ���ٸ� (=> Enum Field�� �ʿ���)
        if (diff > 0)
        {
            // ����
            for (int i = 0; i < Mathf.Abs(diff); i++)
                CreateSpreadData();
        }

        for (int i = 0; i < typeScrollViewUI.Items.Count; i++)
        {
            bool active = (i < info.variableNames.Count);
            EnumField enumField = typeScrollViewUI.Items[i];

            enumField.visible = active;

            if (active)
            {
                enumField.Init(DataType.Int);
                enumField.label = info.variableNames[i];
                enumField.value = info.types[i];
            }
        }
    }

    private void CreateSpreadData()
    {
        EnumField enumField = typeScrollViewUI.Create(spreadTypeField);

        EventCallback<ChangeEvent<Enum>> evt = (x) => OnChangeEnumValue(x, enumField);
        enumField.RegisterValueChangedCallback(evt);
    }

    private void OnChangeEnumValue(ChangeEvent<Enum> evt, EnumField field)
    {
        int idx = typeScrollViewUI.IndexOf(field);
        curInfo.types[idx] = (DataType)evt.newValue;
    }
}