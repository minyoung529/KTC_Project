using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Codice.CM.Common;
using System.IO;
using Codice.Utils;

public class SpreadManagingWindow : EditorWindow
{
    private List<SpreadInformation> spreadInfos = new();
    public static SpreadInformation CurInfo { get; private set; }

    private ScrollView itemViewList;
    private Button addButton;
    private Button removeButton;
    private Button loadButton;

    #region Manage Instance
    private SpreadList spreadList = new();
    private SpreadProfile spreadProfile = new();
    private SpreadLoadButton spreadLoadButton = new();
    #endregion

    [MenuItem("Tools/Spread Manager")]
    public static void ShowWindow()
    {
        SpreadManagingWindow window = GetWindow<SpreadManagingWindow>();
        window.titleContent = new GUIContent("SpreadSheet Manager");
    }

    private void CreateGUI()
    {
        VisualTreeAsset mainWindowPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/02.UIBuilder/SpreadLoad/SpreadUIToolkit.uxml");
        mainWindowPrefab.CloneTree(rootVisualElement);

        LoadSpreadSheetDatas();
        BindingVariables();

        // 초기화
        Initialize();

        // 이벤트 바인딩
        BindingEvent();

        // 선택
        if (spreadInfos.Count > 0)
        {
            spreadList.SetCurIndex(0);
        }
    }


    private void OnDestroy()
    {
        SaveSpreadSheetDatas();
    }

    private void LoadSpreadSheetDatas()
    {
        string directory = $"{Application.dataPath}/Saves";
        string filePath = $"{directory}/SheetDatas.json";
        string json;

        if (File.Exists(filePath))
        {
            json = File.ReadAllText(filePath);
            spreadInfos = JsonUtility.FromJson<SerlizedList<SpreadInformation>>(json).list;
        }
        else
        {
            if (!File.Exists(directory))
                Directory.CreateDirectory(directory);

            SaveSpreadSheetDatas();
            LoadSpreadSheetDatas();
        }
    }

    private void SaveSpreadSheetDatas()
    {
        string filePath = $"{Application.dataPath}/Saves/SheetDatas.json";
        SerlizedList<SpreadInformation> list = new SerlizedList<SpreadInformation>(spreadInfos);
        string json = JsonUtility.ToJson(list, true);

        File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
    }

    private void BindingVariables()
    {
        itemViewList = rootVisualElement.Q<ScrollView>("SpreadScroll");
        addButton = rootVisualElement.Q<Button>("AddButton");
        removeButton = rootVisualElement.Q<Button>("RemoveButton");
        loadButton = rootVisualElement.Q<Button>("LoadButton");
    }

    private void BindingEvent()
    {
        addButton.clicked += spreadList.MakeNewSpreadData;
        removeButton.clicked += spreadList.RemoveSpreadData;

        spreadList.OnSelectSpread += (x) => CurInfo = x;
        spreadList.OnSelectSpread += spreadProfile.OnSelectSpread;
        spreadList.OnSelectSpread += spreadLoadButton.OnSelectedSpread;

        spreadProfile.OnNameChanged(spreadList.OnNamechange);
    }

    private void Initialize()
    {
        spreadList.Initialize(spreadInfos, itemViewList);
        spreadProfile.Initizlie(rootVisualElement);
        spreadLoadButton.Initialize(rootVisualElement.Q<VisualElement>("LoadButtonElement"));
    }
}
