using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Manager
{
    private List<InputEvent> inputEvents = new List<InputEvent>();

    public override void Initialize()
    {
        base.Initialize();

        // TODO: 사용자가 바꾼 거로 가져올 수 있게 하기

        string directory = $"{Application.dataPath}/Saves";
        SerializedList<InputEvent> list = FileUtils.GetJsonFile<SerializedList<InputEvent>>(directory, "InputBinding.json");

        if (list != null)
        {
            inputEvents = list.list;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (inputEvents == null)
            return;

        for (int i = 0; i < inputEvents.Count; i++)
        {
            if (ActionSuccess(inputEvents[i].key, inputEvents[i].actType))
            {
                EventManager.Trigger(inputEvents[i].evt);
                //EventManager<KeyCode>.Trigger(inputEvents[i].evt, inputEvents[i].key);
            }
        }
    }

    private bool ActionSuccess(KeyCode keyCode, KeyActionType actionType)
    {
        switch (actionType)
        {
            case KeyActionType.KeyDown:
                return Input.GetKeyDown(keyCode);

            case KeyActionType.KeyHold:
                return Input.GetKey(keyCode);

            case KeyActionType.KeyUp:
                return Input.GetKeyUp(keyCode);
        }

        return false;
    }
}
