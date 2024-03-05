using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float atk;
    [SerializeField] protected float knockBack;
    [SerializeField] protected float speed;

    public virtual float Atk
    {
        get { return atk; }
    }
    public virtual float KnockBack
    {
        get { return knockBack; }
    }

    public virtual float Speed
    {
        get { return speed; }
    }
}
