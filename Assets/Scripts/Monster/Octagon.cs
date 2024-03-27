using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octagon : Monster
{
    private void Awake()
    {
        maxHp = 50;
        hp = maxHp;
        atk = 5;
    }
}
