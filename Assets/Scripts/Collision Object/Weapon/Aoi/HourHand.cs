using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourHand : Weapon
{
    private void OnEnable()
    {
        transform.position = new Vector3(0, 1.25f, 0);
        normalspeed = 20f;
        atk = 10f;
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
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Weapon.instance.aswSpeedBuff * Weapon.instance.pwsSpeedBuff * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MinuteHand weapon = other.GetComponent<MinuteHand>();

        if (weapon != null)
        {
            if(AoiWeapon.instance.standbyWeapon[3].activeSelf == false)
            {
                AoiWeapon.instance.standbyWeapon[3].gameObject.SetActive(true);
                AoiWeapon.instance.standbyWeapon[3].gameObject.GetComponent<Weapon>().Atk = atk * 2;
                AoiWeapon.instance.standbyWeapon[3].gameObject.GetComponent<Weapon>().NormalSpeed = normalspeed * Weapon.instance.aswSpeedBuff * Weapon.instance.pwsSpeedBuff * 18;
            }
            else if (AoiWeapon.instance.standbyWeapon[5].activeSelf == false)
            {
                AoiWeapon.instance.standbyWeapon[5].gameObject.SetActive(true);
                AoiWeapon.instance.standbyWeapon[5].gameObject.GetComponent<Weapon>().Atk = atk * 2;
                AoiWeapon.instance.standbyWeapon[5].gameObject.GetComponent<Weapon>().NormalSpeed = normalspeed * Weapon.instance.aswSpeedBuff * Weapon.instance.pwsSpeedBuff * 18;
            }
        }
    }
}
