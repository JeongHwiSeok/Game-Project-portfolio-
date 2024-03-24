using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSearch : MonoBehaviour
{
    [SerializeField] bool flag;

    [SerializeField] Mushroom mushroom;

    private void OnEnable()
    {
        flag = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();

        if (monster != null && flag)
        {
            mushroom.target = monster.transform.position;
            mushroom.Target();
            mushroom.RunOn();
            flag = false;
        }
    }
}
