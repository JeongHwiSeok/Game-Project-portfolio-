using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMezzo : BigMonster
{
    private void Awake()
    {
        maxHp = 1200;
        hp = maxHp;
        atk = 12;
    }
}
