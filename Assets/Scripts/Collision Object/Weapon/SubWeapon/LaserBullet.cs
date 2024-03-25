using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
        AttackDirection(target);
        transform.position = direction;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
    }

    private void AttackDirection(Vector3 position)
    {
        direction = position - transform.position;

        direction.z = 0f;
        direction.Normalize();
        direction *= 2.7f * transform.localScale.x;
    }

    public void Target(Vector3 _target)
    {
        target = _target;
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    public void ColliderOn()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderOff()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void DisableOff()
    {
        gameObject.SetActive(false);
    }
}
