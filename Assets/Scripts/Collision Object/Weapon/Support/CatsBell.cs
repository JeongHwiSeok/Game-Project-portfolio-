using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsBell : MonoBehaviour
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
                SpawnManager.instance.cbDebuff = 1.1f;
                break;
            case 2:
                SpawnManager.instance.cbDebuff = 1.25f;
                break;
            case 3:
                SpawnManager.instance.cbDebuff = 1.5f;
                break;
            case 4:
                SpawnManager.instance.cbDebuff = 1.75f;
                break;
            case 5:
                SpawnManager.instance.cbDebuff = 2f;
                break;
        }
    }
}
