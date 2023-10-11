using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : CharacterFunction
{
    protected Rigidbody rigid;
    public Rigidbody Rigidbody => rigid;

    public override void Initialize(Character character)
    {
        rigid = character.GetComponent<Rigidbody>();    
    }

    public void Move(Vector2 moveVec)
    {
        rigid.MovePosition(rigid.position + (Vector3)moveVec);
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigid.velocity = velocity;
    }
}
