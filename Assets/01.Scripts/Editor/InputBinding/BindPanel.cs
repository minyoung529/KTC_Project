using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class BindPanel 
{
    private Button removeButton;
    private EnumField inputField;
    private EnumField eventField;
    private EnumField actTypeField;
    private VisualElement visualElement;

    private InputEvent inputEvent;

    #region PROPERTY
    public Action<VisualElement> OnRemove { get; set; }

    public InputEvent InputEvent => inputEvent;
    #endregion

    public BindPanel(VisualElement visualElement)
    {
        Initialize(visualElement);
    }

    public void Initialize(VisualElement visualElement)
    {
        this.visualElement = visualElement;

        removeButton = visualElement.Q<Button>("RemoveButton");
        inputField = visualElement.Q<EnumField>("InputField");
        eventField = visualElement.Q<EnumField>("EventField");
        actTypeField = visualElement.Q<EnumField>("ActionField");

        inputField.Init(KeyCode.A);
        eventField.Init(EventName.None);
        actTypeField.Init(KeyActionType.KeyDown);

        inputField.RegisterValueChangedCallback(OnChangeInput);
        eventField.RegisterValueChangedCallback(OnChangeEvent);
        actTypeField.RegisterValueChangedCallback(OnChangeActType);

        removeButton.clicked += RemoveFunction; 
    }

    public void SetInputEvent(InputEvent inputEvent)
    {
        this.inputEvent = inputEvent;
        inputField.SetValueWithoutNotify(inputEvent.key);
        eventField.SetValueWithoutNotify(inputEvent.evt);
        actTypeField.SetValueWithoutNotify(inputEvent.actType);
    }

    private void RemoveFunction()
    {
        OnRemove?.Invoke(visualElement);
    }

    private void OnChangeInput(ChangeEvent<Enum> input)
    {
        inputEvent.key = (KeyCode)input.newValue;
    }

    private void OnChangeEvent(ChangeEvent<Enum> input)
    {
        inputEvent.evt = (EventName)input.newValue;
    }

    private void OnChangeActType(ChangeEvent<Enum> input)
    {
        inputEvent.actType = (KeyActionType)input.newValue;
    }
}
