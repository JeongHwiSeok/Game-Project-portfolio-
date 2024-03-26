using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected int coin;
    [SerializeField] protected int exp;
    [SerializeField] public bool pickUPCheck;

    [SerializeField] Vector3 direction;

    [SerializeField] int speed;

    protected virtual void OnEnable()
    {
        speed = 3;
    }

    protected virtual void Update()
    {
        PickUPItem();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if(obj.GetComponent<PickUP>() != null)
        {
            pickUPCheck = true;
        }
        if (obj.GetComponent<PlayerManager>() != null)
        {
            UIManager.instance.DropCoin += coin;

            if (PuzzleGameCollection.instance != null)
            {
                PlayerManager.instance.exp += (exp * BuffDebuffManager.instance.pgcExpPow);
            }
            else
            {
                PlayerManager.instance.exp += exp;
            }

            if (gameObject.name == "coin(Clone)")
            {
                DropItemManager.instance.removeCoin(gameObject);
            }
            else if (gameObject.name == "RedCoin(Clone)")
            {
                DropItemManager.instance.removeCoin(gameObject);
            }
            else
            {
                DropItemManager.instance.removeDropItem(gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    protected virtual void PickUPItem()
    {
        if(pickUPCheck && GameManager.instance.state)
        {
            direction = GameManager.instance.player.transform.position - transform.position;

            direction.z = 0f;
            direction.Normalize();

            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
