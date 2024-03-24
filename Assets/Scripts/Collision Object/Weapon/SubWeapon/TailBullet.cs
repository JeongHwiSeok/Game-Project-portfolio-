using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailBullet : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void OffCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    public void Target(Vector3 _target)
    {
        target = _target;
    }

    public void DisableOn()
    {
        gameObject.SetActive(false);
    }
}
