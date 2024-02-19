using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionObject : MonoBehaviour
{
    public abstract void Activate(PlayerManager player);

    public abstract void UnActivate(PlayerManager player);

    public virtual void CollisionActivate(PlayerManager player)
    {

    }

    public virtual void CollisionUnActivate(PlayerManager player)
    {

    }
}
