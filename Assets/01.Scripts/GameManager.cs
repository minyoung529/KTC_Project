using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Manager> managers = new();

    private void Awake()
    {
        managers.Add(new InputManager());

        managers.ForEach(x => x.Initialize());
    }

    private void Update()
    {
        managers.ForEach(x => x.Initialize());
    }

    private void FixedUpdate()
    {
        managers.ForEach(x => x.FixedUpdate());
    }

    private void LateUpdate()
    {
        managers.ForEach(x => x.LateUpdate());
    }

    private void OnDestroy()
    {
        managers.ForEach(x => x.Destroy());
    }
}
