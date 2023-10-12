using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InputBindingWindow : EditorWindow
{
    private VisualTreeAsset bindingFieldPrefab;

    private ScrollViewUI<VisualElement> scrollViewUI = new();
    private Button addButton;

    private List<BindPanel> bindPanels = new();

    [MenuItem("Tools/Input Binding Manager")]
    public static void ShowWindow()
    {
        InputBindingWindow window = GetWindow<InputBindingWindow>();
        window.titleContent = new GUIContent("Input Binding Manager");
    }

    private void CreateGUI()
    {
        VisualTreeAsset mainWindowPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/02.UIBuilder/EventBinding/EventBindingWindow.uxml");
        mainWindowPrefab.CloneTree(rootVisualElement);

        bindingFieldPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/02.UIBuilder/EventBinding/BindingButton.uxml");

        addButton = rootVisualElement.Q<Button>("AddButton");
        addButton.clicked += AddEvent;

        scrollViewUI.Initialize(rootVisualElement.Q<ScrollView>("ScrollView"));

        LoadFile();
    }

    private void AddEvent()
    {
        VisualElement newElement = scrollViewUI.Create(bindingFieldPrefab);
        BindPanel newPanel = new(newElement);
        newPanel.OnRemove += RemoveEvent;

        bindPanels.Add(newPanel);
    }

    private void RemoveEvent(VisualElement visualElement)
    {
        int index = scrollViewUI.IndexOf(visualElement);

        bindPanels.RemoveAt(index);
        scrollViewUI.Remove(index);
    }

    private void UpdatePanels(List<InputEvent> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            bindPanels[i].SetInputEvent(list[i]);
        }
    }

    #region FILE
    private void LoadFile()
    {
        SerializedList<InputEvent> list =
        FileUtils.GetJsonFile<SerializedList<InputEvent>>($"{Application.dataPath}/Saves", "InputBinding.json");
        
        if (list == null || list.list == null)
        {
            list = new SerializedList<InputEvent>(new List<InputEvent>());
        }

        for (int i = 0; i < list.list.Count; i++)
            AddEvent();

        UpdatePanels(list.list);
    }

    private void SaveFile()
    {
        List<InputEvent> eventList = new();

        foreach (BindPanel panel in bindPanels)
        {
            eventList.Add(panel.InputEvent);
        }

        string filePath = $"{Application.dataPath}/Saves/InputBinding.json";
        FileUtils.SaveJsonFile(filePath, new SerializedList<InputEvent>(eventList));
    }
    #endregion

    private void OnDestroy()
    {
        SaveFile();
    }
}