using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : CharacterFunction
{
    [SerializeField]
    protected MovementSO moveStat;

    [SerializeField]
    protected Rigidbody rigid;
    public Rigidbody Rigidbody => rigid;

    public override void Initialize(Character character)
    {
        OnInitialized(character);
    }

    public void Move(Vector2 moveVec)
    {
        //rigid.position += (Vector3)moveVec;
        rigid.MovePosition(rigid.position + (Vector3)moveVec * Time.deltaTime * moveStat.speed);
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigid.velocity = velocity;
    }

    protected virtual void OnInitialized(Character character) { }
}
