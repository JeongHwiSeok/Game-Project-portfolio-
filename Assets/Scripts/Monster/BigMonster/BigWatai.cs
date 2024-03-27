using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWatai : BigMonster
{
    private void Awake()
    {
        maxHp = 2000;
        hp = maxHp;
        atk = 14;
    }
}
