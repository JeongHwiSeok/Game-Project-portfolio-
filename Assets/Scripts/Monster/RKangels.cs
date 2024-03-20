using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RKangels : Monster
{
    private void Awake()
    {
        maxHp = 75;
        hp = maxHp;
        atk = 5;
    }
}
