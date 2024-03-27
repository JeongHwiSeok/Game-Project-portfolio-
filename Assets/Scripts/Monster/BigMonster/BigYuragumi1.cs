using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigYuragumi1 : BigMonster
{
    private void Awake()
    {
        maxHp = 1500;
        hp = maxHp;
        atk = 12;
    }
}
