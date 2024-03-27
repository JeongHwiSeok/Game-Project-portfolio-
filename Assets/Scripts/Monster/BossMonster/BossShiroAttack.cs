using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShiroAttack : MonoBehaviour
{
    [SerializeField] BossShiro bossShiro;
    [SerializeField] GameObject attackRange;

    private void OnEnable()
    {
        attackRange.GetComponent<PolygonCollider2D>().enabled = true;
    }

    public void GameObjectOff()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null)
        {
            collision.GetComponent<PlayerManager>().Damage(bossShiro.ATK);
        }
    }

    private void OnDisable()
    {
        bossShiro.attackStap[1] = false;
        bossShiro.attackStap[2] = true;
        attackRange.GetComponent<PolygonCollider2D>().enabled = false;
        attackRange.SetActive(false);
    }
}
