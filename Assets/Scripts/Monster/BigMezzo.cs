using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMezzo : BigMonster
{
    private void Awake()
    {
        maxHp = 500;
        hp = maxHp;
        atk = 10;
        speed = GameManager.instance.MonsterSpeed;
    }
}
