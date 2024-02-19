using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourHand : Weapon
{
    private float speed;

    private void OnEnable()
    {
        transform.position = new Vector3(0, 1.25f, 0);
        speed = 20f;
    }

    private void Update()
    {
        AttackDirection();
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        MinuteHand weapon = other.GetComponent<MinuteHand>();

        if (weapon != null)
        {
            if(AoiWeapon.instance.standbyWeapon[2].activeSelf == false)
            {
                AoiWeapon.instance.standbyWeapon[2].gameObject.SetActive(true);
            }
        }
    }
}
