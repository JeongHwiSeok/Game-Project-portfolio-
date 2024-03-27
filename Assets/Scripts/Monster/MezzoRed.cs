using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MezzoRed : Monster
{
    private void Awake()
    {
        maxHp = 275;
        hp = maxHp;
        atk = 8;
    }
}
