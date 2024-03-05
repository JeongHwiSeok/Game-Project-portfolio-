using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourHand : Weapon
{
    private void OnEnable()
    {
        transform.position = new Vector3(0, 1.25f, 0);
        speed = 20f;
        atk = 20f;
        knockBack = 1f;
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
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
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
