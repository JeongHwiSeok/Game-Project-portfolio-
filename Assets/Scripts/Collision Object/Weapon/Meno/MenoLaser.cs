using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoLaser : Weapon
{
    private void OnEnable()
    {
        if (DataManager.instance.subArray[2, 7] == 1)
        {
            atk = 50 * (MenoWeapon.instance.jewalCount / 100 + 1) * MenoWeapon.instance.pickUpBuff;
        }
        else if (DataManager.instance.subArray[2, 7] == 2)
        {
            atk = 50 * (MenoWeapon.instance.jewalCount / 50 + 1) * MenoWeapon.instance.pickUpBuff;
        }
        else
        {
            atk = 50 * (MenoWeapon.instance.jewalCount / 25 + 1) * MenoWeapon.instance.pickUpBuff;
        }
        MenoWeapon.instance.jewalCount = 0;
    }

    public void ColliderOn()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderOff()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ObjectOff()
    {
        GameObject parent = transform.parent.gameObject;
        parent.transform.gameObject.SetActive(false);
    }
}
