using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMezzoRed : BigMonster
{
    private void Awake()
    {
        maxHp = 3500;
        hp = maxHp;
        atk = 16;
    }
}
