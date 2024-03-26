using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourHand : Weapon
{
    private void OnEnable()
    {
        transform.position = new Vector3(0, 1.25f, 0);
        normalspeed = 15f;
        atk = 5f;
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
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * BuffDebuffManager.instance.aoiP2SpeedBuff * BuffDebuffManager.instance.pwsSpeedBuff * Time.deltaTime * AoiManager.instance.spdBuff);
        transform.localScale = new Vector3(3 * AoiManager.instance.size, 3 * AoiManager.instance.size, 3 * AoiManager.instance.size);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MinuteHand weapon = other.GetComponent<MinuteHand>();

        if (weapon != null && GameManager.instance.attackLV > 3)
        {
            if(AoiManager.instance.standbyWeapon[3].activeSelf == false)
            {
                AoiManager.instance.standbyWeapon[3].gameObject.SetActive(true);
                AoiManager.instance.standbyWeapon[3].gameObject.GetComponent<Weapon>().Atk = atk * 2;
                AoiManager.instance.standbyWeapon[3].gameObject.GetComponent<Weapon>().NormalSpeed = normalspeed * BuffDebuffManager.instance.aoiP2SpeedBuff * BuffDebuffManager.instance.pwsSpeedBuff * 18 * AoiManager.instance.spdBuff;
            }
            else if (AoiManager.instance.standbyWeapon[5].activeSelf == false)
            {
                AoiManager.instance.standbyWeapon[5].gameObject.SetActive(true);
                AoiManager.instance.standbyWeapon[5].gameObject.GetComponent<Weapon>().Atk = atk * 2;
                AoiManager.instance.standbyWeapon[5].gameObject.GetComponent<Weapon>().NormalSpeed = normalspeed * BuffDebuffManager.instance.aoiP2SpeedBuff * BuffDebuffManager.instance.pwsSpeedBuff * 18 * AoiManager.instance.spdBuff;
            }
        }
    }
}
