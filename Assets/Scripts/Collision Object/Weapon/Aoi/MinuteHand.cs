using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteHand : Weapon
{
    private void OnEnable()
    {
        transform.position = new Vector3(0, 1.5f, 0);
        normalspeed = 180f;
        atk = 10f;
        knockBack = 0.5f;
        speed = normalspeed;
    }

    private void Update()
    {
        if (GameManager.instance.state)
        {
            AttackDirection();
        }
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * BuffDebuffManager.instance.aoiP2SpeedBuff * BuffDebuffManager.instance.pwsSpeedBuff * Time.deltaTime * AoiManager.instance.spdBuff);
        transform.localScale = new Vector3(4 * AoiManager.instance.size, 4 * AoiManager.instance.size, 4 * AoiManager.instance.size);
    }
}
