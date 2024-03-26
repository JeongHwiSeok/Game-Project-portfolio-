using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomBoom : Weapon
{
    public void StatInput(float a, float b, float c, float range)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        gameObject.GetComponent<CircleCollider2D>().radius = 0.2f * range;
        SpeedUP();
    }

    public void Boom()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
