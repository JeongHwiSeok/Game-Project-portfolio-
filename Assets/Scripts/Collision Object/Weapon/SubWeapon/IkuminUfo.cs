using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminUfo : Weapon
{
    [SerializeField] float time;
    [SerializeField] float duration;

    private void OnEnable()
    {
        time = 0;
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
        if (gameObject.activeSelf && time > duration)
        {
            gameObject.SetActive(false);
        }
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
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
