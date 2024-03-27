using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRKangels : BigMonster
{
    private void Awake()
    {
        maxHp = 2500;
        hp = maxHp;
        atk = 14;
    }
}
