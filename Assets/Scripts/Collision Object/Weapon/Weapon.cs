using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float atk;
    [SerializeField] protected float knockBack;
    [SerializeField] protected float speed;
    [SerializeField] protected float normalspeed;

    public static Weapon instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        instance = this;
    }
    public virtual float Atk
    {
        set { atk = value; }
        get { return atk; }
    }
    public virtual float KnockBack
    {
        set { knockBack = value; }
        get { return knockBack; }
    }

    public virtual float Speed
    {
        set { speed = value; }
        get { return speed; }
    }

    public virtual float NormalSpeed
    {
        set { normalspeed = value; }
        get { return normalspeed; }
    }

    public virtual void SpeedUP()
    {
        speed = normalspeed * BuffDebuffManager.instance.aoiP2SpeedBuff * BuffDebuffManager.instance.pwsSpeedBuff * BuffDebuffManager.instance.shopAtkSpeed;
    }
}
