using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : Monster
{
    private void Awake()
    {
        maxHp = 100;
        hp = maxHp;
        atk = 5;
        speed = GameManager.instance.MonsterSpeed;
    }
}
