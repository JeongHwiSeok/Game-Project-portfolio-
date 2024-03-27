using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTimeKeeper : BigMonster
{
    private void Awake()
    {
        maxHp = 700;
        hp = maxHp;
        atk = 10;
    }
}
