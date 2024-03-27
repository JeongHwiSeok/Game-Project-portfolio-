using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBullet : Weapon
{
    private void OnEnable()
    {
        if (DataManager.instance.subArray[2, 7] == 1)
        {
            atk = 30 * (MenoManager.instance.jewalCount / 200 + 1) * MenoManager.instance.pickUpBuff * BuffDebuffManager.instance.shopSkillAtk;
        }
        else if (DataManager.instance.subArray[2, 7] == 2)
        {
            atk = 30 * (MenoManager.instance.jewalCount / 100 + 1) * MenoManager.instance.pickUpBuff * BuffDebuffManager.instance.shopSkillAtk;
        }
        else
        {
            atk = 30 * (MenoManager.instance.jewalCount / 50 + 1) * MenoManager.instance.pickUpBuff * BuffDebuffManager.instance.shopSkillAtk;
        }
    }
}
