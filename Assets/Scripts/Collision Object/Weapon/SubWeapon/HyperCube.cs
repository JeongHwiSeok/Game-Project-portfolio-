using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperCube : Weapon
{
    [SerializeField] float time;
    [SerializeField] float duration;

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
        if (gameObject.activeSelf && time > duration)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    public void Duration(float num)
    {
        duration = num;
    }
}
