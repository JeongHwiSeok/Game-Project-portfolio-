using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialHairpin : MonoBehaviour
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
                PickUP.instance.specialHairpin = 1.2f;
                break;
            case 2:
                PickUP.instance.specialHairpin = 1.4f;
                break;
            case 3:
                PickUP.instance.specialHairpin = 1.6f;
                break;
            case 4:
                PickUP.instance.specialHairpin = 1.8f;
                break;
            case 5:
                PickUP.instance.specialHairpin = 2.0f;
                break;
        }
        PickUP.instance.PickUPRangeUP();
    }
}
