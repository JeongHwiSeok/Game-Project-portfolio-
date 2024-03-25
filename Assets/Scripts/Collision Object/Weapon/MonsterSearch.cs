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
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();

        if (monster != null && flag)
        {
            mushroom.Target();
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            flag = false;
        }
    }
}
