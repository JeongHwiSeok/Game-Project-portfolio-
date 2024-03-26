using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminBoom : Weapon
{
    public void StatInput(float a, float b, float c, float range)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1.5f * range, 0.6f * range);
        SpeedUP();
    }

    public void Boom()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
