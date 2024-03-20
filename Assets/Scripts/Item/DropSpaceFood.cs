using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpaceFood : MonoBehaviour
{
    [SerializeField] Vector3 target;
    [SerializeField] GameObject spaceFood;

    public Vector3 Target
    {
        set { target = value; }
        get { return target; }
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * 5);
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
