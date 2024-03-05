using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ikumin : Monster
{
    private void Awake()
    {
        maxHp = 30;
        hp = maxHp;
        atk = 5;
        speed = GameManager.instance.MonsterSpeed;
    }
}
