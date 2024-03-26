using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBulletRotation : Weapon
{
    private float time;

    private void OnEnable()
    {
        transform.position = new Vector3(0, 0, 0);
        speed = 120f;
    }

    private void Update()
    {
        if (GameManager.instance.state)
        {
            AttackDirection();
            time += Time.deltaTime;
            CheckDgree();
        } 
    }

    private void CheckDgree()
    {
        if (gameObject.activeSelf && time > 3f)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
    }
}
