using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CharacterFunction[] characterFunctions;

    private void Awake()
    {
        characterFunctions = GetComponentsInChildren<CharacterFunction>();
    }

    private void Start()
    {
        foreach(CharacterFunction function in characterFunctions)
        {
            function.Initialize(this);
        }
    }
}
