using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameCollection : MonoBehaviour
{
    [SerializeField] public int itemLV;

    public static PuzzleGameCollection instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        itemLV = 1;
        instance = this;
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                BuffDebuffManager.instance.pgcExpPow = 1.1f;
                break;
            case 2:
                BuffDebuffManager.instance.pgcExpPow = 1.2f;
                break;
            case 3:
                BuffDebuffManager.instance.pgcExpPow = 1.3f;
                break;
            case 4:
                BuffDebuffManager.instance.pgcExpPow = 1.4f;
                break;
            case 5:
                BuffDebuffManager.instance.pgcExpPow = 1.5f;
                break;
        }
    }
}
