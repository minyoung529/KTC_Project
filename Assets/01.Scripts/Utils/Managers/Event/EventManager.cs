using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<EventName, Action> actionDict = new();

    public static void StartListening(EventName eventName, Action action)
    {
        if (!actionDict.ContainsKey(eventName))
        {
            actionDict.Add(eventName, action);
        }
        else
        {
            actionDict[eventName] += action;
        }
    }

    public static void StopListening(EventName eventName, Action action)
    {
        if (!actionDict.ContainsKey(eventName))
            return;

        actionDict[eventName] -= action;
    }

    public static void Trigger(EventName eventName)
    {
        if (!actionDict.ContainsKey(eventName)) return;
        actionDict[eventName]?.Invoke();
    }
}

public static class EventManager<T>
{
    private static Dictionary<EventName, Action<T>> actionDict;

    public static void StartListening(EventName eventName, Action<T> action)
    {
        actionDict[eventName] += action;
    }

    public static void StopListening(EventName eventName, Action<T> action)
    {
        actionDict[eventName] -= action;
    }

    public static void Trigger(EventName eventName, T value)
    {
        actionDict[eventName]?.Invoke(value);
    }
}
