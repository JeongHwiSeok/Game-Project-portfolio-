using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondHand : Weapon
{
    public void GameObjectOff()
    {
        Destroy(gameObject);
    }
}
