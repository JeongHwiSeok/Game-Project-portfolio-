using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mezzo : Monster
{
    private void Awake()
    {
        maxHp = 50;
        hp = maxHp;
        atk = 5;
        speed = GameManager.instance.MonsterSpeed;
    }
}
