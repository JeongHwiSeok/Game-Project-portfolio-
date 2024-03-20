using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewalBox : MonoBehaviour
{
    [SerializeField] public int itemLV;
    [SerializeField] int pow;
    [SerializeField] bool redCoin;

    public bool RedCoin
    {
        get { return redCoin; }
    }

    public int Pow
    {
        get { return pow; }
    }

    public static JewalBox instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        pow = 1;
        redCoin = true;
        instance = this;
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                pow = 3;
                break;
            case 2:
                pow = 5;
                break;
            case 3:
                pow = 10;
                break;
        }
    }
}
