using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using SheetImporter;

public class SheetManagingWindow : EditorWindow
{
    private List<SheetInformation> spreadInfos = new();
    public static SheetInformation CurInfo { get; private set; }

    private ScrollView itemViewList;
    private Button addButton;
    private Button removeButton;

    #region Manage Instance
    private SheetList spreadList = new();
    private SheetProfile spreadProfile = new();
    private SheetLoadButton spreadLoadButton = new();
    private CreateScriptButton createScriptButton = new();
    private CreateSOButton createSOButton = new();
    #endregion

    [MenuItem("Tools/Spread Manager")]
    public static void ShowWindow()
    {
        SheetManagingWindow window = GetWindow<SheetManagingWindow>();
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
        SerlizedList<SheetInformation> list =
        FileUtils.GetJsonFile<SerlizedList<SheetInformation>>($"{Application.dataPath}/Saves", "SheetDatas.json");

        spreadInfos = list.list;
    }

    private void SaveSpreadSheetDatas()
    {
        string filePath = $"{Application.dataPath}/Saves/SheetDatas.json";
        FileUtils.SaveJsonFile(filePath, new SerlizedList<SheetInformation>(spreadInfos));
    }

    private void BindingVariables()
    {
        itemViewList = rootVisualElement.Q<ScrollView>("SpreadScroll");
        addButton = rootVisualElement.Q<Button>("AddButton");
        removeButton = rootVisualElement.Q<Button>("RemoveButton");
    }

    private void BindingEvent()
    {
        addButton.clicked += spreadList.MakeNewSpreadData;
        removeButton.clicked += spreadList.RemoveSpreadData;

        spreadList.OnSelectSpread += (x) => CurInfo = x;
        spreadList.OnSelectSpread += spreadProfile.OnSelectSpread;
        spreadList.OnSelectSpread += spreadLoadButton.OnSelectedSpread;

        spreadLoadButton.OnSuccessLoad += OnSuccessLoad;
        
        spreadProfile.OnNameChanged(spreadList.OnNamechange);
    }

    private void OnSuccessLoad(string sheet)
    {
        CurInfo.sheet = sheet;

        Dictionary<string, DataType> dataDict = ExtractSheetTypes.GetSheetTypes(sheet);

        CurInfo.variableNames = new List<string>();
        CurInfo.types = new List<DataType>();

        foreach (var pair in  dataDict)
        {
            CurInfo.variableNames.Add(pair.Key);
            CurInfo.types.Add(pair.Value);
        }

        spreadProfile.UpdateList(CurInfo);
    }

    private void Initialize()
    {
        spreadList.Initialize(spreadInfos, itemViewList);
        spreadProfile.Initizlie(rootVisualElement);
        spreadLoadButton.Initialize(rootVisualElement.Q<VisualElement>("LoadButtonElement"));
        createScriptButton.Initialize(rootVisualElement.Q<Button>("CreateScriptButton"));
        createSOButton.Initialize(rootVisualElement.Q<Button>("CreateSOButton"));
    }
}