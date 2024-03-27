using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigYuragumi2 : BigMonster
{
    private void Awake()
    {
        maxHp = 4000;
        hp = maxHp;
        atk = 16;
    }
}
