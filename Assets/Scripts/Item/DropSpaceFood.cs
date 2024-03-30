using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpaceFood : MonoBehaviour
{
    [SerializeField] Vector3 target;
    [SerializeField] GameObject spaceFood;
    [SerializeField] bool drop;

    public Vector3 Target
    {
        set { target = value; }
        get { return target; }
    }

    private void Update()
    {
        if (drop)
        {
            return;
        }
        else
        {
            if (GameManager.instance.state)
            {
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 10);

                if ((transform.position - target).sqrMagnitude < 0.1f)
                {
                    transform.position = target;
                    drop = true;
                }
            }
        }
    }

    public void GameObjectInput(GameObject obj)
    {
        spaceFood = obj;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.GetComponent<PlayerManager>() != null)
        {
            if (spaceFood.GetComponent<SpaceFood>().flag == false)
            {
                spaceFood.GetComponent<SpaceFood>().PickUP();
                Destroy(this.gameObject);
            }
        }
    }
}
