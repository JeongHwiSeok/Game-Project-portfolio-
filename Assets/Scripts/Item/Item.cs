using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected int coin;
    [SerializeField] protected int exp;
    [SerializeField] protected bool pickUPCheck;

    [SerializeField] Vector3 direction;

    [SerializeField] int speed;

    public virtual int Coin
    {
        get { return coin; }
    }
    public virtual int Exp
    {
        get { return exp; }
    }

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
            DataManager.instance.data.shopCoin += coin;

            PlayerManager.instance.exp += exp;

            DropItemManager.instance.removeDropItem(gameObject);

            Destroy(this.gameObject);
        }
    }

    protected virtual void PickUPItem()
    {
        if(pickUPCheck)
        {
            direction = GameManager.instance.player.transform.position - transform.position;

            direction.z = 0f;
            direction.Normalize();

            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
