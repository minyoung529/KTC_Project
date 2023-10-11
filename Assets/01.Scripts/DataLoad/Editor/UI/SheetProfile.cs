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
    private ScrollView typeListView;
    private List<EnumField> enumFields = new List<EnumField>();
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

        typeListView = rootVisualElement.Q<ScrollView>("TypeScrollView");
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
        int diff = info.variableNames.Count - enumFields.Count;

        // Enum Field가 더 적다면 (=> Enum Field가 필요함)
        if (diff > 0)
        {
            // 생성
            for (int i = 0; i < Mathf.Abs(diff); i++)
                CreateSpreadData();
        }

        for (int i = 0; i < enumFields.Count; i++)
        {
            bool active = (i < info.variableNames.Count);

            enumFields[i].visible = active;

            if (active)
            {
                enumFields[i].Init(DataType.Int);
                enumFields[i].label = info.variableNames[i];
                enumFields[i].value = info.types[i];
            }
        }
    }

    private void CreateSpreadData()
    {
        TemplateContainer container = spreadTypeField.CloneTree();
        EnumField enumField = container.Q<EnumField>("EnumField");

        enumFields.Add(enumField);
        EventCallback<ChangeEvent<Enum>> evt = (x) => OnChangeEnumValue(x, enumField);
        enumField.RegisterValueChangedCallback(evt);

        typeListView.Add(container);
    }

    private void OnChangeEnumValue(ChangeEvent<Enum> evt, EnumField field)
    {
        int idx = enumFields.IndexOf(field);
        curInfo.types[idx] = (DataType)evt.newValue;
    }
}