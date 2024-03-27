using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watai : Monster
{
    private void Awake()
    {
        maxHp = 200; 
        hp = maxHp;
        atk = 7;
    }
}
