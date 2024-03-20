using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameCollection : MonoBehaviour
{
    [SerializeField] public int itemLV;
    [SerializeField] float pow;

    public static PuzzleGameCollection instance
    {
        get;
        private set;
    }

    public float Pow
    {
        get { return pow; }
    }

    private void Awake()
    {
        itemLV = 1;
        pow = 1;
        instance = this;
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                pow = 1.1f;
                break;
            case 2:
                pow = 1.2f;
                break;
            case 3:
                pow = 1.3f;
                break;
            case 4:
                pow = 1.4f;
                break;
            case 5:
                pow = 1.5f;
                break;
        }
    }
}
