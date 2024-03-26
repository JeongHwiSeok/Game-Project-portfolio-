using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeChakramRotation : Weapon
{
    [SerializeField] float time;
    [SerializeField] float duration;
    [SerializeField] float angle;

    [SerializeField] float r;

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
        angle += Time.deltaTime * 4 * BuffDebuffManager.instance.pwsSpeedBuff * BuffDebuffManager.instance.aoiP2SpeedBuff;

        r += Time.deltaTime;

        transform.position = new Vector3(r * Mathf.Cos(angle), r *  Mathf.Sin(angle), 0);
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        r = 1.5f;
        angle = 0;
        SpeedUP();
    }

    public void Duration(float num)
    {
        duration = num;
    }
}
