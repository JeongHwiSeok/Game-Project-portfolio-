using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBullet : Weapon
{
    private void OnEnable()
    {
        if (DataManager.instance.subArray[2, 7] == 1)
        {
            atk = 30 * (MenoWeapon.instance.jewalCount / 200 + 1) * MenoWeapon.instance.pickUpBuff;
        }
        else if (DataManager.instance.subArray[2, 7] == 2)
        {
            atk = 30 * (MenoWeapon.instance.jewalCount / 100 + 1) * MenoWeapon.instance.pickUpBuff;
        }
        else
        {
            atk = 30 * (MenoWeapon.instance.jewalCount / 50 + 1) * MenoWeapon.instance.pickUpBuff;
        }
    }

    public void ObjectOff()
    {
        GameObject parent = transform.parent.gameObject;
        parent.transform.gameObject.SetActive(false);
    }
}
