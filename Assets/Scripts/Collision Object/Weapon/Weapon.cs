using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public List<Weapon> weaponCount;
    public int attackCount;

    protected virtual void Start()
    {
        attackCount = this.attackCount;
        weaponCount.Capacity = 10;
        Create();
        StartCoroutine(Attack());
    }

    protected abstract void Create();
    protected abstract IEnumerator Attack();
}
