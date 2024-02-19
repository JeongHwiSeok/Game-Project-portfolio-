using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBullet : Weapon
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private float speed;

    public float Speed
    {
        get { return speed; }
    }

    
}
