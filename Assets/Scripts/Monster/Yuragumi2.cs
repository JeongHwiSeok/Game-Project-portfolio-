using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yuragumi2 : Monster
{
    private void Awake()
    {
        maxHp = 300;
        hp = maxHp;
        atk = 8;
    }
}
