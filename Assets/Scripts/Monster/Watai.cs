using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watai : Monster
{
    private void Awake()
    {
        maxHp = 150; 
        hp = maxHp;
        atk = 5;
    }
}
