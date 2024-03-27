using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penpals : Monster
{
    private void Awake()
    {
        maxHp = 350;
        hp = maxHp;
        atk = 9;
    }
}
