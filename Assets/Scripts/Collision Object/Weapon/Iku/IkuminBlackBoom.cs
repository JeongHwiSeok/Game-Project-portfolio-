using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminBlackBoom : Weapon
{
    private void OnEnable()
    {
        atk = 100 * BuffDebuffManager.instance.shopSkillAtk;
        normalspeed = 5;
        knockBack = 1;
        SpeedUP();
    }

    public void Boom()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
