using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteHand : Weapon
{
    private void OnEnable()
    {
        transform.position = new Vector3(0, 1.5f, 0);
        normalspeed = 240f;
        atk = 15f;
        knockBack = 1f;
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
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Weapon.instance.aswSpeedBuff * Weapon.instance.pwsSpeedBuff * Time.deltaTime * AoiWeapon.instance.spdBuff);
        transform.localScale = new Vector3(4 * AoiWeapon.instance.size, 4 * AoiWeapon.instance.size, 4 * AoiWeapon.instance.size);
    }
}
