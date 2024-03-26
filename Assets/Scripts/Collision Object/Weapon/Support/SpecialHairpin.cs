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
                BuffDebuffManager.instance.shpPickUpRangePow = 1.1f;
                break;
            case 2:
                BuffDebuffManager.instance.shpPickUpRangePow = 1.2f;
                break;
            case 3:
                BuffDebuffManager.instance.shpPickUpRangePow = 1.3f;
                break;
            case 4:
                BuffDebuffManager.instance.shpPickUpRangePow = 1.4f;
                break;
            case 5:
                BuffDebuffManager.instance.shpPickUpRangePow = 1.5f;
                break;
        }
        PickUP.instance.PickUPRangeUP();
    }
}
