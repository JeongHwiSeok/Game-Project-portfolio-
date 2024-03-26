using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketWatchString : MonoBehaviour
{
    [SerializeField] public int itemLV;

    private void Awake()
    {
        itemLV = 1;
    }
    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                BuffDebuffManager.instance.pwsSpeedBuff = 1.1f;
                break;
            case 2:
                BuffDebuffManager.instance.pwsSpeedBuff = 1.2f;
                break;
            case 3:
                BuffDebuffManager.instance.pwsSpeedBuff = 1.3f;
                break;
            case 4:
                BuffDebuffManager.instance.pwsSpeedBuff = 1.4f;
                break;
            case 5:
                BuffDebuffManager.instance.pwsSpeedBuff = 1.5f;
                break;
        }
        BuffDebuffManager.instance.pwsDamageDebuff = 1.3f;
    }
}
