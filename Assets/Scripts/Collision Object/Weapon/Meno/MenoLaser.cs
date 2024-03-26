using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoLaser : Weapon
{
    [SerializeField] GameObject parent;

    private void OnEnable()
    {
        if (DataManager.instance.subArray[2, 7] == 1)
        {
            atk = 50 * (MenoManager.instance.jewalCount / 100 + 1) * MenoManager.instance.pickUpBuff;
        }
        else if (DataManager.instance.subArray[2, 7] == 2)
        {
            atk = 50 * (MenoManager.instance.jewalCount / 50 + 1) * MenoManager.instance.pickUpBuff;
        }
        else
        {
            atk = 50 * (MenoManager.instance.jewalCount / 25 + 1) * MenoManager.instance.pickUpBuff;
        }
        MenoManager.instance.jewalCount = 0;
    }

    private void Update()
    {
        if (GameManager.instance.state)
        {
            GetComponent<Animator>().speed = 1.0f;
        }
        else
        {
            GetComponent<Animator>().speed = 0f;
        }
    }

    public void ColliderOn()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderOff()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ObjectOff()
    {
        parent.SetActive(false);
    }
}
