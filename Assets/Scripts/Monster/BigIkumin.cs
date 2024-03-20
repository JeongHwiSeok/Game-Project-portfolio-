using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigIkumin : BigMonster
{
    private void Awake()
    {
        maxHp = 300;
        hp = maxHp;
        atk = 10;
        speed = GameManager.instance.MonsterSpeed;
    }
}
