using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mezzo : Monster
{
    private void Awake()
    {
        maxHp = 120;
        hp = maxHp;
        atk = 6;
    }
}
