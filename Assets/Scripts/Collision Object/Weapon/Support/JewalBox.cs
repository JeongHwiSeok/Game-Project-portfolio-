using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewalBox : MonoBehaviour
{
    [SerializeField] public int itemLV;

    public static JewalBox instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        instance = this;
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                BuffDebuffManager.instance.jbCoinPow = 3;
                break;
            case 2:
                BuffDebuffManager.instance.jbCoinPow = 5;
                break;
            case 3:
                BuffDebuffManager.instance.jbCoinPow = 10;
                break;
        }
    }
}
