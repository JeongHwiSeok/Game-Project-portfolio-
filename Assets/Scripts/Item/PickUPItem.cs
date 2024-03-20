using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUPItem : Item
{
    private void Awake()
    {
        exp = 0;
        coin = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.GetComponent<PickUP>() != null)
        {
            pickUPCheck = true;
        }
        if (obj.GetComponent<PlayerManager>() != null)
        {
            DropItemManager.instance.AllPickUp();

            Destroy(this.gameObject);
        }
    }
}
