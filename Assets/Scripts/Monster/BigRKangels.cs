using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRKangels : BigMonster
{
    private void Awake()
    {
        maxHp = 750;
        hp = maxHp;
        atk = 10;
        speed = GameManager.instance.MonsterSpeed;
    }
}
