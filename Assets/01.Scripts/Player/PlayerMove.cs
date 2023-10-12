using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerMove : CharacterMove
{
    private void Awake()
    {
        EventManager.StartListening(EventName.MoveUp, MoveUp);
        EventManager.StartListening(EventName.MoveDown, MoveDown);
        EventManager.StartListening(EventName.MoveLeft, MoveLeft);
        EventManager.StartListening(EventName.MoveRight, MoveRight);
    }

    private void MoveUp() => MoveToDir(Vector2.up);
    private void MoveDown() => MoveToDir(Vector2.down);
    private void MoveLeft() => MoveToDir(Vector2.left);
    private void MoveRight() => MoveToDir(Vector2.right);

    private void MoveToDir(Vector2 dir) => Move(dir * Time.deltaTime * moveStat.speed);

    private void OnDestroy()
    {
        EventManager.StopListening(EventName.MoveUp, MoveUp);
        EventManager.StopListening(EventName.MoveDown, MoveDown);
        EventManager.StopListening(EventName.MoveLeft, MoveLeft);
        EventManager.StopListening(EventName.MoveRight, MoveRight);
    }
}
