using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InputEvent 
{
    public KeyCode key;
    public KeyActionType actType;
    public EventName evt;
}
