using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminBlackAttack : Weapon
{
    [SerializeField] Vector3 direction;

    private void OnEnable()
    {
        normalspeed = 5f;
        atk = 0f;
        knockBack = 1f;
        speed = normalspeed;
        direction = Random.insideUnitSphere;
        direction.z = 0;
        Target();
    }

    protected virtual void Update()
    {
        if (GameManager.instance.state)
        {
            Target();
            PositionStatus(direction);
        }
    }

    protected void Target()
    {
        if (direction.x >= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    protected void PositionStatus(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Monster>() != null || collision.GetComponent<BigMonster>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.transform.GetChild(0).GetComponent<IkuminBlackBoom>().gameObject.SetActive(true);
        }
    }
}
