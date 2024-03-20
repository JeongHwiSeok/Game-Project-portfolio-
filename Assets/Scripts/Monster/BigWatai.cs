using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWatai : BigMonster
{
    private void Awake()
    {
        maxHp = 1500;
        hp = maxHp;
        atk = 10;
        speed = GameManager.instance.MonsterSpeed;
    }
}
