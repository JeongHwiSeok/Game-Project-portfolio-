using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yuragumi1 : Monster
{
    private void Awake()
    {
        maxHp = 150;
        hp = maxHp;
        atk = 6;
    }
}
