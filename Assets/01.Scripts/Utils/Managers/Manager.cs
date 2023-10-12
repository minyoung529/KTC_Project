using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager 
{
    public virtual void Initialize() { }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }

    public virtual void Destroy() { }
}
